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

EMAIL=os.getenv("EMAIL")
EMAIL_PASSWORD=os.getenv("EMAIL_PASSWORD")
CORE_URL=os.getenv("CORE_URL")

app = FastAPI(title="FastAPI → SMTP")

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
        with smtplib.SMTP_SSL("smtp.yandex.ru", 465) as server:
            server.set_debuglevel(1)
            server.login(EMAIL, EMAIL_PASSWORD)
            server.auth_plain()
            server.send_message(msg)
            server.quit()
        print("✅ Письмо успешно отправлено!")
    except smtplib.SMTPAuthenticationError:
        print("❌ Ошибка авторизации: проверьте пароль приложения и наличие 2FA.")
    except smtplib.SMTPException as e:
        print(f"❌ Ошибка SMTP: {e}")

@app.post("/publish")
def publish(userId: int, message: str):
    response = requests.get(f"{CORE_URL}/api/user/{userId}/get-mail", timeout=10)
    response.raise_for_status()
    address = response.text.strip()
    send_email(address, "SYSTEM", message)
    print(f"Successfully sent to {address} message: {message}")

if __name__ == "__main__":
    print("Starting server initialization...")
    
    uvicorn.run(
        f"EmailNotificationService:app",
        host="0.0.0.0",
        port=5000,
        log_level="info"
    )
