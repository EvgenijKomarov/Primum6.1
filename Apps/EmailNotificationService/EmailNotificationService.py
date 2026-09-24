import asyncio
import logging
import os
import smtplib
import time
from contextlib import asynccontextmanager
from dataclasses import dataclass, field
from email.message import EmailMessage

import uvicorn
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, Field

from TemplateRender import EmailTemplate, render_email

logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s %(levelname)s %(name)s: %(message)s",
)
log = logging.getLogger("mailer")

# ---------- Конфигурация ----------

EMAIL = os.getenv("EMAIL")
EMAIL_PASSWORD = os.getenv("EMAIL_PASSWORD")
CORE_URL = os.getenv("CORE_URL")

SMTP_HOST = "smtp.yandex.ru"
SMTP_PORT = 465
SMTP_TIMEOUT = 10          # сек, чтобы зависший SMTP не блокировал воркер

SEND_INTERVAL = 2.0        # пауза между письмами, сек (~30 писем/мин)
MAX_QUEUE = 1000           # при переполнении /publish отвечает 503
MAX_ATTEMPTS = 3           # всего попыток на письмо
RETRY_BASE_DELAY = 10.0    # 10 -> 20 -> 40 ... сек между попытками
MESSAGE_TTL = 600          # письма старше 10 минут не отправляем

PRIORITY_HIGH = 0          # коды подтверждения и т.п.
PRIORITY_NORMAL = 1        # всё остальное


# ---------- Модель задачи ----------

@dataclass(order=True)
class Job:
    # Сортировка только по (priority, created): при равном приоритете
    # раньше созданные письма (в том числе вернувшиеся с ретрая) идут первыми.
    priority: int
    created: float
    address: str = field(compare=False)
    subject: str = field(compare=False)
    body: str = field(compare=False)
    template: EmailTemplate = field(compare=False)
    attempt: int = field(default=0, compare=False)


# ---------- Отправка (синхронная, бросает исключения) ----------

def send_email(
    address: str,
    subject: str,
    body: str,
    template: EmailTemplate = EmailTemplate.INFO,
) -> None:
    msg = EmailMessage()
    msg["Subject"] = subject
    msg["From"] = EMAIL
    msg["To"] = address
    msg.set_content(body)  # текстовый фолбэк для клиентов без HTML
    msg.add_alternative(render_email(template, subject, body), subtype="html")

    with smtplib.SMTP_SSL(SMTP_HOST, SMTP_PORT, timeout=SMTP_TIMEOUT) as server:
        server.login(EMAIL, EMAIL_PASSWORD)
        server.send_message(msg)


def is_permanent_refusal(e: smtplib.SMTPRecipientsRefused) -> bool:
    """True, если все получатели отвергнуты навсегда (5xx). 4xx -- временная ошибка."""
    return all(500 <= code < 600 for code, _ in e.recipients.values())


# ---------- Очередь, воркер, ретраи ----------

async def requeue_later(queue: asyncio.PriorityQueue, job: Job, delay: float) -> None:
    await asyncio.sleep(delay)
    try:
        queue.put_nowait(job)
    except asyncio.QueueFull:
        log.error("queue full, retry dropped for %s", job.address)


def schedule_retry(app: FastAPI, job: Job, reason: object) -> None:
    if job.attempt >= MAX_ATTEMPTS:
        log.error("gave up on %s after %d attempts: %s", job.address, job.attempt, reason)
        return

    delay = RETRY_BASE_DELAY * 2 ** (job.attempt - 1)
    log.warning(
        "attempt %d/%d for %s failed: %s; retry in %.0fs",
        job.attempt, MAX_ATTEMPTS, job.address, reason, delay,
    )
    # Ждём в отдельной задаче, чтобы не блокировать воркер и остальные письма
    task = asyncio.create_task(requeue_later(app.state.queue, job, delay))
    app.state.retry_tasks.add(task)
    task.add_done_callback(app.state.retry_tasks.discard)


async def process(app: FastAPI, job: Job) -> bool:
    """Обрабатывает одну задачу. Возвращает True, если была попытка отправки."""
    # TTL проверяем перед каждой попыткой, в том числе перед ретраем
    if time.monotonic() - job.created > MESSAGE_TTL:
        log.warning("expired, dropped: %s", job.address)
        return False

    job.attempt += 1
    try:
        await asyncio.to_thread(
            send_email, job.address, job.subject, job.body, job.template
        )
        log.info("sent to %s", job.address)
    except smtplib.SMTPRecipientsRefused as e:
        # Должен идти раньше SMTPException: это его подкласс
        if is_permanent_refusal(e):
            # Ящика нет / адрес отвергнут навсегда, так и задумано, не ретраим.
            # Пишем полный ответ сервера: 5xx бывает и по политике (спам, репутация).
            log.info("recipient rejected, skipping %s: %s", job.address, e.recipients)
        else:
            schedule_retry(app, job, e.recipients)
    except (smtplib.SMTPException, OSError) as e:
        # Включая SMTPAuthenticationError: бывает временной (сбой/троттлинг Яндекса)
        schedule_retry(app, job, e)
    return True


async def worker(app: FastAPI) -> None:
    queue: asyncio.PriorityQueue = app.state.queue
    while True:
        job = await queue.get()
        attempted = False
        try:
            attempted = await process(app, job)
        except Exception:
            log.exception("unexpected error while processing %s", job.address)
            attempted = True
        finally:
            queue.task_done()
        if attempted:
            await asyncio.sleep(SEND_INTERVAL)


@asynccontextmanager
async def lifespan(app: FastAPI):
    if not EMAIL or not EMAIL_PASSWORD:
        raise RuntimeError("EMAIL и EMAIL_PASSWORD должны быть заданы в переменных окружения")

    app.state.queue = asyncio.PriorityQueue(maxsize=MAX_QUEUE)
    app.state.retry_tasks = set()
    worker_task = asyncio.create_task(worker(app))
    log.info("mail worker started")
    try:
        yield
    finally:
        worker_task.cancel()
        for t in list(app.state.retry_tasks):
            t.cancel()
        pending = app.state.queue.qsize() + len(app.state.retry_tasks)
        if pending:
            log.warning("shutdown: %d unsent message(s) lost", pending)


app = FastAPI(title="FastAPI → SMTP", lifespan=lifespan)


# ---------- API ----------

class PublishRequest(BaseModel):
    address: str
    subject: str
    message: str
    template: EmailTemplate = EmailTemplate.INFO
    priority: int = Field(default=PRIORITY_NORMAL, ge=PRIORITY_HIGH, le=PRIORITY_NORMAL)


@app.post("/publish", status_code=202)
async def publish(request: PublishRequest):
    job = Job(
        priority=request.priority,
        created=time.monotonic(),
        address=request.address,
        subject=request.subject,
        body=request.message,
        template=request.template,
    )
    try:
        app.state.queue.put_nowait(job)
    except asyncio.QueueFull:
        raise HTTPException(status_code=503, detail="Queue is full")

    # Тело письма не логируем: в нём могут быть коды подтверждения
    log.info("queued email to %s (priority=%d)", request.address, request.priority)
    return {"status": "accepted", "address": request.address, "queued": app.state.queue.qsize()}


# хелсчек
@app.get("/health")
async def health():
    return {"status": "ok", "queued": app.state.queue.qsize()}


if __name__ == "__main__":
    log.info("Starting server initialization...")
    uvicorn.run(
        "EmailNotificationService:app",
        host="0.0.0.0",
        port=5000,
        log_level="info",
    )