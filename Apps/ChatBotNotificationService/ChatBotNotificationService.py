import os
import json
import logging
import threading
from typing import List, Dict

import pika
import requests
import uvicorn
from fastapi import FastAPI, HTTPException, BackgroundTasks

logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] %(message)s",
)
logger = logging.getLogger("ChatBotNotificationService")

RABBITMQ_URL = os.getenv("RABBITMQ_URL")
SIGNSERVICE_URL = os.getenv("SIGNSERVICE_URL")
MODE = os.getenv("MODE", "Development")

if not RABBITMQ_URL:
    raise RuntimeError("Missing env variable RABBITMQ_URL")
if not SIGNSERVICE_URL:
    raise RuntimeError("Missing env variable SIGNSERVICE_URL")

# --- Таймауты, чтобы ничего не могло зависнуть на неопределённый срок ---
HTTP_TIMEOUT_SECONDS = 5          # запрос к SignService
RABBITMQ_SOCKET_TIMEOUT = 5       # установка TCP-соединения с брокером
RABBITMQ_CONNECTION_ATTEMPTS = 3
RABBITMQ_RETRY_DELAY = 1          # секунд между попытками подключения
RABBITMQ_HEARTBEAT = 30

_rabbitmq_params = pika.URLParameters(RABBITMQ_URL)
_rabbitmq_params.socket_timeout = RABBITMQ_SOCKET_TIMEOUT
_rabbitmq_params.connection_attempts = RABBITMQ_CONNECTION_ATTEMPTS
_rabbitmq_params.retry_delay = RABBITMQ_RETRY_DELAY
_rabbitmq_params.heartbeat = RABBITMQ_HEARTBEAT
_rabbitmq_params.blocked_connection_timeout = RABBITMQ_SOCKET_TIMEOUT

app = FastAPI(title="FastAPI → RabbitMQ Publisher")

# --- Переиспользуемое соединение с RabbitMQ ---
# pika.BlockingConnection не потокобезопасен, поэтому защищаем его локом:
# открывать новое соединение на каждое сообщение (как было раньше) —
# дорого и является одной из причин зависаний.
_connection_lock = threading.Lock()
_connection: pika.BlockingConnection | None = None
_channel: pika.adapters.blocking_connection.BlockingChannel | None = None
_declared_exchanges: set[str] = set()


def _ensure_connection() -> pika.adapters.blocking_connection.BlockingChannel:
    """Возвращает рабочий channel, переоткрывая соединение при необходимости."""
    global _connection, _channel

    if _connection is not None and _connection.is_open and _channel is not None and _channel.is_open:
        return _channel

    logger.info("Opening new RabbitMQ connection")
    _connection = pika.BlockingConnection(_rabbitmq_params)
    _channel = _connection.channel()
    _declared_exchanges.clear()
    return _channel


def _publish_one(tag: str, user_chat_id: int, username: str, message: str) -> None:
    with _connection_lock:
        channel = _ensure_connection()

        if tag not in _declared_exchanges:
            channel.exchange_declare(exchange=tag, exchange_type="fanout", durable=True)
            _declared_exchanges.add(tag)

        message_body = json.dumps(
            {
                "userChatId": user_chat_id,
                "username": username,
                "message": message,
            }
        ).encode("utf-8")

        channel.basic_publish(
            exchange=tag,
            routing_key="",
            body=message_body,
            properties=pika.BasicProperties(delivery_mode=2),  # persistent
        )
        logger.info("Pushed on %s: %s", tag, message)


def _get_user_signs(user_id: int) -> List[Dict]:
    response = requests.get(
        f"{SIGNSERVICE_URL}/get-signs/{user_id}",
        timeout=HTTP_TIMEOUT_SECONDS,
    )
    response.raise_for_status()
    return response.json()


def _process_publish(user_id: int, message: str) -> None:
    """Вся тяжёлая работа выполняется здесь, уже ПОСЛЕ того как клиенту
    отдан ответ, чтобы медленный SignService/RabbitMQ не мог заставить
    вызывающую сторону (C#-клиент) упереться в её собственный таймаут."""
    try:
        all_user_signs = _get_user_signs(user_id)
    except requests.RequestException:
        logger.exception("Failed to fetch signs for userId=%s", user_id)
        return

    for sign in all_user_signs:
        try:
            _publish_one(
                sign["realizationTag"],
                int(sign["chatId"]),
                sign["username"],
                message,
            )
        except Exception:
            # Одна упавшая публикация не должна прерывать рассылку остальным
            # получателям того же userId.
            logger.exception(
                "Failed to publish notification for userId=%s, tag=%s",
                user_id,
                sign.get("realizationTag"),
            )
            # Соединение могло протухнуть — сбросим его, чтобы следующая
            # попытка открыла новое, а не билась в мёртвый сокет.
            global _connection, _channel
            with _connection_lock:
                _connection = None
                _channel = None


@app.post("/publish", status_code=202)
def publish(user_id: int, message: str, background_tasks: BackgroundTasks):
    """Сразу подтверждает приём запроса и выполняет реальную рассылку в фоне.

    Раньше эндпоинт делал всю работу синхронно и отвечал клиенту только
    в конце — при медленном SignService или RabbitMQ это могло занимать
    больше таймаута HttpClient на стороне вызывающего сервиса, из-за чего
    клиент считал вызов проваленным и повторял его, хотя уведомление уже
    было отправлено. Теперь ответ уходит немедленно, а публикация
    происходит асинхронно после него.
    """
    background_tasks.add_task(_process_publish, user_id, message)
    return {"status": "accepted"}


@app.get("/health")
async def health():
    return {"status": "ok"}


if __name__ == "__main__":
    logger.info("Starting server initialization...")
    uvicorn.run(
        "ChatBotNotificationService:app",
        host="0.0.0.0",
        port=5000,
        log_level="info",
    )