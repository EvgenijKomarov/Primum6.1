import os
from contextlib import asynccontextmanager
from urllib import request
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import uvicorn
import aio_pika
import smtplib
import requests
from email.message import EmailMessage
from get_url import load_url, load_variable, load_host_and_port

is_prod_mode = os.getenv("MODE", "Development") == "Production"

EMAIL=os.getenv("EMAIL", "")
EMAIL_PASSWORD=os.getenv("EMAIL_PASSWORD", "")
CORE_URL=load_url("PrimumCore/PublicUrl")

app = FastAPI(title="FastAPI → RabbitMQ Publisher")

def send_email(address: str, subject: str, body: str):
    if EMAIL == "" or EMAIL_PASSWORD == "":
        print("❌ Ошибка: EMAIL и EMAIL_PASSWORD должны быть установлены в переменных окружения.")
        print("Adress: ", address)
        print("Body: ", body)
        return

    msg = EmailMessage()
    msg["Subject"]= subject
    msg["From"] = EMAIL
    msg["To"] = address
    msg.set_content(body)

    try:
        with smtplib.SMTP("smtp.yandex.ru", 465) as server:
            server.starttls()
            server.login(EMAIL, EMAIL_PASSWORD)
            server.send_message(msg)
        print("✅ Письмо успешно отправлено!")
    except smtplib.SMTPAuthenticationError:
        print("❌ Ошибка авторизации: проверьте пароль приложения и наличие 2FA.")
    except smtplib.SMTPException as e:
        print(f"❌ Ошибка SMTP: {e}")

@app.post("/publish")
def publish(userId: int, message: str):
    if is_prod_mode:
        response = requests.get(f"{CORE_URL}/api/user/{userId}/get-mail", timeout=10)
        response.raise_for_status()
        address = response.text.strip()
        send_email(address, "SYSTEM", message)
        print(f"Successfully sent to {address} message: {message}")
    else:
        print(f"Send to user {userId} mail with message {message}")

if __name__ == "__main__":
    print("Starting server initialization...")
    
    HOST, PORT = load_host_and_port("EmailNotificationService/SelfUrl")
    print(f"Binding to http://{HOST}:{PORT}")
    
    uvicorn.run(
        f"EmailNotificationService:app",
        host=HOST,
        port=PORT,
        log_level="info"
    )
