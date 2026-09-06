import asyncio
import logging
import os

from vkbottle import Bot, Callback, Keyboard
from vkbottle.bot import Message, MessageEvent
from vkbottle_types.events import GroupEventType

from botconnection import BotApiClient, ChatBotNotificationsConsumer

logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s %(levelname)s %(name)s: %(message)s",
)
logger = logging.getLogger("vk_bot")

VK_TOKEN = os.environ.get("VK_TOKEN")
# Локальный запуск: адрес BotCore. В docker-compose приходит через переменную окружения.
BOTCORE_URL = os.environ.get("BOTCORE_URL", "http://localhost:5003")
# Опционально: без RabbitMQ бот работает, но уведомления из системы не доставляет.
RABBITMQ_URL = os.environ.get("RABBITMQ_URL")

REALIZATION_TAG = "vk"

botcore_client = BotApiClient(BOTCORE_URL, REALIZATION_TAG)
bot = Bot(token=VK_TOKEN)


def build_keyboard(buttons: dict) -> str | None:
    """Словарь {текст кнопки: callback-команда} -> inline-клавиатура VK.

    Ограничения VK: подпись кнопки до 40 символов, payload до 255 байт.
    """
    if not buttons:
        return None
    keyboard = Keyboard(inline=True)
    for text, command in buttons.items():
        keyboard.row().add(Callback(label=text[:40], payload={"cmd": command}))
    return keyboard.get_json()


@bot.on.message()
async def handle_text(message: Message) -> None:
    """Текстовое сообщение или команда (/start и т.п.) -> текстовый контроллер BotCore."""
    chat_id = message.peer_id
    username = str(message.from_id)
    text = message.text

    logger.info("Сообщение от peer_id=%s: %s", chat_id, text)

    # BotApiClient синхронный (requests) — выносим в поток, чтобы не вешать event loop.
    response = await asyncio.to_thread(
        botcore_client.process_text_message, chat_id, username, text
    )

    await message.answer(
        response.message,
        keyboard=build_keyboard(response.buttons),
    )


@bot.on.raw_event(GroupEventType.MESSAGE_EVENT, dataclass=MessageEvent)
async def handle_callback(event: MessageEvent) -> None:
    """Нажатие callback-кнопки -> callbackquery-контроллер BotCore, ответ подменяет сообщение."""
    chat_id = event.object.peer_id
    username = str(event.object.user_id)
    payload = event.object.payload or {}
    if isinstance(payload, str):  # на случай, если payload пришёл сырым JSON
        import json
        payload = json.loads(payload)
    command = payload.get("cmd", "")

    logger.info("Callback от peer_id=%s: %s", chat_id, command)

    # гасим индикатор загрузки на кнопке сразу
    await bot.api.messages.send_message_event_answer(
        event_id=event.object.event_id,
        user_id=event.object.user_id,
        peer_id=chat_id,
    )

    response = await asyncio.to_thread(
        botcore_client.process_callbackquery_command, chat_id, username, command
    )

    # редактируем исходное сообщение с кнопками вместо отправки нового
    await bot.api.messages.edit(
        peer_id=chat_id,
        conversation_message_id=event.object.conversation_message_id,
        message=response.message,
        keyboard=build_keyboard(response.buttons),
    )


def handle_notification(data: dict) -> None:
    """Вызывается из фонового потока consumer'а RabbitMQ при уведомлении из системы."""
    logger.info("Уведомление для chat_id=%s: %s", data["userChatId"], data["message"])
    asyncio.run_coroutine_threadsafe(
        bot.api.messages.send(
            user_id=data["userChatId"], message=data["message"], random_id=0
        ),
        bot.loop_wrapper.loop,
    ).result(timeout=10)


async def on_startup() -> None:
    if RABBITMQ_URL:
        consumer = ChatBotNotificationsConsumer(RABBITMQ_URL, REALIZATION_TAG)
        consumer.start(handler=handle_notification, blocking=False)
        logger.info("Consumer уведомлений запущен (RabbitMQ)")
    else:
        logger.info("RABBITMQ_URL не задан — уведомления отключены")


def main() -> None:
    if not VK_TOKEN:
        raise RuntimeError(
            "Не задан VK_TOKEN — создайте ключ доступа в управлении сообществом "
            "(Работа с API -> Ключи доступа, права: сообщения)"
        )

    bot.loop_wrapper.on_startup.append(on_startup())
    logger.info("Бот запущен, BotCore: %s", BOTCORE_URL)

    # На Python 3.12+ get_event_loop() внутри vkbottle не создаёт цикл сам —
    # создаём и ставим текущим явно.
    asyncio.set_event_loop(asyncio.new_event_loop())
    bot.run_forever()


if __name__ == "__main__":
    main()
