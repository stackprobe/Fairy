IF NOT EXIST Dungeon\. GOTO END
CLS
rem �����[�X���� qrum ���܂��B
PAUSE

CALL newcsrr

CALL ff
cx **

CD /D %~dp0.

IF NOT EXIST Dungeon\. GOTO END

CALL qq
cx **

CALL _Release.bat /-P

MOVE out\Dungeon.zip S:\�����[�X��\.

START "" /B /WAIT /DC:\home\bat syncRev

CALL qrumauto rel

rem **** AUTO RELEASE COMPLETED ****

:END
