@echo off
setlocal

set FEED_PATH=.artifacts\nuget
set SOURCE_NAME=BuzzLocal

if not "%~1"=="" set FEED_PATH=%~1
if not "%~2"=="" set SOURCE_NAME=%~2

powershell -ExecutionPolicy Bypass -File "%~dp0add-local-feed.ps1" -FeedPath "%FEED_PATH%" -SourceName "%SOURCE_NAME%"

endlocal
