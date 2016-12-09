@echo off

%windir%\microsoft.net\framework\v4.0.30319\msbuild ..\Platform.sln /p:Configuration=Release "/p:Platform=Any CPU" /m /verbosity:detailed

pause