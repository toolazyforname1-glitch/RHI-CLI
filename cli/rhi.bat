@echo off
setlocal
rem rhi.bat - terminal control for RHI.
rem Resolves RHI.exe, then forwards every argument to it in terminal mode (--cli).
rem Override the executable with: set RHI_CLI_EXE=C:\path\to\RHI.exe

set "RHI_EXE=%RHI_CLI_EXE%"

if not defined RHI_EXE if exist "%~dp0RHI.exe" set "RHI_EXE=%~dp0RHI.exe"
if not defined RHI_EXE if exist "%~dp0..\RHI\RenoDXCommander\bin\x64\Release\net8.0-windows10.0.19041.0\RHI.exe" set "RHI_EXE=%~dp0..\RHI\RenoDXCommander\bin\x64\Release\net8.0-windows10.0.19041.0\RHI.exe"
if not defined RHI_EXE if exist "%~dp0..\RHI\RenoDXCommander\bin\x64\Debug\net8.0-windows10.0.19041.0\RHI.exe" set "RHI_EXE=%~dp0..\RHI\RenoDXCommander\bin\x64\Debug\net8.0-windows10.0.19041.0\RHI.exe"
if not defined RHI_EXE if exist "%LOCALAPPDATA%\Programs\RHI\RHI.exe" set "RHI_EXE=%LOCALAPPDATA%\Programs\RHI\RHI.exe"
if not defined RHI_EXE if exist "%ProgramFiles%\RHI\RHI.exe" set "RHI_EXE=%ProgramFiles%\RHI\RHI.exe"

if not defined RHI_EXE (
  echo rhi: RHI.exe not found.
  echo      Set RHI_CLI_EXE to its full path, e.g.:
  echo      set RHI_CLI_EXE=C:\path\to\RHI.exe
  exit /b 2
)

rem cmd does not wait for GUI-subsystem executables unless one of their handles is redirected;
rem `< nul` forces the wait, so both the output and the exit code reach the caller.
"%RHI_EXE%" --cli %* < nul
exit /b %ERRORLEVEL%

