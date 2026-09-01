from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
import os
import logging
import uvicorn
from motor.motor_asyncio import AsyncIOMotorClient
from pymongo.errors import DuplicateKeyError
from bson import ObjectId
from bson.errors import InvalidId
from datetime import datetime, timezone

MONGO_URI = os.getenv("MONGO_URI")
DB_NAME = "commonnotifications"

logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] [%(name)s] %(message)s"
)

logger = logging.getLogger("common-notifications")

# ------------------ Lifespan ------------------
@asynccontextmanager
async def lifespan(app: FastAPI):
    print("Starting service...")
    app.state.mongo_client = AsyncIOMotorClient(MONGO_URI)
    app.state.db = app.state.mongo_client[DB_NAME]
    app.state.notifications = app.state.db["notifications"]
    await app.state.notifications.create_index([("userId", 1), ("datetime", -1)])
    yield
    print("Shutting down service...")
    app.state.mongo_client.close()

app = FastAPI(lifespan=lifespan, title="CommonNotificationService")

# ----------- POST /publish -----------
@app.post("/publish")
async def add_notification(userId: int, message: str):
    try:
        await app.state.notifications.insert_one({
            "userId": userId,
            "text": message,
            "seen": False,
            "datetime": datetime.now(timezone.utc)
        })

        # Удаление старых записей сверх лимита 20
        cursor = app.state.notifications.find(
            {"userId": userId}
        ).sort("datetime", -1).skip(20)

        old_ids = [doc["_id"] async for doc in cursor]
        if old_ids:
            await app.state.notifications.delete_many({"_id": {"$in": old_ids}})

        return {"status": "ok"}
    except DuplicateKeyError:
        raise HTTPException(status_code=409, detail="Entity already exists")

# ----------- GET /by-user/{userId} -----------
@app.get("/by-user/{userId}")
async def get_by_user(userId: int):
    cursor = app.state.notifications.find(
        {"userId": userId}
    ).sort([("seen", 1), ("datetime", -1)])

    rows = await cursor.to_list(length=None)
    if not rows:
        return []
    return [
        {
            "id": str(row["_id"]),
            "userId": row["userId"],
            "text": row["text"],
            "seen": row["seen"],
            "datetime": row["datetime"].isoformat().replace("+00:00", "Z")
        }
        for row in rows
    ]

# ----------- POST /set-seen/{notif_id} -----------
@app.post("/set-seen/{notif_id}")
async def set_seen(notif_id: str):
    try:
        oid = ObjectId(notif_id)
    except InvalidId:
        raise HTTPException(status_code=400, detail="Invalid notification id")

    result = await app.state.notifications.update_one(
        {"_id": oid},
        {"$set": {"seen": True}}
    )

    if result.matched_count == 0:
        raise HTTPException(status_code=404, detail="Notification not found")

    return {"status": "ok"}


# ==================== ТОЧКА ВХОДА ====================
if __name__ == "__main__":
    print("Starting server initialization...")

    uvicorn.run(
        "CommonNotificationService:app",
        host="0.0.0.0",
        port=5000,
        log_level="info"
    )