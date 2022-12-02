@ECHO OFF
CLS

rmdir ..\documentation /s /q

xmldocmd ..\src\lib\iPdfWriter\iPdfWriter\bin\Debug\net461\iPdfWriter.dll ..\documentation

PAUSE
