param(
    [string]$SpecUrl = "https://localhost:5002/swagger/v1/swagger.json",
    [string]$OutputDir = "lib/api",
    [string]$Generator = "dart-dio",
    [string]$PackageName = "api_client"
)

Write-Host "Generating API client from ${SpecUrl} -> $OutputDir using generator $Generator"

$cwd = Get-Location
$tmpDir = Join-Path $cwd '.openapi'
if (-Not (Test-Path $tmpDir)) { New-Item -ItemType Directory -Path $tmpDir | Out-Null }
$specFile = Join-Path $tmpDir 'swagger.json'

try {
    Write-Host "Downloading spec..."
    Invoke-WebRequest -Uri $SpecUrl -OutFile $specFile -UseBasicParsing -ErrorAction Stop
    Write-Host "Spec saved to $specFile"
}
catch {
    Write-Error "Failed to download spec from ${SpecUrl}: ${_}"
    exit 1
}

# Try openapi-generator-cli from PATH
Write-Host "Looking for openapi-generator-cli in PATH..."
try {
    $null = & openapi-generator-cli version 2>$null
    Write-Host "Found openapi-generator-cli locally. Running generator..."
    openapi-generator-cli generate -i $specFile -g $Generator -o $OutputDir -p "pubName=$PackageName"
    Write-Host "Generation completed. Output: $OutputDir"
    exit 0
}
catch {
    Write-Host "openapi-generator-cli not found locally. Trying Docker fallback..."
}

# Docker fallback
try {
    $pwdPath = $cwd.Path -replace "\\","/"
    $dockerCmd = "docker run --rm -v ${pwdPath}:/local openapitools/openapi-generator-cli generate -i /local/.openapi/swagger.json -g $Generator -o /local/$OutputDir -p pubName=$PackageName"
    Write-Host "Running: $dockerCmd"
    iex $dockerCmd
    Write-Host "Generation completed via Docker. Output: $OutputDir"
    exit 0
}
catch {
    Write-Error "Docker generation failed or Docker not available: $_"
    Write-Host "Manual steps: install openapi-generator-cli (npm i -g @openapitools/openapi-generator-cli) or use Docker, then run this script again."
    exit 2
}
