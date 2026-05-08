FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Apps/CoreDBIterator Apps/CoreDBIterator
COPY /Libs/Common Libs/Common
COPY Libs/CoreDBModel Libs/CoreDBModel
COPY Libs/PublishServiceConnection Libs/PublishServiceConnection
COPY Libs/Resourses Libs/Resourses
COPY Libs/ConfigurationServiceClient Libs/ConfigurationServiceClient
COPY Libs/PaymentServiceClient Libs/PaymentServiceClient
WORKDIR /src/Apps/CoreDBIterator
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish --no-restore -p:UseAppHost=false /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "CoreDBIterator.dll"]
