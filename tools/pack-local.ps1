param(
    [string]$Configuration = "Release",
    [string]$Version = "0.1.0-preview.1",
    [string]$Output = ".artifacts/nuget"
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$outputPath = Join-Path $repoRoot $Output

Write-Host "Packing Buzz NuGet packages..."
Write-Host "Configuration: $Configuration"
Write-Host "Version: $Version"
Write-Host "Output: $outputPath"

New-Item -ItemType Directory -Force -Path $outputPath | Out-Null

$projects = @(
    "Buzz.Core/Buzz.Core.csproj",
    "Buzz.Blazor/Buzz.Blazor.csproj",
    "Buzz.Provider.OpenAI/Buzz.Provider.OpenAI.csproj",
    "Buzz.Provider.Ollama/Buzz.Provider.Ollama.csproj"
)

foreach ($project in $projects) {
    $projectPath = Join-Path $repoRoot $project
    Write-Host "Packing $project..."
    dotnet pack $projectPath `
        -c $Configuration `
        -o $outputPath `
        /p:PackageVersion=$Version `
        /p:ContinuousIntegrationBuild=true `
        /p:IncludeSymbols=true `
        /p:SymbolPackageFormat=snupkg
}

Write-Host ""
Write-Host "Done. Packages created in: $outputPath"
