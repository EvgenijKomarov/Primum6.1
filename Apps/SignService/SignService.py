from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
from UserCreate import UserCreate
import sqlite3
import os
import logging
import uvicorn
from get_url import load_url, load_host_and_port
import asyncio

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

# ------------------ Lifespan ------------------
@asynccontextmanager
async def lifespan(app: FastAPI):
    print("Starting service...")
    init_db()
    try:
        app.state.url = load_url("SignService/SelfUrl")
        print(f"Loaded selfUrl (runtime): {app.state.url}")
    except Exception as e:
        print(f"Failed to load config at runtime: {e}")
        app.state.url = None
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
    
    HOST, PORT = load_host_and_port("SignService/SelfUrl")
    print(f"Binding to http://{HOST}:{PORT}")
    
    uvicorn.run(
        "SignService:app",
        host=HOST,
        port=PORT,
        log_level="info",
        reload=os.getenv("RELOAD_MODE", "false").lower() == "true"
    )