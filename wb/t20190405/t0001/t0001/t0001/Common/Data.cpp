/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void swapBlock(void *a, void *b, int size)
{
	swap((uchar *)a, (uchar *)b, size);
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int d2i(double value)
{
	return (int)(value + (value < 0.0 ? -0.5 : 0.5));
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
__int64 d2i64(double value)
{
	return (__int64)(value + (value < 0.0 ? -0.5 : 0.5));
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int s2i(char *line, int minval, int maxval, int defval)
{
	if(m_isEmpty(line) || !strchr("-0123456789", *line))
	{
		return defval;
	}
	int value = atoi(line);
	m_range(value, minval, maxval);
	return value;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int s2i_x(char *line, int minval, int maxval, int defval)
{
	int value = s2i(line, minval, maxval, defval);
	memFree(line);
	return value;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int getZero(void)
{
	return 0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
uint getUIZero(void)
{
	return 0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
__int64 getI64Zero(void)
{
	return 0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void noop(void)
{
	1; // noop
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void noop_i(int dummy)
{
	1; // noop
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void noop_ui(uint dummy)
{
	1; // noop
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void noop_i64(__int64 dummy)
{
	1; // noop
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
i2D_t makeI2D(int x, int y)
{
	i2D_t pos;

	pos.X = x;
	pos.Y = y;

	return pos;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
d2D_t makeD2D(double x, double y)
{
	d2D_t pos;

	pos.X = x;
	pos.Y = y;

	return pos;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void my_memset(void *block, int fillValue, int size)
{
	for(int index = 0; index < size; index++)
	{
		((uchar *)block)[index] = fillValue;
	}
}

// 角度から方向 ... MakeXYSpeed, angleToXY

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
/*
	ret: 0.0 ～ PI * 2.0
		右真横(0,0 -> 1,0方向)を0.0として、時計回り。(但し、X軸プラス方向を右、Y軸プラス方向を下)
		1周は PI * 2.0
*/
double getAngle(double x, double y)
{
	if(y < 0.0) return PI * 2.0 - getAngle(x, -y);
	if(x < 0.0) return PI - getAngle(-x, y);

	if(x <= 0.0) return PI / 2.0;
	if(y <= 0.0) return 0;

	double r1 = 0;
	double r2 = PI / 2.0;
	double t = y / x;
	double rm;

	for(int c = 1; ; c++)
	{
		rm = (r1 + r2) / 2.0;

		if(10 <= c)
			break;

		double rmt = tan(rm);

		if(t < rmt)
			r2 = rm;
		else
			r1 = rm;
	}
	return rm;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
double getAngle(double x, double y, double originX, double originY)
{
	return getAngle(x - originX, y - originY);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void rotatePos(double angle, double &x, double &y)
{
	double w;

	w = x * cos(angle) - y * sin(angle);
	y = x * sin(angle) + y * cos(angle);
	x = w;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void rotatePos(double angle, double &x, double &y, double originX, double originY)
{
	x -= originX;
	y -= originY;

	rotatePos(angle, x, y);

	x += originX;
	y += originY;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void angleToXY(double angle, double distance, double &x, double &y)
{
	x = distance * cos(angle);
	y = distance * sin(angle);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void angleToXY(double angle, double distance, double &x, double &y, double originX, double originY)
{
	angleToXY(angle, distance, x, y);

	x += originX;
	y += originY;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void angleMoveXY(double angle, double distance, double &x, double &y)
{
	angleToXY(angle, distance, x, y, x, y);
}
