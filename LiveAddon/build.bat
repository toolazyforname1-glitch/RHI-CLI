@echo off
rem Builds the 32-bit ReShade addon (rhi-live.addon32).
call "%~dp0find-msvc.bat"
if errorlevel 1 exit /b 1

call "%VCVARS_BAT%" x86 >nul
if errorlevel 1 (
  echo [rhi-live] vcvarsall failed for x86.
  exit /b 1
)

cl /nologo /LD /EHsc /std:c++20 /O2 /MT /W3 /I "%~dp0include" "%~dp0RhiLiveAddon.cpp" /link /OUT:"%~dp0rhi-live.addon32"
exit /b %ERRORLEVEL%
