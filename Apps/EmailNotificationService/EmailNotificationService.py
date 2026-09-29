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

from TemplateRender import EmailTemplate, TemplateDataError, render_email

logging.basicConfig(
    level="INFO",
    format="%(asctime)s %(levelname)s %(name)s: %(message)s",
)
log = logging.getLogger("mailer")


# ---------- Конфигурация ----------

def _env_str(name: str, default: str) -> str:
    return os.getenv(name) or default


def _env_int(name: str, default: int) -> int:
    raw = os.getenv(name)
    if not raw:
        return default
    try:
        return int(raw)
    except ValueError:
        raise RuntimeError(f"{name} должен быть целым числом, получено: {raw!r}")


def _env_float(name: str, default: float) -> float:
    raw = os.getenv(name)
    if not raw:
        return default
    try:
        return float(raw)
    except ValueError:
        raise RuntimeError(f"{name} должен быть числом, получено: {raw!r}")


# Учётные данные (обязательные)
EMAIL = os.getenv("EMAIL")
EMAIL_PASSWORD = os.getenv("EMAIL_PASSWORD")
EMAIL_FROM = os.getenv("EMAIL_FROM") or EMAIL

# SMTP
SMTP_HOST = _env_str("SMTP_HOST", "smtp.yandex.ru")
SMTP_PORT = _env_int("SMTP_PORT", 465)
SMTP_TIMEOUT = _env_float("SMTP_TIMEOUT", 10.0)
# ssl (implicit TLS, обычно 465) | starttls (обычно 587) | plain (без шифрования, только для тестов)
SMTP_SECURITY = _env_str("SMTP_SECURITY", "ssl" if SMTP_PORT == 465 else "starttls").lower()

# Очередь и ретраи
SEND_INTERVAL = _env_float("SEND_INTERVAL", 2.0)          # пауза между письмами, сек
MAX_QUEUE = _env_int("MAX_QUEUE", 1000)                   # при переполнении /publish отвечает 503
MAX_ATTEMPTS = _env_int("MAX_ATTEMPTS", 3)                # всего попыток на письмо
RETRY_BASE_DELAY = _env_float("RETRY_BASE_DELAY", 10.0)   # 10 -> 20 -> 40 ... сек
MESSAGE_TTL = _env_float("MESSAGE_TTL", 600.0)            # HIGH/NORMAL: старше N сек не отправляем
MESSAGE_TTL_LOW = _env_float("MESSAGE_TTL_LOW", 3600.0)   # LOW: ждёт дольше, уходит последним
# Доля очереди, которую может занять LOW; остальное зарезервировано для HIGH/NORMAL
LOW_QUEUE_RATIO = _env_float("LOW_QUEUE_RATIO", 0.8)

# Приоритеты -- внутренняя логика, не настройка
PRIORITY_HIGH = 0          # коды подтверждения и т.п.
PRIORITY_NORMAL = 1        # всё остальное
PRIORITY_LOW = 2           # незначительные сообщения, рассылки


def validate_config() -> None:
    if not EMAIL or not EMAIL_PASSWORD:
        raise RuntimeError("EMAIL и EMAIL_PASSWORD должны быть заданы в переменных окружения")
    if SMTP_SECURITY not in ("ssl", "starttls", "plain"):
        raise RuntimeError(f"SMTP_SECURITY должен быть ssl, starttls или plain, получено: {SMTP_SECURITY!r}")
    if MAX_ATTEMPTS < 1:
        raise RuntimeError("MAX_ATTEMPTS должен быть >= 1")
    if MAX_QUEUE < 1:
        raise RuntimeError("MAX_QUEUE должен быть >= 1")
    if not 0.0 < LOW_QUEUE_RATIO <= 1.0:
        raise RuntimeError("LOW_QUEUE_RATIO должен быть в диапазоне (0, 1]")


# ---------- Модель задачи ----------

@dataclass(order=True)
class Job:
    priority: int
    created: float
    address: str = field(compare=False)
    subject: str = field(compare=False)
    text: str = field(compare=False)
    html: str = field(compare=False)
    attempt: int = field(default=0, compare=False)


# ---------- Отправка (синхронная, бросает исключения) ----------

