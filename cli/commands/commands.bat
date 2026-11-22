@echo off
call "%~dp0..\rhi.bat" commands %*
exit /b %ERRORLEVEL%
