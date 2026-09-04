import json
import requests
import logging
import threading
from .models import Response, Request, Sign
from typing import Callable, Optional

import pika

class BotApiClient:
    def __init__(self, base_url: str, realization_tag: str, timeout: float = 10.0):
        self.base_url = base_url.rstrip("/")
        self.timeout = timeout
        self.realization_tag = realization_tag
        self.session = requests.Session()

    def process_text_message(self, chat_id: int, username: str, data: str) -> Response:
        return self.__send_message(chat_id, username, data, "/bot-core/text-command")

    def process_callbackquery_command(self, chat_id: int, username: str, data: str) -> Response:
        return self.__send_message(chat_id, username, data, "/bot-core/callbackquery-command")

    def __send_message(self, chat_id: int, username: str, data: str, endpoint: str) -> Response:
        body = Request(
            sign=Sign(
                realization_tag=self.realization_tag,
                chat_id=chat_id,
                username=username,
            ),
            data=data,
        )

        url = f"{self.base_url}{endpoint}"

        try:
            response = self.session.post(
                url,
                json=body.to_dict(),
                timeout=self.timeout,
            )
            response.raise_for_status()
        except requests.exceptions.RequestException as e:
            print("Ошибка запроса к ", url, e)
            raise RuntimeError(f"Не удалось связаться с ядром по адресу {url}") from e

        try:
            payload = response.json()
        except requests.exceptions.JSONDecodeError as e:
            print("Некорректный JSON в ответе от ", url, e)
            raise RuntimeError(f"Сервер вернул некорректный JSON") from e

        result = Response.from_dict(payload)

        print(
            "Получен ответ: message_len=%d, buttons_count=%d",
            len(getattr(result, 'message', '')), len(getattr(result, 'buttons', [])),
        )

        return result

class ChatBotNotificationsConsumer:

    def __init__(
        self,
        rabbitmq_url: str,
        tag: str,
        queue_name: Optional[str] = None,
    ):
        self.rabbitmq_url = rabbitmq_url
        # своя устойчивая очередь на этот tag, чтобы сообщения не терялись
        # между рестартами бота (durable + фиксированное имя, а не auto-delete)
        self.tag = tag
        self.queue_name = queue_name or f"{tag}.telegram-bot.queue"

        self._connection: Optional[pika.BlockingConnection] = None
        self._channel = None
        self._thread: Optional[threading.Thread] = None

    def _connect(self):
        parameters = pika.URLParameters(self.rabbitmq_url)
        self._connection = pika.BlockingConnection(parameters)
        self._channel = self._connection.channel()

        self._channel.exchange_declare(
            exchange=self.tag, exchange_type="fanout", durable=True
        )
        self._channel.queue_declare(queue=self.queue_name, durable=True)
        self._channel.queue_bind(exchange=self.tag, queue=self.queue_name)

        # обрабатываем по одному сообщению за раз, пока не подтвердили предыдущее
        self._channel.basic_qos(prefetch_count=1)

    def _on_message(self, channel, method, properties, body, handler: Callable[[dict], None]):
        try:
            data = json.loads(body.decode("utf-8"))
        except json.JSONDecodeError as e:
            print("Некорректный JSON в сообщении из ", self.queue_name, e)
            channel.basic_nack(delivery_tag=method.delivery_tag, requeue=False)
            return

        try:
            handler(data)
            channel.basic_ack(delivery_tag=method.delivery_tag)
        except Exception as e:
            print("Ошибка обработки сообщения из ", self.queue_name, e)
            # requeue=False — чтобы битое сообщение не зацикливалось бесконечно;
            # если нужна dead-letter очередь, настройте её отдельно на exchange
            channel.basic_nack(delivery_tag=method.delivery_tag, requeue=False)

    def start(self, handler: Callable[[dict], None], blocking: bool = True):
        """
        handler(data: dict) — ваша функция обработки сообщения,
        data = {"userChatId": ..., "username": ..., "message": ...}

        blocking=True  — блокирует текущий поток (start_consuming не вернёт управление).
        blocking=False — запускает consumer в отдельном daemon-потоке.
        """
        self._connect()

        self._channel.basic_consume(
            queue=self.queue_name,
            on_message_callback=lambda ch, method, properties, body: self._on_message(
                ch, method, properties, body, handler
            ),
        )

        print("Consumer запущен: ", self.tag, self.queue_name)

        if blocking:
            self._consume_loop()
        else:
            self._thread = threading.Thread(target=self._consume_loop, daemon=True)
            self._thread.start()

    def _consume_loop(self):
        try:
            self._channel.start_consuming()
        except Exception as e:
            print("Ошибка в цикле consumer'а: ", e, exc_info=True)

    def stop(self):
        if self._channel is not None:
            try:
                self._channel.stop_consuming()
            except Exception:
                pass
        if self._connection is not None and self._connection.is_open:
            self._connection.close()
        if self._thread is not None:
            self._thread.join(timeout=5)
        print("Consumer остановлен: exchange=", self.tag)