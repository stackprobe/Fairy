C:\Factory\Tools\RDMD.exe /RC out

C:\Factory\SubTools\makeDDResourceFile.exe Resource out\Resource.dat Tools\MaskGZData.exe

COPY /B Dungeon\Dungeon\bin\Release\Dungeon.exe out
rem C:\Factory\SubTools\CallConfuserCLI.exe Dungeon\Dungeon\bin\Release\Dungeon.exe out\Dungeon.exe
COPY /B Dungeon\Dungeon\bin\Release\Chocolate.dll out
COPY /B Dungeon\Dungeon\bin\Release\DxLib.dll out
COPY /B Dungeon\Dungeon\bin\Release\DxLib_x64.dll out
COPY /B Dungeon\Dungeon\bin\Release\DxLibDotNet.dll out

C:\Factory\Tools\xcp.exe doc out
C:\Factory\Tools\xcp.exe C:\Dev\Fairy\Donut2\doc out

C:\Factory\SubTools\zip.exe /O out Dungeon

IF NOT "%1" == "/-P" PAUSE
