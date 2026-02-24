Param(
    [string]$Root = "$(Get-Location)",
    [bool]$KeepOpen = $true
)

Write-Host "Root: $Root"

$files = Get-ChildItem -Path $Root -Recurse -Include *.cs | Where-Object { $_.FullName -notmatch "\\Tests\\" -and $_.FullName -notmatch "\\.git\\" }

foreach ($file in $files) {
    try {
        $relative = Resolve-Path -Path $file.FullName -Relative
        # make relative path from root
        $relPath = $file.FullName.Substring($Root.Length).TrimStart('\\','/')
        $dir = [System.IO.Path]::GetDirectoryName($relPath)
        if ([string]::IsNullOrEmpty($dir)) {
            # skip files at repository root
            continue
        }
        $expectedNamespace = $dir -replace "[\\/]+","." -replace "^\\.",""

        $content = Get-Content -Raw -Path $file.FullName
        $pattern = '(?m)^(\s*namespace\s+)[\w\.]+'
        if ($content -match $pattern) {
            $newContent = [regex]::Replace($content, $pattern, "`$1$expectedNamespace")
            if ($newContent -ne $content) {
                Write-Host "Updating namespace in $($file.FullName) -> $expectedNamespace"
                Set-Content -Path $file.FullName -Value $newContent -Encoding UTF8
            }
        } else {
            # if no namespace declaration, optionally add one after using directives
            $usingsPattern = '(?s)^(?:using\s+[\w\.\,\s;]+;\s*)+'
            if ($content -match $usingsPattern) {
                $matches = [regex]::Match($content, $usingsPattern)
                $insertPos = $matches.Index + $matches.Length
                $toInsert = "`r`nnamespace $expectedNamespace`r`n{"
                $toAppend = "`r`n}" 
                $body = $content.Substring($insertPos).TrimStart()
                $newContent = $content.Substring(0, $insertPos) + $toInsert + $body + $toAppend
                Write-Host "Adding namespace wrapper in $($file.FullName) -> $expectedNamespace"
                Set-Content -Path $file.FullName -Value $newContent -Encoding UTF8
            }
        }
    } catch {
        Write-Warning "Failed processing $($file.FullName): $_"
    }
}
Write-Host "Done."

if ($KeepOpen) {
    Write-Host ""
    Write-Host "Press Enter to exit..."
    Read-Host | Out-Null
}
