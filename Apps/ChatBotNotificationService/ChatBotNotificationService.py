import os
import json
from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import uvicorn
import pika
import requests
from get_url import load_variable, load_url, load_host_and_port

RABBITMQ_URL = load_variable("RabbitMQConnection")
SIGNSERVICE_URL = load_url("SignService/PublicUrl")

is_prod_mode = os.getenv("MODE", "Development") == "Production"
parameters = pika.URLParameters(RABBITMQ_URL)

app = FastAPI(title="FastAPI → RabbitMQ Publisher")

def rabbitmq_post(tag: str, userChatId: int, username: str, message: str):
    if is_prod_mode:
        with pika.BlockingConnection(parameters) as connection:
            channel = connection.channel()
    
            # 1. Объявляем fanout-обменник (durable=True = переживёт перезагрузку брокера)
            channel.exchange_declare(exchange=tag, exchange_type='fanout', durable=True)

            # Формируем JSON-сообщение и конвертируем в байты
            message_body = json.dumps({
                "userChatId": userChatId,
                "username": username,
                "message": message
            }).encode('utf-8')

            channel.basic_publish(
                exchange=tag,
                routing_key='',
                body=message_body,
                properties=pika.BasicProperties(delivery_mode=2) # persistent
            )
            print(f"✅Pushed on {tag}: {message}")
    else:
        print(f"Fakely pushed on: {tag}: {message}")

@app.post("/publish")
def publish(userId: int, message: str):
    # Получаем ответ
    response = requests.get(f"{SIGNSERVICE_URL}/get-signs/{userId}")
    
    # На случай, если SignService вернет HTTP-ошибку (404, 500 и т.д.)
    response.raise_for_status() 
    
    # Парсим JSON в список словарей
    all_user_signs = response.json()

    for sign in all_user_signs:
        rabbitmq_post(
            sign["realizationTag"], 
            int(sign["chatId"]), 
            sign["username"], 
            message
        )
    return {"status": "ok"}

if __name__ == "__main__":
    print("Starting server initialization...")
    
    HOST, PORT = load_host_and_port("ChatBotNotificationService/SelfUrl")
    print(f"Binding to http://{HOST}:{PORT}")
    
    uvicorn.run(
        f"ChatBotNotificationService:app",
        host=HOST,
        port=PORT,
        log_level="info"
    )
