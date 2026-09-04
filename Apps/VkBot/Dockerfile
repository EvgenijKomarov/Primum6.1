FROM python:3.11-slim

WORKDIR /app

COPY Libs/BotConnection Libs/BotConnection
RUN pip install --no-cache-dir Libs/BotConnection

COPY Apps/VkBot Apps/VkBot

WORKDIR /app/Apps/VkBot

RUN pip install --no-cache-dir -r requirements.txt
CMD ["python", "VkBot.py"]