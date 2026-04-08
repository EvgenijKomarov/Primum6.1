FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Apps/BotCore Apps/BotCore
COPY Libs/CoreConnection Libs/CoreConnection
COPY Libs/CoreDBModel Libs/CoreDBModel
COPY Libs/SignServiceConnection Libs/SignServiceConnection
COPY Libs/ConfigurationServiceClient Libs/ConfigurationServiceClient
COPY Libs/Resourses Libs/Resourses
WORKDIR /src/Apps/BotCore
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish --no-restore -p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5003
EXPOSE 5003
ENTRYPOINT ["dotnet", "BotCore.dll"]
