@echo off
rem Builds the 64-bit ReShade addon (rhi-live.addon64).
call "%~dp0find-msvc.bat"
if errorlevel 1 exit /b 1

call "%VCVARS_BAT%" x64 >nul
if errorlevel 1 (
  echo [rhi-live] vcvarsall failed for x64.
  exit /b 1
)

cl /nologo /LD /EHsc /std:c++20 /O2 /MT /W3 /I "%~dp0include" "%~dp0RhiLiveAddon.cpp" /link /OUT:"%~dp0rhi-live.addon64"
exit /b %ERRORLEVEL%
