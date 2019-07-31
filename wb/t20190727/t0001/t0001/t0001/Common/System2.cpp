/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
oneObject(finalizers, new finalizers(), GetEndProcFinalizers); // EndProc()     で Flish() する。DxLib_End(); より前。error(); のとき呼ばれない。
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
oneObject(finalizers, new finalizers(), GetFinalizers);        // termination() で Flish() する。DxLib_End(); より後。error(); のとき呼ばれる。
