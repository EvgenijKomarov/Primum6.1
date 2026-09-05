import asyncio
import logging
import os
import time
 
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

BOTCORE_URL = os.getenv("BOTCORE_URL")
TOKEN = os.getenv("TELEGRAM_TOKEN")
RABBITMQ_URL = os.environ["RABBITMQ_URL"]

tag = "telegram"

client = BotApiClient(BOTCORE_URL, tag)
consumer = ChatBotNotificationsConsumer(RABBITMQ_URL, tag)

def build_keyboard(buttons: dict) -> InlineKeyboardMarkup | None:
    if not buttons:
        return None
    keyboard = [
        [InlineKeyboardButton(text=text, callback_data=data)]
        for text, data in buttons.items()
    ]
    return InlineKeyboardMarkup(keyboard)
 
 
async def handle_text(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    """Любое текстовое сообщение, включая команды вроде /start — botcore сам разбирает текст."""
    chat_id = update.effective_chat.id
    username = get_username(update)
    text = update.message.text
 
    # process_text_message скорее всего синхронный HTTP-вызов — уводим его
    # в отдельный поток, чтобы не блокировать event loop бота.
    response = await asyncio.to_thread(client.process_text_message, chat_id, username, text)
 
    await update.message.reply_text(
        response.message,
        reply_markup=build_keyboard(response.buttons),
    )
 
 
async def handle_callback(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    query = update.callback_query
    chat_id = update.effective_chat.id
    username = get_username(update)
    command = query.data
 
    await query.answer()  # убираем "часики" с кнопки сразу
 
    response = await asyncio.to_thread(
        client.process_callbackquery_command, chat_id, username, command
    )
 
    # редактируем исходное сообщение с кнопками вместо отправки нового
    await query.edit_message_text(
        response.message,
        reply_markup=build_keyboard(response.buttons),
    )
 
 
async def error_handler(update: object, context: ContextTypes.DEFAULT_TYPE) -> None:
    logger.error("Ошибка при обработке апдейта %s", update, exc_info=context.error)
 
 
def main() -> None:
    # Захватываем loop текущего потока ДО run_polling — тот же loop будет
    # использовать Application, т.к. они выполняются в одном потоке.
    # Это нужно, чтобы из фонового потока consumer'а безопасно слать сообщения.
    loop = asyncio.get_event_loop()
 
    application = Application.builder().token(TOKEN).build()
 
    application.add_handler(MessageHandler(filters.TEXT, handle_text))
    application.add_handler(CallbackQueryHandler(handle_callback))
    application.add_error_handler(error_handler)
 
    def handle_notification(data: dict) -> None:
        """Вызывается из фонового потока ChatBotNotificationsConsumer."""
        chat_id = data["userChatId"]
        message = data["message"]
        username = data["username"]
 
        logger.info("Уведомление для chat_id=%s (%s): %s", chat_id, username, message)
 
        asyncio.run_coroutine_threadsafe(
            application.bot.send_message(chat_id=chat_id, text=message),
            loop,
        )
 
    consumer.start(handler=handle_notification, blocking=False)
 
    logger.info("Бот запущен, начинаю polling")
    application.run_polling(allowed_updates=Update.ALL_TYPES)
 
 
if __name__ == "__main__":
    main()