def send_email(address: str, subject: str, text: str, html: str) -> None:
    msg = EmailMessage()
    msg["Subject"] = subject
    msg["From"] = EMAIL_FROM
    msg["To"] = address
    msg.set_content(text)
    msg.add_alternative(html, subtype="html")

    if SMTP_SECURITY == "ssl":
        server = smtplib.SMTP_SSL(SMTP_HOST, SMTP_PORT, timeout=SMTP_TIMEOUT)
    else:
        server = smtplib.SMTP(SMTP_HOST, SMTP_PORT, timeout=SMTP_TIMEOUT)

    with server:
        if SMTP_SECURITY == "starttls":
            server.starttls()
        server.login(EMAIL, EMAIL_PASSWORD)
        server.send_message(msg)


def is_permanent_refusal(e: smtplib.SMTPRecipientsRefused) -> bool:
    """True, если все получатели отвергнуты навсегда (5xx). 4xx -- временная ошибка."""
    return all(500 <= code < 600 for code, _ in e.recipients.values())


# ---------- Очередь, воркер, ретраи ----------

def ttl_for(job: Job) -> float:
    return MESSAGE_TTL_LOW if job.priority == PRIORITY_LOW else MESSAGE_TTL


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
    if time.monotonic() - job.created > ttl_for(job):
        log.warning("expired, dropped: %s (priority=%d)", job.address, job.priority)
        return False

    job.attempt += 1
    try:
        await asyncio.to_thread(
            send_email, job.address, job.subject, job.text, job.html
        )
        log.info("sent to %s", job.address)
    except smtplib.SMTPRecipientsRefused as e:
        # Должен идти раньше SMTPException: это его подкласс
        if is_permanent_refusal(e):
            # Адрес отвергнут навсегда, не ретраим.
            # Пишем полный ответ сервера: 5xx бывает и по политике (спам, репутация).
            log.info("recipient rejected, skipping %s: %s", job.address, e.recipients)
        else:
            schedule_retry(app, job, e.recipients)
    except (smtplib.SMTPException, OSError) as e:
        # Включая SMTPAuthenticationError: бывает временной (сбой/троттлинг провайдера)
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
    validate_config()

    app.state.queue = asyncio.PriorityQueue(maxsize=MAX_QUEUE)
    app.state.retry_tasks = set()
    worker_task = asyncio.create_task(worker(app))
    # Пароль в лог не пишем
    log.info(
        "mail worker started: smtp=%s:%d (%s), from=%s, interval=%.1fs, "
        "attempts=%d, ttl=%.0fs, ttl_low=%.0fs, queue=%d, low_ratio=%.2f",
        SMTP_HOST, SMTP_PORT, SMTP_SECURITY, EMAIL_FROM,
        SEND_INTERVAL, MAX_ATTEMPTS, MESSAGE_TTL, MESSAGE_TTL_LOW,
        MAX_QUEUE, LOW_QUEUE_RATIO,
    )
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
    template: EmailTemplate = EmailTemplate.INFO
    data: dict[str, str] = Field(default_factory=dict)
    priority: int = Field(default=PRIORITY_NORMAL, ge=PRIORITY_HIGH, le=PRIORITY_LOW)


@app.post("/publish", status_code=202)
async def publish(request: PublishRequest):
    queue: asyncio.PriorityQueue = app.state.queue

    # LOW не может занять всю очередь: оставляем запас для HIGH/NORMAL
    if request.priority == PRIORITY_LOW and queue.qsize() >= MAX_QUEUE * LOW_QUEUE_RATIO:
        raise HTTPException(status_code=503, detail="Queue is full for low priority")

    try:
        rendered = render_email(request.template, request.subject, request.data)
    except TemplateDataError as e:
        raise HTTPException(status_code=422, detail=str(e))

    # Предполагаю, что render_email возвращает объект с полями text и html.
    # Если у тебя кортеж или другие имена, поправь эти две строки.
    job = Job(
        priority=request.priority,
        created=time.monotonic(),
        address=request.address,
        subject=request.subject,
        text=rendered.text,
        html=rendered.html,
    )
    try:
        queue.put_nowait(job)
    except asyncio.QueueFull:
        raise HTTPException(status_code=503, detail="Queue is full")

    # Тело письма не логируем: в нём могут быть коды подтверждения
    log.info("queued email to %s (priority=%d)", request.address, request.priority)
    return {"status": "accepted", "address": request.address, "queued": queue.qsize()}


@app.get("/health")
async def health():
    return {"status": "ok", "queued": app.state.queue.qsize()}


if __name__ == "__main__":
    log.info("Starting server initialization...")
    uvicorn.run(
        "EmailNotificationService:app",
        host="0.0.0.0",
        port=5000,
        log_level="INFO",
    )