import json
import os
import uvicorn
from fastapi import FastAPI, HTTPException
from typing import Any

app = FastAPI(title="Config Service")

ENV = os.getenv("ENVIRONMENT", "Development")
CONFIG_FILE = "config.Production.json" if ENV == "Production" else "config.json"


# загрузка конфига каждый раз (hot reload)
def load_config() -> dict:
    if not os.path.exists(CONFIG_FILE):
        return {}

    with open(CONFIG_FILE, "r", encoding="utf-8") as f:
        return json.load(f)


# получение значения по пути services/user-service/url
def get_value_by_path(config: dict, path: str) -> Any:
    keys = path.split("/")

    value = config

    for key in keys:
        if key not in value:
            raise KeyError(path)

        value = value[key]

    return value


# endpoint получить значение
@app.get("/config/{full_path:path}")
def get_config(full_path: str):

    config = load_config()

    try:
        value = get_value_by_path(config, full_path)

        return value

    except KeyError:

        raise HTTPException(
            status_code=404,
            detail="Key not found"
        )


# endpoint получить весь config
@app.get("/config")
def get_all_config():

    return load_config()


# health check
@app.get("/health")
def health():
    return {"status": "ok"}

if __name__ == "__main__":
    host = "localhost"
    port = 5000
    print(f"-> Starting server on http://{host}:{port} in {ENV} mode")

    uvicorn.run(
        app,
        host=host,
        port=port,
        log_level="info"
    )