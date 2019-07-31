C:\Factory\Tools\RDMD.exe /RC out

SET RAWKEY=ffffffffffffffffffffffffffffffff
rem $_git:secretBegin
///////////////////////////////////////////
rem $_git:secretEnd

C:\Factory\SubTools\makeAESCluster.exe Picture.txt     out\Picture.dat     %RAWKEY% 110000000
C:\Factory\SubTools\makeAESCluster.exe Music.txt       out\Music.dat       %RAWKEY% 120000000
C:\Factory\SubTools\makeAESCluster.exe SoundEffect.txt out\SoundEffect.dat %RAWKEY% 130000000
C:\Factory\SubTools\makeAESCluster.exe Etcetera.txt    out\Etcetera.dat    %RAWKEY% 140000000
C:\Factory\SubTools\makeAESClusterForSH.exe Storehouse out\Storehouse.dat  %RAWKEY% 150000000

COPY /B t0001\Release\t0001.exe out\t0001.exe

out\t0001.exe /L
IF ERRORLEVEL 1 START ?_LOG_ENABLED

C:\Factory\Tools\xcp.exe doc out

COPY /B AUTHORS out
COPY /B C:\Dev\Game\Codevil\doc\Config.conf out

C:\Factory\SubTools\zip.exe /G out t0001
C:\Factory\Tools\summd5.exe /M out

PAUSE
