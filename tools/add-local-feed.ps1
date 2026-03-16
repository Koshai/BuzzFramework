param(
    [string]$FeedPath = ".artifacts/nuget",
    [string]$SourceName = "BuzzLocal"
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$resolvedFeedPath = Join-Path $repoRoot $FeedPath

New-Item -ItemType Directory -Force -Path $resolvedFeedPath | Out-Null

$existing = dotnet nuget list source | Select-String -Pattern $SourceName -SimpleMatch
if ($existing) {
    Write-Host "NuGet source '$SourceName' already exists. Updating path..."
    dotnet nuget update source $SourceName --source $resolvedFeedPath
}
else {
    Write-Host "Adding NuGet source '$SourceName'..."
    dotnet nuget add source $resolvedFeedPath --name $SourceName
}

Write-Host "NuGet source ready:"
Write-Host "  Name: $SourceName"
Write-Host "  Path: $resolvedFeedPath"
