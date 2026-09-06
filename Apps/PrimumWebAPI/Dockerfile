FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Apps/PrimumWebAPI Apps/PrimumWebAPI
COPY Libs/CoreConnection Libs/CoreConnection
COPY Libs/CoreDBModel Libs/CoreDBModel
COPY Libs/SignServiceConnection Libs/SignServiceConnection
COPY Libs/PaymentServiceClient Libs/PaymentServiceClient
COPY Libs/CommonNotificationServiceClient Libs/CommonNotificationServiceClient
WORKDIR /src/Apps/PrimumWebAPI
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish --no-restore -p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "PrimumWebAPI.dll"]
