import os
import smtplib
from email.message import EmailMessage

from fastapi import BackgroundTasks, FastAPI
import uvicorn

from TemplateRender import EmailTemplate, render_email

EMAIL = os.getenv("EMAIL")
EMAIL_PASSWORD = os.getenv("EMAIL_PASSWORD")
CORE_URL = os.getenv("CORE_URL")

app = FastAPI(title="FastAPI → SMTP")


def send_email(
    address: str,
    subject: str,
    body: str,
    template: EmailTemplate = EmailTemplate.INFO,
) -> None:
    if not EMAIL or not EMAIL_PASSWORD:
        print("❌ Ошибка: EMAIL и EMAIL_PASSWORD должны быть установлены в переменных окружения.")
        print("Address:", address)
        print("Body:", body)
        return

    msg = EmailMessage()
    msg["Subject"] = subject
    msg["From"] = EMAIL
    msg["To"] = address
    msg.set_content(body)  # текстовый фолбэк для клиентов без HTML

    html = render_email(template, subject, body)
    msg.add_alternative(html, subtype="html")

    try:
        with smtplib.SMTP_SSL("smtp.yandex.ru", 465) as server:
            server.login(EMAIL, EMAIL_PASSWORD)
            server.send_message(msg)
        print(f"✅ Письмо успешно отправлено на {address}!")
    except smtplib.SMTPAuthenticationError:
        print("❌ Ошибка авторизации: проверьте пароль приложения и наличие 2FA.")
    except smtplib.SMTPException as e:
        print(f"❌ Ошибка SMTP: {e}")


@app.post("/publish", status_code=202)
def publish(
    background_tasks: BackgroundTasks,
    address: str,
    subject: str,
    message: str,
    template: EmailTemplate = EmailTemplate.INFO,
):
    background_tasks.add_task(send_email, address, subject, message, template)
    print(f"Queued email to {address}: {message}")
    return {"status": "accepted", "address": address}


# хелсчек
@app.get("/health")
async def health():
    return {"status": "ok"}


if __name__ == "__main__":
    print("Starting server initialization...")

    uvicorn.run(
        "EmailNotificationService:app",
        host="0.0.0.0",
        port=5000,
        log_level="info",
    )