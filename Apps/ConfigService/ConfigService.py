import json
import os
import uvicorn
from fastapi import FastAPI, HTTPException
from typing import Any

app = FastAPI(title="Config Service")

is_prod_mode = os.getenv("MODE", "Development") == "Production"
ROUTES_FILE = "routes.Production.json" if is_prod_mode else "routes.json"
DEFAULT_VARIABLES_FILE = "defaultVariables.json"

_HOST = os.getenv("HOST", "localhost")
_PORT = int(os.getenv("PORT", 5000))

if is_prod_mode:
    VARIABLES = {
        "CoreDatabaseConnection": f"Server={os.environ.get('DB_HOST')},{os.environ.get('DB_PORT')};User Id=sa;Password={os.environ.get('DB_PASSWORD')};TrustServerCertificate=True;",
        "RabbitMQConnection": f"amqp://{os.environ.get('RABBITMQ_USER')}:{os.environ.get('RABBITMQ_PASSWORD')}@{os.environ.get('RABBITMQ_HOST')}:{os.environ.get('RABBITMQ_PORT')}/",
        "GatewayUrl": os.environ.get('GATEWAY_URL')
    }
else:
    with open(DEFAULT_VARIABLES_FILE, "r", encoding="utf-8") as f:
        VARIABLES = json.load(f)

# загрузка конфига каждый раз (hot reload)
def load_routes() -> dict:
    if not os.path.exists(ROUTES_FILE):
        return {}

    with open(ROUTES_FILE, "r", encoding="utf-8") as f:
        return json.load(f)


# получение значения по пути services/user-service/url
def get_value_by_path(routes_config: dict, path: str) -> Any:
    keys = path.split("/")

    value = routes_config

    for key in keys:
        if key not in value:
            raise KeyError(path)

        value = value[key]

    return value

# endpoint получить весь routes_config
@app.get("/routes")
def get_all_routes():

    return load_routes()

# endpoint получить значение
@app.get("/routes/{full_path:path}")
def get_routes(full_path: str):

    config = load_routes()

    try:
        value = get_value_by_path(config, full_path)

        return value

    except KeyError:

        raise HTTPException(
            status_code=404,
            detail="Key not found"
        )

@app.get("/variable/{variable:path}")
def get_variable(variable: str):

    config = load_routes()

    try:
        value = VARIABLES[variable]

        return value

    except KeyError:

        raise HTTPException(
            status_code=404,
            detail="Key not found"
        )


# health check
@app.get("/health")
def health():
    return {"status": "ok"}

if __name__ == "__main__":
    host = _HOST
    port = _PORT
    print(f"-> Starting server on http://{host}:{port}")

    uvicorn.run(
        app,
        host=host,
        port=port,
        log_level="info"
    )