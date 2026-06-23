#!/bin/sh
set -e

CONFIG_URL="http://configservice:5000/variable/CoreDatabaseConnection"

echo "Получение строки подключения из ${CONFIG_URL}..."

MAX_RETRIES=10
RETRY_DELAY=3
CONN=""

for i in $(seq 1 $MAX_RETRIES); do
    RESPONSE=$(curl -sf "$CONFIG_URL" || true)
    if [ -n "$RESPONSE" ]; then
        # Ответ - JSON-строка вида "Host=...;...", убираем jq и парсим через него же,
        # чтобы корректно обработать экранирование
        CONN=$(echo "$RESPONSE" | jq -r '.')
        if [ -n "$CONN" ] && [ "$CONN" != "null" ]; then
            break
        fi
    fi
    echo "Попытка $i/$MAX_RETRIES не удалась, повтор через ${RETRY_DELAY}с..."
    sleep $RETRY_DELAY
done

if [ -z "$CONN" ] || [ "$CONN" = "null" ]; then
    echo "ОШИБКА: не удалось получить строку подключения из ${CONFIG_URL}"
    exit 1
fi

echo "Строка подключения получена, запуск миграций..."

./efbundle --connection "$CONN"

echo "Миграции успешно применены."