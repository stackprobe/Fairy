/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
/*
	[Žg‚¢•û]

	SW_Start();

	SW_Lap();
	SW_Lap();
	SW_Lap();
	...

	SW_End();
*/

#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define TIMETABLE_MAX 5
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define LAPTIME_MAX 10000

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct LapTime_st
{
	__int64 LapTime;
	char *Source;
	int LineNo;
	char *Function;
}
LapTime_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static LapTime_t TimeTable[TIMETABLE_MAX][LAPTIME_MAX];
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int LapIndex[TIMETABLE_MAX];
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int TimeIndex = -1;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SW_Start2(void)
{
	errorCase(TIMETABLE_MAX - 1 <= TimeIndex);

	TimeIndex++;
	TimeTable[TimeIndex][0].LapTime = GetPerformanceCounter();
	LapIndex[TimeIndex] = 0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SW_Lap2(char *source, int lineNo, char *function)
{
	errorCase(TimeIndex < 0);
	errorCase(LAPTIME_MAX - 1 <= LapIndex[TimeIndex]);

	__int64 currTime = GetPerformanceCounter();

	LapTime_t *lt = TimeTable[TimeIndex] + LapIndex[TimeIndex];

	lt->LapTime = currTime - lt->LapTime;
	lt->Source = source;
	lt->LineNo = lineNo;
	lt->Function = function;

	LapIndex[TimeIndex]++;
	TimeTable[TimeIndex][LapIndex[TimeIndex]].LapTime = currTime;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SW_End2(char *source, int lineNo, char *function)
{
	errorCase(TimeIndex < 0);

	LapTime_t *lt = TimeTable[TimeIndex] + LapIndex[TimeIndex];

	lt->LapTime = GetPerformanceCounter() - lt->LapTime;
	lt->Source = source;
	lt->LineNo = lineNo;
	lt->Function = function;

	double freq = (double)GetPerformanceFrequency();

	for(int index = 0; index <= LapIndex[TimeIndex]; index++)
	{
		LapTime_t *lt = TimeTable[TimeIndex] + index;

		fprintf(GetLogFp(),
			"[LAP.%d] %I64d %.6f @ %s %d %s\n"
			,index
			,lt->LapTime
			,lt->LapTime / freq
			,lt->Source
			,lt->LineNo
			,lt->Function
			);
	}
	TimeIndex--;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
__int64 GetPerformanceCounter(void)
{
	LARGE_INTEGER i;

	errorCase(!QueryPerformanceCounter(&i)); // ? Ž¸”s
	errorCase(i.QuadPart <= 0i64); // ? Ž¸”s

	return i.QuadPart;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
__int64 GetPerformanceFrequency(void)
{
	static __int64 freq;

	if(freq == 0i64)
	{
		LARGE_INTEGER i;

		errorCase(!QueryPerformanceFrequency(&i)); // ? Ž¸”s
		errorCase(i.QuadPart <= 0i64); // ? Ž¸”s

		freq = i.QuadPart;
	}
	return freq;
}
