# ---- Build stage: создаём efbundle ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore CoreDBModel.csproj

# Копируем только нужные csproj для restore (можно расширить при необходимости)
COPY . .

RUN dotnet tool install --global dotnet-ef --version 10.0.0 \
    && export PATH="$PATH:/root/.dotnet/tools" \
    && dotnet ef migrations bundle \
        --self-contained \
        -r linux-x64 \
        -o /app/efbundle \
        --project CoreDBModel.csproj

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/runtime-deps:10.0
WORKDIR /app

# curl нужен для запроса к configservice
RUN apt-get update && apt-get install -y curl jq && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/efbundle ./efbundle
COPY entrypoint.sh ./entrypoint.sh
RUN sed -i 's/\r$//' ./entrypoint.sh && chmod +x ./efbundle ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]