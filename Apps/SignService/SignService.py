from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
from UserCreate import UserCreate
from urllib.parse import urlparse
import sqlite3
import os
import httpx
import logging
import uvicorn
import asyncio
import time

DB_NAME = "data.db"

logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] [%(name)s] %(message)s"
)

logger = logging.getLogger("sign-service")

# ----------- Инициализация БД -----------
def init_db():
    conn = sqlite3.connect(DB_NAME)
    cursor = conn.cursor()
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS users (
            userId INTEGER NOT NULL,
            realizationTag TEXT NOT NULL,
            chatId INTEGER NOT NULL,
            username TEXT,
            PRIMARY KEY (realizationTag, chatId)
        )
    """)
    conn.commit()
    conn.close()

# ------------------ Парсинг URL ------------------
def parse_url_to_host_port(url: str) -> tuple[str, int]:
    """
    Парсит URL и возвращает кортеж (host, port).
    Пример: "http://192.168.1.10:8080" → ("192.168.1.10", 8080)
    """
    parsed = urlparse(url)
    host = parsed.hostname
    port = parsed.port
    return host, port

# ------------------ Загрузка конфигурации ------------------
async def load_self_url():
    config_base_url = os.getenv("CONFIG_URL", "http://127.0.0.1:5000")#127.0.0.1 вместо localhost из-за разных версий IPv6
    url = f"{config_base_url}/config/SignService/SelfUrl"
    time.sleep(5)
    async with httpx.AsyncClient() as client:
        response = await client.get(url, timeout=20.0)
        response.raise_for_status()
        return response.json()  # ожидаем строку вида "http://host:port"

def load_self_url_sync() -> str | None:
    try:
        result = asyncio.run(load_self_url())
        return str(result).strip()
    except Exception as e:
        logger.warning(f"Не удалось загрузить SelfUrl из конфига: {e}")
        return None

# ------------------ Lifespan ------------------
@asynccontextmanager
async def lifespan(app: FastAPI):
    print("Starting service...")
    init_db()
    try:
        self_url = await load_self_url()
        app.state.self_url = self_url
        print(f"Loaded selfUrl (runtime): {self_url}")
    except Exception as e:
        print(f"Failed to load config at runtime: {e}")
        app.state.self_url = None
    yield
    print("Shutting down service...")

app = FastAPI(lifespan=lifespan, title="SignService")

# ----------- POST /add -----------
@app.post("/add")
def add_user(user: UserCreate):
    try:
        conn = sqlite3.connect(DB_NAME)
        cursor = conn.cursor()
        cursor.execute("""
            INSERT INTO users (userId, realizationTag, chatId, username)
            VALUES (?, ?, ?, ?)
        """, (user.userId, user.realizationTag, user.chatId, user.username))
        conn.commit()
        conn.close()
        return {"status": "ok"}
    except sqlite3.IntegrityError:
        raise HTTPException(status_code=409, detail="Entity already exists")

# ----------- GET /get-userId -----------
@app.get("/get-userId")
def get_user(realizationTag: str, chatId: int):
    conn = sqlite3.connect(DB_NAME)
    cursor = conn.cursor()
    cursor.execute("""
        SELECT userId FROM users
        WHERE realizationTag = ? AND chatId = ?
    """, (realizationTag, chatId))
    result = cursor.fetchone()
    conn.close()
    if result:
        return {"userId": result[0]}
    else:
        return {"userId": None}

# ----------- GET /get-signs/{userId} -----------
@app.get("/get-signs/{userId}")
def get_by_user(userId: int):
    conn = sqlite3.connect(DB_NAME)
    cursor = conn.cursor()
    cursor.execute("""
        SELECT realizationTag, username, chatId
        FROM users
        WHERE userId = ?
    """, (userId,))
    rows = cursor.fetchall()
    conn.close()
    if not rows:
        return {}
    return [
        {"realizationTag": row[0], "username": row[1], "chatId": row[2]} 
        for row in rows
    ]


# ==================== ТОЧКА ВХОДА ====================
if __name__ == "__main__":
    print("Starting server initialization...")
    
    self_url = load_self_url_sync()
    
    # 3️⃣ Парсим URL на host и port
    HOST, PORT = parse_url_to_host_port(self_url)
    print(f"Binding to http://{HOST}:{PORT}")
    
    uvicorn.run(
        "SignService:app",  # замените "main" на имя вашего файла без .py
        host=HOST,
        port=PORT,
        log_level="info",
        reload=os.getenv("RELOAD_MODE", "false").lower() == "true"
    )