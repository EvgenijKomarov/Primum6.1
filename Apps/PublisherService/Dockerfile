FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Apps/PublisherService Apps/PublisherService
COPY Libs/CoreConnection Libs/CoreConnection
COPY Libs/CoreDBModel Libs/CoreDBModel
COPY Libs/PublishServiceConnection Libs/PublishServiceConnection
COPY Libs/Resourses Libs/Resourses
COPY Libs/ConfigurationServiceClient Libs/ConfigurationServiceClient
COPY Libs/SignServiceConnection Libs/SignServiceConnection
COPY Libs/Common Libs/Common
WORKDIR /src/Apps/PublisherService
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish --no-restore -p:UseAppHost=false /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5004
EXPOSE 5004
ENTRYPOINT ["dotnet", "PublisherService.dll"]
