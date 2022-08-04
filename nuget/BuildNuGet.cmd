@ECHO OFF
CLS

..\src\.nuget\nuget Pack iPdfWriter.1.0.5.nuspec -NoDefaultExcludes -NoPackageAnalysis -OutputDirectory ..\deployment\nuget 

pause

