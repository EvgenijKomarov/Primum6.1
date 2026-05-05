import os
from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import uvicorn
import aio_pika
from get_url import load_variable, load_host_and_port

is_prod_mode = os.getenv("MODE", "Development") == "Production"
app = FastAPI(title="FastAPI → RabbitMQ Publisher")

@app.post("/publish")
def publish(userId: int, message: str):
    if is_prod_mode:
        print("!")
    else:
        print(f"send on user {userId} mail with {message}")

if __name__ == "__main__":
    print("Starting server initialization...")
    
    HOST, PORT = load_host_and_port("EmailNotificationService/SelfUrl")
    print(f"Binding to http://{HOST}:{PORT}")
    
    uvicorn.run(
        "EmailNotificationService:app",
        host=HOST,
        port=PORT,
        log_level="info"
    )
