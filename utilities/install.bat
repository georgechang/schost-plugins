@ECHO OFF
7za.exe e -otemp\* -y -bsp0 -bso0 -an -air!..\src\*.nupkg
cd temp
for /r %x in (*.dll) do copy "%x" c:\stage\sitecoreruntime\production\ /Y