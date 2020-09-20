C:\Factory\Tools\RDMD.exe /RC out

C:\Factory\SubTools\makeDDResourceFile.exe C:\Dat\Resource /SD Fairy\Donut3 out\Resource.dat C:\Factory\Program\MaskGZDataForDonut3\MaskGZData.exe
C:\Factory\SubTools\makeDDResourceFile.exe res out\res.dat C:\Factory\Program\MaskGZDataForDonut3\MaskGZData.exe

COPY /B Donut3\Donut3\bin\Release\Donut3.exe out
COPY /B Donut3\Donut3\bin\Release\Chocolate.dll out
COPY /B Donut3\Donut3\bin\Release\DxLib.dll out
COPY /B Donut3\Donut3\bin\Release\DxLib_x64.dll out
COPY /B Donut3\Donut3\bin\Release\DxLibDotNet.dll out

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /RVE- /G out Donut3
C:\Factory\Tools\summd5.exe /M out

PAUSE
