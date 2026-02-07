# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project files
COPY ["PrimumCore/PrimumCore.csproj", "PrimumCore/"]
COPY ["DTO/CoreConnection.csproj", "DTO/"]

# Restore dependencies
RUN dotnet restore "PrimumCore/PrimumCore.csproj"

# Copy source code
COPY . .

# Build the application
RUN dotnet build "PrimumCore/PrimumCore.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "PrimumCore/PrimumCore.csproj" -c Release -o /app/publish


# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

# Copy published application from build stage
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080
EXPOSE 8443

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080;https://+:8443
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=40s --retries=3 \
    CMD dotnet /app/PrimumCore.dll --health || exit 1

# Entry point
ENTRYPOINT ["dotnet", "PrimumCore.dll"]
