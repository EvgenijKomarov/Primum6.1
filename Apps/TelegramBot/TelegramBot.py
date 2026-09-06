import asyncio
import logging
import os

from telegram import InlineKeyboardButton, InlineKeyboardMarkup, Update
from telegram.ext import (
    Application,
    CallbackQueryHandler,
    ContextTypes,
    MessageHandler,
    filters,
)

from botconnection import BotApiClient, ChatBotNotificationsConsumer

logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s %(levelname)s %(name)s: %(message)s",
)
logger = logging.getLogger("telegram_bot")

TELEGRAM_TOKEN = os.environ.get("TELEGRAM_TOKEN")
# Локальный запуск: http-порт BotCore из Apps/BotCore/Properties/launchSettings.json.
# В docker-compose адрес приходит через переменную окружения.
BOTCORE_URL = os.environ.get("BOTCORE_URL", "http://localhost:57293")
# Опционально: без RabbitMQ бот работает, но уведомления из системы не доставляет.
RABBITMQ_URL = os.environ.get("RABBITMQ_URL")
# Опционально: прокси для доступа к Telegram API, если он заблокирован провайдером,
# например socks5h://127.0.0.1:9050 (локальный Tor). В docker-compose не нужен.
TELEGRAM_PROXY = os.environ.get("TELEGRAM_PROXY")

REALIZATION_TAG = "telegram"

botcore_client = BotApiClient(BOTCORE_URL, REALIZATION_TAG)

# Цикл событий, в котором крутится telegram-приложение. Захватывается при старте,
# чтобы поток consumer'а уведомлений мог безопасно отправлять сообщения.
bot_loop: asyncio.AbstractEventLoop | None = None


def get_username(update: Update) -> str:
    user = update.effective_user
    if user is None:
        return ""
    return user.username or user.full_name or str(user.id)


def build_keyboard(buttons: dict) -> InlineKeyboardMarkup | None:
    """Словарь {текст кнопки: callback-команда} -> разметка под сообщением."""
    if not buttons:
        return None
    keyboard = [
        [InlineKeyboardButton(text=text, callback_data=callback)]
        for text, callback in buttons.items()
    ]
    return InlineKeyboardMarkup(keyboard)


async def handle_text(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    """Текстовое сообщение или команда (/start и т.п.) -> текстовый контроллер BotCore."""
    chat_id = update.effective_chat.id
    username = get_username(update)
    text = update.message.text

    logger.info("Сообщение от chat_id=%s (%s): %s", chat_id, username, text)

    # BotApiClient синхронный (requests) — выносим в поток, чтобы не вешать event loop.
    response = await asyncio.to_thread(
        botcore_client.process_text_message, chat_id, username, text
    )

    await update.message.reply_text(
        response.message,
        reply_markup=build_keyboard(response.buttons),
    )


async def handle_callback(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    """Нажатие inline-кнопки -> callbackquery-контроллер BotCore, ответ подменяет сообщение."""
    query = update.callback_query
    chat_id = update.effective_chat.id
    username = get_username(update)
    command = query.data

    logger.info("Callback от chat_id=%s (%s): %s", chat_id, username, command)

    await query.answer()  # гасим "часики" на кнопке сразу, не дожидаясь BotCore

    response = await asyncio.to_thread(
        botcore_client.process_callbackquery_command, chat_id, username, command
    )

    await query.edit_message_text(
        response.message,
        reply_markup=build_keyboard(response.buttons),
    )


def handle_notification(data: dict) -> None:
    """Вызывается из фонового потока consumer'а RabbitMQ при уведомлении из системы."""
    if bot_loop is None:
        logger.warning("Уведомление пропущено: event loop ещё не запущен")
        return

    logger.info("Уведомление для chat_id=%s: %s", data["userChatId"], data["message"])

    asyncio.run_coroutine_threadsafe(
        application.bot.send_message(chat_id=data["userChatId"], text=data["message"]),
        bot_loop,
    ).result(timeout=10)


async def on_startup(app: Application) -> None:
    global bot_loop
    bot_loop = asyncio.get_running_loop()

    if RABBITMQ_URL:
        consumer = ChatBotNotificationsConsumer(RABBITMQ_URL, REALIZATION_TAG)
        consumer.start(handler=handle_notification, blocking=False)
        logger.info("Consumer уведомлений запущен (RabbitMQ)")
    else:
        logger.info("RABBITMQ_URL не задан — уведомления отключены")


async def error_handler(update: object, context: ContextTypes.DEFAULT_TYPE) -> None:
    logger.error("Ошибка при обработке апдейта %s", update, exc_info=context.error)


application: Application | None = None


def main() -> None:
    global application
    if not TELEGRAM_TOKEN:
        raise RuntimeError("Не задан TELEGRAM_TOKEN — возьмите токен у @BotFather")

    builder = (
        Application.builder()
        .token(TELEGRAM_TOKEN)
        .post_init(on_startup)
    )
    if TELEGRAM_PROXY:
        # proxy() действует только на Bot.request; для long polling нужен отдельный
        # get_updates_proxy(), иначе getUpdates пойдёт напрямую мимо прокси.
        builder = builder.proxy(TELEGRAM_PROXY).get_updates_proxy(TELEGRAM_PROXY)
        logger.info("Telegram API через прокси: %s", TELEGRAM_PROXY)
    application = builder.build()

    application.add_handler(MessageHandler(filters.TEXT, handle_text))
    application.add_handler(CallbackQueryHandler(handle_callback))
    application.add_error_handler(error_handler)

    logger.info("Бот запущен, BotCore: %s", BOTCORE_URL)

    # На Python 3.12+ get_event_loop() внутри run_polling не создаёт цикл сам —
    # создаём и ставим текущим явно. post_init захватит именно этот цикл.
    asyncio.set_event_loop(asyncio.new_event_loop())
    application.run_polling(allowed_updates=Update.ALL_TYPES)


if __name__ == "__main__":
    main()
