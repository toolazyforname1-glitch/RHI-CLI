@echo off
call "%~dp0..\rhi.bat" doctor %*
exit /b %ERRORLEVEL%
