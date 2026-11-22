@echo off
rem Finds vcvarsall.bat and returns it in VCVARS_BAT.
rem Order: %VCVARS% override -> vswhere (any VS with the C++ toolset) -> common install paths.
rem Returns 1 with a message when nothing is found.
setlocal
set "PF86=%ProgramFiles(x86)%"
set "PF64=%ProgramFiles%"
set "VCVARS_BAT="

if defined VCVARS set "VCVARS_BAT=%VCVARS%"
if defined VCVARS_BAT goto :done

set "VSWHERE=%PF86%\Microsoft Visual Studio\Installer\vswhere.exe"
if not exist "%VSWHERE%" set "VSWHERE=%PF64%\Microsoft Visual Studio\Installer\vswhere.exe"
if exist "%VSWHERE%" for /f "usebackq delims=" %%i in (`"%VSWHERE%" -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath`) do set "VCVARS_BAT=%%i\VC\Auxiliary\Build\vcvarsall.bat"
if defined VCVARS_BAT goto :done

for %%d in (
  "%PF86%\Microsoft Visual Studio\2022\BuildTools"
  "%PF86%\Microsoft Visual Studio\2022\Community"
  "%PF86%\Microsoft Visual Studio\2022\Professional"
  "%PF86%\Microsoft Visual Studio\2022\Enterprise"
  "%PF64%\Microsoft Visual Studio\2022\BuildTools"
  "%PF64%\Microsoft Visual Studio\2022\Community"
  "%PF64%\Microsoft Visual Studio\2022\Professional"
  "%PF64%\Microsoft Visual Studio\2022\Enterprise"
  "%PF86%\Microsoft Visual Studio\2019\BuildTools"
  "%PF86%\Microsoft Visual Studio\2019\Community"
  "%PF86%\Microsoft Visual Studio\2019\Professional"
  "%PF86%\Microsoft Visual Studio\2019\Enterprise"
) do if not defined VCVARS_BAT if exist "%%~d\VC\Auxiliary\Build\vcvarsall.bat" set "VCVARS_BAT=%%~d\VC\Auxiliary\Build\vcvarsall.bat"

:done
if not defined VCVARS_BAT (
  echo [rhi-live] vcvarsall.bat not found.
  echo [rhi-live] Install the "Desktop development with C++" workload in Visual Studio,
  echo [rhi-live] or set VCVARS to the full path of vcvarsall.bat and run again.
  exit /b 1
)
endlocal & set "VCVARS_BAT=%VCVARS_BAT%"
exit /b 0
