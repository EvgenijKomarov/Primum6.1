import logging
import os
import time

import requests

from botconnection import BotApiClient, ChatBotNotificationsConsumer

logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s %(levelname)s %(name)s: %(message)s",
)
logger = logging.getLogger("max_bot")

MAX_TOKEN = os.environ.get("MAX_TOKEN")
# Локальный запуск: адрес BotCore. В docker-compose приходит через переменную окружения.
BOTCORE_URL = os.environ.get("BOTCORE_URL", "http://localhost:5003")
# Опционально: без RabbitMQ бот работает, но уведомления из системы не доставляет.
RABBITMQ_URL = os.environ.get("RABBITMQ_URL")
# platform-api2.max.ru использует сертификат Минцифры; platform-api.max.ru — обычный TLS.
MAX_API_URL = os.environ.get("MAX_API_URL", "https://platform-api.max.ru")

REALIZATION_TAG = "max"

botcore_client = BotApiClient(BOTCORE_URL, REALIZATION_TAG)

session = requests.Session()
session.headers["Authorization"] = MAX_TOKEN or ""

POLL_TIMEOUT = 30  # секунд long polling; сетевой таймаут должен быть больше
POLL_TYPES = "message_created,message_callback"


def build_attachments(buttons: dict) -> list | None:
    """Словарь {текст кнопки: callback-команда} -> inline-клавиатура MAX."""
    if not buttons:
        return None
    return [
        {
            "type": "inline_keyboard",
            "payload": {
                "buttons": [
                    [{"type": "callback", "text": text[:40], "payload": command}]
                    for text, command in buttons.items()
                ]
            },
        }
    ]


def send_message(user_id: int, text: str, buttons: dict | None = None) -> None:
    body = {"text": text}
    attachments = build_attachments(buttons or {})
    if attachments:
        body["attachments"] = attachments
    response = session.post(
        f"{MAX_API_URL}/messages",
        params={"user_id": user_id},
        json=body,
        timeout=15,
    )
    response.raise_for_status()


def answer_callback(callback_id: str, text: str, buttons: dict | None = None) -> None:
    """Ответ на callback: подменяет сообщение, с которого нажали кнопку."""
    body = {
        "message": {
            "text": text,
            "attachments": build_attachments(buttons or {}) or [],
        }
    }
    response = session.post(
        f"{MAX_API_URL}/answers",
        params={"callback_id": callback_id},
        json=body,
        timeout=15,
    )
    response.raise_for_status()


def handle_message_created(update: dict) -> None:
    """Текстовое сообщение -> текстовый контроллер BotCore."""
    message = update.get("message") or {}
    sender = message.get("sender") or {}
    user_id = sender.get("user_id")
    username = sender.get("name") or str(user_id)
    text = (message.get("body") or {}).get("text") or ""

    if user_id is None or not text:
        return

    logger.info("Сообщение от user_id=%s (%s): %s", user_id, username, text)

    response = botcore_client.process_text_message(user_id, username, text)
    send_message(user_id, response.message, response.buttons)


def handle_message_callback(update: dict) -> None:
    """Нажатие callback-кнопки -> callbackquery-контроллер BotCore, ответ подменяет сообщение."""
    callback = update.get("callback") or {}
    callback_id = callback.get("callback_id")
    command = callback.get("payload") or ""
    user = callback.get("user") or {}
    user_id = user.get("user_id")
    username = user.get("name") or str(user_id)

    if callback_id is None or user_id is None:
        return

    logger.info("Callback от user_id=%s (%s): %s", user_id, username, command)

    response = botcore_client.process_callbackquery_command(user_id, username, command)
    answer_callback(callback_id, response.message, response.buttons)


def handle_notification(data: dict) -> None:
    """Вызывается из фонового потока consumer'а RabbitMQ при уведомлении из системы."""
    logger.info("Уведомление для user_id=%s: %s", data["userChatId"], data["message"])
    send_message(data["userChatId"], data["message"])


def polling_loop() -> None:
    marker = None
    logger.info("Long polling запущен (%s)", MAX_API_URL)
    while True:
        try:
            response = session.get(
                f"{MAX_API_URL}/updates",
                params={"timeout": POLL_TIMEOUT, "marker": marker, "types": POLL_TYPES},
                timeout=POLL_TIMEOUT + 15,
            )
            response.raise_for_status()
            data = response.json()
            marker = data.get("marker", marker)

            for update in data.get("updates", []):
                try:
                    update_type = update.get("update_type")
                    if update_type == "message_created":
                        handle_message_created(update)
                    elif update_type == "message_callback":
                        handle_message_callback(update)
                except Exception:
                    logger.exception("Ошибка обработки обновления: %s", update)
        except requests.exceptions.RequestException as e:
            logger.warning("Ошибка polling: %s — повтор через 5 с", e)
            time.sleep(5)


def main() -> None:
    if not MAX_TOKEN:
        raise RuntimeError(
            "Не задан MAX_TOKEN — создайте бота в MAX и возьмите токен "
            "(Чат-боты -> Расширенные настройки -> Настроить)"
        )

    me = session.get(f"{MAX_API_URL}/me", timeout=15)
    me.raise_for_status()
    logger.info("Авторизован как: %s", me.json())

    if RABBITMQ_URL:
        consumer = ChatBotNotificationsConsumer(RABBITMQ_URL, REALIZATION_TAG)
        consumer.start(handler=handle_notification, blocking=False)
        logger.info("Consumer уведомлений запущен (RabbitMQ)")
    else:
        logger.info("RABBITMQ_URL не задан — уведомления отключены")

    logger.info("Бот запущен, BotCore: %s", BOTCORE_URL)
    polling_loop()


if __name__ == "__main__":
    main()
