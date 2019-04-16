C:\Factory\Tools\RDMD.exe /RC out

SET RAWKEY=ae133d2791c4de47911b890b22bba7b9

C:\Factory\SubTools\makeAESCluster.exe Picture.txt     out\Picture.dat     %RAWKEY% 110000000
C:\Factory\SubTools\makeAESCluster.exe Music.txt       out\Music.dat       %RAWKEY% 120000000
C:\Factory\SubTools\makeAESCluster.exe SoundEffect.txt out\SoundEffect.dat %RAWKEY% 130000000
C:\Factory\SubTools\makeAESCluster.exe Etcetera.txt    out\Etcetera.dat    %RAWKEY% 140000000

COPY /B Patchouli\Release\Patchouli.exe out\Patchouli.exe

out\Patchouli.exe /L
IF ERRORLEVEL 1 START ?_LOG_ENABLED

C:\Factory\Tools\xcp.exe doc out

COPY /B AUTHORS out
COPY /B C:\Dev\Game\Codevil\doc\Config.conf out

C:\Factory\SubTools\zip.exe /G out Patchouli
C:\Factory\Tools\summd5.exe /M out

PAUSE
