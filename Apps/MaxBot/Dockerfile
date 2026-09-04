FROM python:3.11-slim

WORKDIR /app

COPY Libs/BotConnection Libs/BotConnection
RUN pip install --no-cache-dir Libs/BotConnection

COPY Apps/MaxBot Apps/MaxBot

WORKDIR /app/Apps/MaxBot

RUN pip install --no-cache-dir -r requirements.txt
CMD ["python", "MaxBot.py"]