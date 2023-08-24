@ECHO OFF
CLS

rmdir ..\documentation /s /q

xmldocmd ..\src\lib\iPdfWriter\iPdfWriter\bin\Debug\net462\iPdfWriter.dll ..\documentation

PAUSE
