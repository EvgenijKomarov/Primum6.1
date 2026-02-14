@echo off

echo ==========================================
echo   Updating API Client via NSwag
echo ==========================================

set PROJECT_DIR=%~dp0
cd /d "%PROJECT_DIR%"

set NSwagPath=%USERPROFILE%\.dotnet\tools\nswag.exe

if not exist "%NSwagPath%" (
    echo NSwag not found at %NSwagPath%
    echo Make sure NSwag.ConsoleCore is installed.
    pause
    exit /b 1
)

"%NSwagPath%" run nswag.json

if %ERRORLEVEL% neq 0 (
    echo.
    echo ERROR: Client generation failed.
    pause
    exit /b %ERRORLEVEL%
)

echo.
echo Client successfully updated.
pause