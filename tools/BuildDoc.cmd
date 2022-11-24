@ECHO OFF
CLS

rmdir ..\documentation /s /q

xmldocmd ..\src\lib\iTin.Utilities\iTin.Utilities.Pdf\iTin.Utilities.Pdf.Writer\bin\Release\net461\iTin.Utilities.Abstractions.Writer.dll ..\documentation
xmldocmd ..\src\lib\iTin.Utilities\iTin.Utilities.Pdf\iTin.Utilities.Pdf.Design\bin\Release\net461\iTin.Utilities.Pdf.Design.dll ..\documentation
xmldocmd ..\src\lib\iTin.Utilities\iTin.Utilities.Pdf\iTin.Utilities.Pdf.Writer\bin\Release\net461\iTin.Utilities.Pdf.Writer.dll ..\documentation

PAUSE
