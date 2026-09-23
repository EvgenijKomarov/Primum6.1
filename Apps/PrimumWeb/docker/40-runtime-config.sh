#!/bin/sh
# Запускается образом nginx при старте контейнера (/docker-entrypoint.d).
# Пишет ссылки на ботов из переменных окружения контейнера в runtime-config.js:
# Vite вшивает import.meta.env на этапе сборки, а образ собирается в CI без этих значений.
set -eu

# Экранирование для строки в двойных кавычках JS: обратный слеш и кавычка
escape() { printf '%s' "$1" | sed -e 's/[\\"]/\\&/g'; }

cat > /usr/share/nginx/html/runtime-config.js <<CONFIG
window.__RUNTIME_CONFIG__ = {
  telegramUrl: "$(escape "${VITE_TELEGRAM_URL:-}")",
  maxUrl: "$(escape "${VITE_MAX_URL:-}")",
  vkUrl: "$(escape "${VITE_VK_URL:-}")"
};
CONFIG
