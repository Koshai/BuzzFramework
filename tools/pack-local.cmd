@echo off
setlocal

set CONFIGURATION=Release
set VERSION=0.1.0-preview.1
set OUTPUT=.artifacts\nuget

if not "%~1"=="" set CONFIGURATION=%~1
if not "%~2"=="" set VERSION=%~2
if not "%~3"=="" set OUTPUT=%~3

powershell -ExecutionPolicy Bypass -File "%~dp0pack-local.ps1" -Configuration "%CONFIGURATION%" -Version "%VERSION%" -Output "%OUTPUT%"

endlocal
