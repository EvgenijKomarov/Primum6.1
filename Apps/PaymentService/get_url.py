from urllib.parse import urlparse
import requests
import os

CONFIG_URL=os.getenv("CONFIG_SERVICE_URL", "http://127.0.0.1:5000")

def parse_url_to_host_port(url: str) -> tuple[str, int]:
    """
    Парсит URL и возвращает кортеж (host, port).
    Пример: "http://192.168.1.10:8080" → ("192.168.1.10", 8080)
    """
    parsed = urlparse(url)
    host = parsed.hostname
    port = parsed.port
    return host, port

def load_url(route: str) -> tuple[str, int]:
    """
    Загружает конфигурацию и возвращает URL.
    """
    url = f"{CONFIG_URL}/routes/{route}"
    
    response = requests.get(url, timeout=20.0)
    response.raise_for_status()  # выбросит исключение при 4xx/5xx
    
    return response.json()

def load_variable(name: str) -> str:
    url = f"{CONFIG_URL}/variable/{name}"
    
    response = requests.get(url, timeout=20.0)
    response.raise_for_status()  # выбросит исключение при 4xx/5xx
    
    return response.json()

def load_host_and_port(route: str) -> tuple[str, int]:
    """
    Получает URL из конфигурации и возвращает (host, port).
    """
    return parse_url_to_host_port(load_url(route))