# Docker развёртывание PrimumCore

## Требования

- Docker (версия 20.10+)
- Docker Compose (версия 2.0+)

## Быстрый старт

### 1. Подготовка переменных окружения

```bash
cp .env.example .env
```

Отредактируйте `.env` файл и установите безопасный пароль для SQL Server:

```env
SA_PASSWORD=YourSafePassword123!@
```

### 2. Запуск с Docker Compose

```bash
docker-compose up -d
```

Приложение будет доступно по адресу:
- **HTTP**: http://localhost:8080
- **HTTPS**: https://localhost:8443
- **RabbitMQ Management**: http://localhost:15672 (guest/guest)
- **SQL Server**: localhost:1433

### 3. Проверка статуса контейнеров

```bash
docker-compose ps
```

### 4. Просмотр логов

```bash
# Все сервисы
docker-compose logs -f

# Конкретный сервис
docker-compose logs -f primumcore
docker-compose logs -f sqlserver
docker-compose logs -f rabbitmq
```

## Сборка только приложения

### Локальная сборка (без базы данных)

```bash
docker build -t primumcore:latest .
```

### Запуск отдельного контейнера

```bash
docker run -d \
  -p 8080:8080 \
  -p 8443:8443 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  --name primumcore-app \
  primumcore:latest
```

## Управление

### Остановка всех сервисов

```bash
docker-compose down
```

### Полная очистка (включая объёмы данных)

```bash
docker-compose down -v
```

### Пересоздание образов

```bash
docker-compose up -d --build
```

### Запуск миграций базы данных

После запуска контейнеров можно выполнить миграции:

```bash
docker-compose exec primumcore dotnet ef database update
```

## Переменные окружения

| Переменная | По умолчанию | Описание |
|-----------|---|---|
| `ASPNETCORE_ENVIRONMENT` | Production | Окружение приложения |
| `SA_PASSWORD` | *не установлен* | Пароль SQL Server SA |
| `ConnectionStrings__DefaultConnection` | *сгенерирована автоматически* | Строка подключения БД |

## Тестирование

### Проверка здоровья контейнера

```bash
docker-compose exec primumcore curl http://localhost:8080/health
```

### Проверка подключения к БД

```bash
docker-compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD
```

## Решение проблем

### Контейнер приложения не запускается

1. Проверьте логи: `docker-compose logs primumcore`
2. Убедитесь, что порты 8080, 8443 свободны
3. Проверьте переменные окружения в `.env`

### Проблема с подключением к БД

1. Убедитесь, что контейнер SQL Server полностью запустился
2. Проверьте пароль SA_PASSWORD в `.env`
3. Подождите 30-40 секунд для инициализации БД

### RabbitMQ недоступен

1. Проверьте статус контейнера: `docker-compose ps rabbitmq`
2. Проверьте логи: `docker-compose logs rabbitmq`

## Production развёртывание

Для production окружения рекомендуется:

1. Использовать управляемые сервисы (Azure SQL Database, Azure Service Bus вместо RabbitMQ)
2. Добавить HTTPS сертификаты
3. Использовать Kubernetes для оркестрации
4. Использовать Azure Container Registry для хранения образов
5. Добавить мониторинг и логирование через Application Insights

## Docker-compose рекомендации для production

- Используйте внешнее хранилище данных вместо локальных томов
- Настройте ограничения ресурсов для контейнеров
- Добавьте reverse proxy (Nginx/Traefik)
- Используйте secrets для чувствительных данных
- Включите автоматический restart политики
