@echo off

IF EXIST .env (
    echo .env найден, продолжаем...
) ELSE (
    IF EXIST .env.example (
        echo .env не найден. Копируем .env.example в .env...
        COPY .env.example .env
        echo .env создан!
    ) ELSE (
        echo ОШИБКА: Не найдены ни .env, ни .env.example!
        pause
        exit /b 1
    )
)

docker-compose up --build
pause