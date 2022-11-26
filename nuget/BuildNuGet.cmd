@ECHO OFF
CLS

..\src\.nuget\nuget Pack iPdfWriter.1.0.6.nuspec -NoDefaultExcludes -NoPackageAnalysis -OutputDirectory ..\deployment\nuget 

pause

