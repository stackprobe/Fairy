/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define _CRT_SECURE_NO_WARNINGS

// 定番 {
#include <conio.h>
#include <ctype.h>
#include <direct.h>
#include <dos.h>
#include <fcntl.h>
#include <io.h>
#include <limits.h>
#include <malloc.h>
#include <math.h>
#include <mbstring.h>
#include <process.h>
#include <stdarg.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys\types.h> // sys/stat.h より先であること。
#include <sys\stat.h>
#include <time.h>

#include <windows.h>
// }

#include <DxLib.h>

// app > @ define LOG_ENABLED

/*
#define LOG_ENABLED 0
/*/
#define LOG_ENABLED 1
//*/

// < app

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef unsigned char uchar;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef unsigned __int16 uint16;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef unsigned __int32 uint;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef unsigned __int64 uint64;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define IMAX 1000000000
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define IMAX_64 1000000000000000000i64
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define PI     3.141592653589793
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define ROOT_2 1.414213562373095

// app > @ define SCREEN_WH

#define SCREEN_W 800
#define SCREEN_H 600

// < app

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define SCREEN_CENTER_X (SCREEN_W / 2)
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define SCREEN_CENTER_Y (SCREEN_H / 2)
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define SCREEN_W_MAX (SCREEN_W * 5)
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define SCREEN_H_MAX (SCREEN_H * 5)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define MONITOR_MAX 10

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
/*
	音楽と効果音の初期ボリューム
	0.0 - 1.0
*/
#define DEFAULT_VOLUME 0.45

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define lengthof(list) \
	(sizeof((list)) / sizeof(*(list)))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_isEmpty(str) \
	(!(str) || !*(str))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_min(value1, value2) \
	((value1) < (value2) ? (value1) : (value2))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_max(value1, value2) \
	((value1) < (value2) ? (value2) : (value1))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_minim(var, value) \
	((var) = m_min((var), (value)))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_maxim(var, value) \
	((var) = m_max((var), (value)))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_range(var, minval, maxval) \
	do { \
	m_maxim((var), (minval)); \
	m_minim((var), (maxval)); \
	} while(0)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_isRange(value, minval, maxval) \
	((minval) <= (value) && (value) <= (maxval))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_iSign(value) \
	((value) < 0 ? -1 : 0 < (value) ? 1 : 0)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define ENUM_RANGE(enum_member, num) \
	enum_member, \
	enum_member##_END = enum_member + (num) - 1,

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
/*
	ex.
		oneObject(int, (int *)na(int, 100), GetIntList);         ... プロトタイプ宣言は oneObjectProto(int, GetIntList);
		static oneObject(ClassABC, new ClassABC(), GetClassABC); ... static の場合
*/
#define oneObject(type_t, init_op, getter) \
	type_t *(getter)(void) { \
		static type_t *p; \
		if(!p) { \
			p = (init_op); \
		} \
		return p; \
	}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define oneObjectProto(type_t, getter) \
	type_t *(getter)(void)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
/*
	終了時の counter ... (-1): 放した, 0: 放している, 1: 押した, 2-: 押している
*/
#define updateInput(counter, status) \
	if((status)) { \
		(counter) = m_max(0, (counter)); \
		(counter)++; \
	} \
	else { \
		(counter) = 0 < (counter) ? -1 : 0; \
	}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_countDown(var) \
	((var) < 0 ? (var)++ : 0 < (var) ? (var)-- : (var))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_approach(var, dest, rate) \
	do { \
	(var) -= (dest); \
	(var) *= (rate); \
	(var) += (dest); \
	} while(0)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_iApproach(var, dest, rateNumer, rateDenom) \
	do { \
	(var) -= (dest); \
	(var) *= (rateNumer); \
	(var) /= (rateDenom); \
	(var) += (dest); \
	} while(0)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_constApproach(var, dest, speed) \
	do { \
	(var) -= (dest); \
	(var) < 0.0 ? \
		((var) += (speed), m_minim((var), 0.0)) : \
		((var) -= (speed), m_maxim((var), 0.0)); \
	(var) += (dest); \
	} while(0)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct i2D_st
{
	int X;
	int Y;
}
i2D_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct iRect_st
{
	int L;
	int T;
	int W;
	int H;
}
iRect_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct d2D_st
{
	double X;
	double Y;
}
d2D_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_swap(var1, var2, TYPE_T) \
	do { \
	TYPE_T tmp = (TYPE_T)var1; \
	*(TYPE_T *)&var1 = (TYPE_T)var2; \
	*(TYPE_T *)&var2 = (TYPE_T)tmp; \
	} while(0)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_simpleComp(v1, v2) \
	((v1) < (v2) ? -1 : ((v2) < (v1) ? 1 : 0))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_color(r, g, b) \
	((r) << 16 | (g) << 8 | (b))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_01(value) \
	((value) == 0 ? 0 : 1)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define m_10(value) \
	((value) == 0 ? 1 : 0)

// app > @ define

// < app
