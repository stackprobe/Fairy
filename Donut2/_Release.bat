C:\Factory\Tools\RDMD.exe /RC out

COPY /B Donut2\Donut2\bin\Release\Donut2.exe out
COPY /B Donut2\Donut2\bin\Release\Chocolate.dll out
COPY /B Donut2\Donut2\bin\Release\DxLib.dll out
COPY /B Donut2\Donut2\bin\Release\DxLib_x64.dll out
COPY /B Donut2\Donut2\bin\Release\DxLibDotNet.dll out

C:\Factory\SubTools\zip.exe /G out Donut2
C:\Factory\Tools\summd5.exe /M out

PAUSE
