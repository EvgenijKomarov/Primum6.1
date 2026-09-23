// В контейнере этот файл перезаписывается при старте (docker/40-runtime-config.sh).
// Для npm run dev ссылки берутся из VITE_* в .env.local.
window.__RUNTIME_CONFIG__ = {};
