#!/bin/sh
set -e

if [ -z "$COREDB_URL" ]; then
    echo "ОШИБКА: переменная окружения COREDB_URL не задана"
    exit 1
fi

echo "Строка подключения получена из переменной окружения, запуск миграций..."
./efbundle --connection "$COREDB_URL"
echo "Миграции успешно применены."