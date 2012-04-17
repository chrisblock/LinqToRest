@ECHO OFF

SET POWERSHELL=%SystemRoot%\system32\WindowsPowerShell\v1.0\powershell.exe

%POWERSHELL% Set-ExecutionPolicy RemoteSigned

%POWERSHELL% ".\build\build.ps1"

%POWERSHELL% Set-ExecutionPolicy Default

PAUSE
