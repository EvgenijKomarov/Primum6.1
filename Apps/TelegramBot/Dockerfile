FROM python:3.11-slim

WORKDIR /app

COPY Libs/BotConnection Libs/BotConnection
RUN pip install --no-cache-dir Libs/BotConnection

COPY Apps/TelegramBot Apps/TelegramBot

WORKDIR /app/Apps/TelegramBot

RUN pip install --no-cache-dir -r requirements.txt
CMD ["python", "TelegramBot.py"]