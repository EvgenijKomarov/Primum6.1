from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
from UserCreate import UserCreate
import os
import logging
import uvicorn
from get_url import load_url, load_host_and_port, load_variable
from motor.motor_asyncio import AsyncIOMotorClient
from pymongo.errors import DuplicateKeyError

MONGO_URI = load_variable("MongoDBUrl")
DB_NAME = "signservice"

logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] [%(name)s] %(message)s"
)

logger = logging.getLogger("sign-service")

# ------------------ Lifespan ------------------
@asynccontextmanager
async def lifespan(app: FastAPI):
    print("Starting service...")
    app.state.mongo_client = AsyncIOMotorClient(MONGO_URI)
    app.state.db = app.state.mongo_client[DB_NAME]
    app.state.users = app.state.db["users"]
    await app.state.users.create_index(
        [("realizationTag", 1), ("chatId", 1)], unique=True
    )
    try:
        app.state.url = load_url("SignService/SelfUrl")
        print(f"Loaded selfUrl (runtime): {app.state.url}")
    except Exception as e:
        print(f"Failed to load config at runtime: {e}")
        app.state.url = None
    yield
    print("Shutting down service...")
    app.state.mongo_client.close()

app = FastAPI(lifespan=lifespan, title="SignService")

# ----------- POST /add -----------
@app.post("/add")
async def add_user(user: UserCreate):
    try:
        await app.state.users.insert_one({
            "userId": user.userId,
            "realizationTag": user.realizationTag,
            "chatId": user.chatId,
            "username": user.username
        })
        return {"status": "ok"}
    except DuplicateKeyError:
        raise HTTPException(status_code=409, detail="Entity already exists")

# ----------- GET /get-userId -----------
@app.get("/get-userId")
async def get_user(realizationTag: str, chatId: int):
    doc = await app.state.users.find_one(
        {"realizationTag": realizationTag, "chatId": chatId},
        {"userId": 1}
    )
    if doc:
        return {"userId": doc["userId"]}
    else:
        return {"userId": None}

# ----------- GET /get-signs/{userId} -----------
@app.get("/get-signs/{userId}")
async def get_by_user(userId: int):
    cursor = app.state.users.find(
        {"userId": userId},
        {"realizationTag": 1, "username": 1, "chatId": 1}
    )
    rows = await cursor.to_list(length=None)
    if not rows:
        return {}
    return [
        {
            "realizationTag": row["realizationTag"],
            "username": row.get("username"),
            "chatId": row["chatId"]
        }
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