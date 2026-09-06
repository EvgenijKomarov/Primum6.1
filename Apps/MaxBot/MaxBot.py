import time
from botconnection import BotApiClient, ChatBotNotificationsConsumer
import os

time.sleep(7)

BOTCORE_URL = os.getenv("BOTCORE_URL")
TOKEN = os.getenv("MAX_TOKEN")
RABBITMQ_URL = os.environ["RABBITMQ_URL"]

tag = "max"

client = BotApiClient(BOTCORE_URL, tag)
consumer = ChatBotNotificationsConsumer(RABBITMQ_URL, tag)

def handle_notification(data: dict): #метод обработки уведомлений
    chat_id = data["userChatId"]
    message = data["message"]
    username = data["username"]

    print(chat_id, username, message)

consumer.start(handler=handle_notification, blocking=False)

#далее идет код для отладки, можно менять на нормальный

while(True):
    time.sleep(10)
    print(client.process_text_message(1, "user", "/start"))
    print(client.process_callbackquery_command(1, "user", "start"))
