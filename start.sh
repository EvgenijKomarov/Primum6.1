#!/bin/bash

echo "Starting ConfigService..."
cd Apps/ConfigService
python ConfigService.py &

sleep 3

echo "Starting BotCore..."
cd ../BotCore
dotnet run &

sleep 5

echo "Starting TelegramBot..."
cd ../TelegramBot
dotnet run
