FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Apps/PrimumWebAPI Apps/PrimumWebAPI
COPY Libs/CoreConnection Libs/CoreConnection
COPY Libs/CoreDBModel Libs/CoreDBModel
COPY Libs/SignServiceConnection Libs/SignServiceConnection
COPY Libs/ConfigurationServiceClient Libs/ConfigurationServiceClient
WORKDIR /src/Apps/PrimumWebAPI
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish --no-restore -p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5002
EXPOSE 5002
ENTRYPOINT ["dotnet", "PrimumWebAPI.dll"]
