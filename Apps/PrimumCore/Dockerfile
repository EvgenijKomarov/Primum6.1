FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy csproj files (for restore caching)
COPY ["Apps/PrimumCore/PrimumCore.csproj", "Apps/PrimumCore/"]
COPY ["Libs/CoreConnection/CoreConnection.csproj", "Libs/CoreConnection/"]

# Restore dependencies
RUN dotnet restore "Apps/PrimumCore/PrimumCore.csproj"

# Copy full source
COPY . .

# Move to project folder
WORKDIR /src/Apps/PrimumCore

# Publish app
RUN dotnet publish "PrimumCore.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


# =========================
# Stage 2 — Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

# Copy published files
COPY --from=build /app/publish .

# Expose ports
EXPOSE 8080
EXPOSE 8443

# Environment
ENV ASPNETCORE_URLS=http://+:8080;https://+:8443
ENV ASPNETCORE_ENVIRONMENT=Production

# Healthcheck
HEALTHCHECK --interval=30s --timeout=10s --start-period=40s --retries=3 \
    CMD dotnet PrimumCore.dll --health || exit 1

# Run app
ENTRYPOINT ["dotnet", "PrimumCore.dll"]