FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Apps/PrimumCore Apps/PrimumCore
COPY Libs/CoreConnection Libs/CoreConnection
COPY Libs/Common Libs/Common
COPY Libs/CoreDBModel Libs/CoreDBModel
COPY Libs/PublishServiceConnection Libs/PublishServiceConnection
COPY Libs/SignServiceConnection Libs/SignServiceConnection
COPY Libs/ConfigurationServiceClient Libs/ConfigurationServiceClient
COPY Libs/Resourses Libs/Resourses
WORKDIR /src/Apps/PrimumCore
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish --no-restore -p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5001
EXPOSE 5001
ENTRYPOINT ["dotnet", "PrimumCore.dll"]
