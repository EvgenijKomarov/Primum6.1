@echo off
chcp 65001 >nul
setlocal

echo ============================================
echo   Database Migration Tool
echo ============================================
echo.

:: Extract PowerShell code from this batch file and execute it
set PS_SCRIPT=%TEMP%\apply_migrations_%RANDOM%.ps1

powershell -NoProfile -Command ^
    "$content = Get-Content '%~f0' -Raw -Encoding UTF8; " ^
    "$marker = '::' + 'POWERSHELL_MARKER'; " ^
    "$idx = $content.IndexOf($marker); " ^
    "if ($idx -ge 0) { " ^
    "    $psCode = $content.Substring($idx + $marker.Length).TrimStart([char]13, [char]10); " ^
    "    $psCode | Set-Content '%PS_SCRIPT%' -Encoding UTF8 " ^
    "} else { " ^
    "    Write-Host 'ERROR: PowerShell marker not found!' -ForegroundColor Red; " ^
    "    exit 1 " ^
    "}"

if not exist "%PS_SCRIPT%" (
    echo [ERROR] Failed to extract PowerShell script.
    pause
    exit /b 1
)

:: Execute the extracted PowerShell script
powershell -NoProfile -ExecutionPolicy Bypass -File "%PS_SCRIPT%"
set RESULT=%ERRORLEVEL%

:: Cleanup
del "%PS_SCRIPT%" 2>nul

echo.
pause
endlocal
exit /b %RESULT%

::POWERSHELL_MARKER
# ============================================
# PowerShell Migration Script
# ============================================

# --- SETTINGS ---
$configUrl = 'http://localhost:5000/variable/CoreDatabaseConnection'
# If migrations are in a specific project folder, set the path here (leave empty for current directory)
$projectDir = ''
# ----------------

# Step 1: Retrieve connection string
Write-Host "[1/3] Retrieving connection string from config service..." -ForegroundColor Cyan
Write-Host "URL: $configUrl"

try {
    $response = Invoke-WebRequest -Uri $configUrl -UseBasicParsing
    $connStr = $response.Content.Trim().Trim('"')
    
    # Try to parse as JSON and extract the value
    try {
        $json = $connStr | ConvertFrom-Json
        if ($json.value) {
            $connStr = $json.value
        }
        elseif ($json.CoreDatabaseConnection) {
            $connStr = $json.CoreDatabaseConnection
        }
    }
    catch {
        # Response is not JSON, use as-is
    }
}
catch {
    Write-Host "[ERROR] Failed to retrieve connection string: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Please check if the config service is running and accessible." -ForegroundColor Yellow
    exit 1
}

if ([string]::IsNullOrWhiteSpace($connStr)) {
    Write-Host "[ERROR] Connection string is empty!" -ForegroundColor Red
    exit 1
}

Write-Host "[OK] Connection string retrieved successfully." -ForegroundColor Green
Write-Host ""

# Step 2: Navigate to project directory if specified
if (-not [string]::IsNullOrWhiteSpace($projectDir)) {
    Write-Host "[2/3] Navigating to project directory: $projectDir" -ForegroundColor Cyan
    try {
        Set-Location $projectDir
    }
    catch {
        Write-Host "[ERROR] Could not navigate to project directory: $($_.Exception.Message)" -ForegroundColor Red
        exit 1
    }
}
else {
    Write-Host "[2/3] Working directory: $(Get-Location)" -ForegroundColor Cyan
}
Write-Host ""

# Step 3: Apply migrations
Write-Host "[3/3] Applying database migrations..." -ForegroundColor Cyan
Write-Host ""

& dotnet ef database update --connection $connStr
$exitCode = $LASTEXITCODE

Write-Host ""
if ($exitCode -eq 0) {
    Write-Host "============================================" -ForegroundColor Green
    Write-Host "  [SUCCESS] Migrations applied successfully!" -ForegroundColor Green
    Write-Host "============================================" -ForegroundColor Green
}
else {
    Write-Host "============================================" -ForegroundColor Red
    Write-Host "  [ERROR] Migration failed! Exit code: $exitCode" -ForegroundColor Red
    Write-Host "============================================" -ForegroundColor Red
}

exit $exitCode