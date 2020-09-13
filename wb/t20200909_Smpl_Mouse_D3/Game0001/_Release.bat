C:\Factory\Tools\RDMD.exe /RC out

C:\Factory\SubTools\makeDDResourceFile.exe ^
	C:\Dat\Resource ^
	/SD Fairy\Donut3\General ^
	/SD Etoile\Game0001 ^
	out\Resource.dat ^
	C:\Factory\Program\MaskGZDataForDonut3\MaskGZData.exe

C:\Factory\SubTools\makeDDResourceFile.exe ^
	res ^
	out\res.dat ^
	C:\Factory\Program\MaskGZDataForDonut3\MaskGZData.exe

C:\Factory\SubTools\CallConfuserCLI.exe Game0001\Game0001\bin\Release\Game0001.exe out\Game0001.exe
rem COPY /B Game0001\Game0001\bin\Release\Game0001.exe out
COPY /B Game0001\Game0001\bin\Release\Chocolate.dll out
COPY /B Game0001\Game0001\bin\Release\DxLib.dll out
COPY /B Game0001\Game0001\bin\Release\DxLib_x64.dll out
COPY /B Game0001\Game0001\bin\Release\DxLibDotNet.dll out

C:\Factory\Tools\xcp.exe doc out
C:\Factory\Tools\xcp.exe C:\Dev\Fairy\Donut3\doc out

C:\Factory\SubTools\zip.exe /PE- /RVE- /G out Game0001
C:\Factory\Tools\summd5.exe /M out

IF NOT "%1" == "/-P" PAUSE
