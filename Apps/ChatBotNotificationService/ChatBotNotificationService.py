import os
import json
from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import uvicorn
import pika
import requests

RABBITMQ_URL = os.getenv("RABBITMQ_URL")
SIGNSERVICE_URL = os.getenv("SIGNSERVICE_URL")

is_prod_mode = os.getenv("MODE", "Development") == "Production"
parameters = pika.URLParameters(RABBITMQ_URL)

app = FastAPI(title="FastAPI → RabbitMQ Publisher")

def rabbitmq_post(tag: str, userChatId: int, username: str, message: str):
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

#хелсчек
@app.get("/health")
async def health():
    return {"status": "ok"}


if __name__ == "__main__":
    print("Starting server initialization...")
    
    uvicorn.run(
        f"ChatBotNotificationService:app",
        host="0.0.0.0",
        port=5000,
        log_level="info"
    )
