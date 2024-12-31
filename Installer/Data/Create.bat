@echo off
setlocal enabledelayedexpansion

rem Kiểm tra phiên bản SQL Server
sqlcmd -E -S (local) -Q "SELECT @@VERSION" > version.txt

rem Đọc phiên bản từ file
set "version="
for /f "usebackq delims=" %%a in ("version.txt") do (
    set "version=%%a"
    goto :check_version
)

:check_version
echo SQL Server version detected: !version!

rem Thực thi script Data.sql
echo Executing Data.sql on the detected SQL Server...
sqlcmd -E -S (local) -i Data.sql

pause
