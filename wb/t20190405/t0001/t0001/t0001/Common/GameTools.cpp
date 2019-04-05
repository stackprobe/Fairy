/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static oneObject(autoList<int>, new autoList<int>(), GetSceneStack);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int sc_numer;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int sc_denom;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
double sc_rate;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void sceneEnter(void)
{
	GetSceneStack()->AddElement(sc_numer);
	GetSceneStack()->AddElement(sc_denom);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void sceneLeave(void)
{
	sc_denom = GetSceneStack()->UnaddElement();
	sc_numer = GetSceneStack()->UnaddElement();
	sc_rate = (double)sc_numer / sc_denom;
}

// ---- Curtain ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static oneObject(autoQueue<double>, new autoQueue<double>(), GetCurtainQueue);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
double CurrCurtainWhiteLevel;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int LastCurtainFrame = -1;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void CurtainEachFrame(int oncePerFrame) // EachFrame()前に呼び出しても可
{
	if(oncePerFrame)
	{
		if(ProcFrame <= LastCurtainFrame)
			return;

		LastCurtainFrame = ProcFrame;
	}
	double wl = GetCurtainQueue()->Dequeue(CurrCurtainWhiteLevel);

	m_range(wl, -1.0, 1.0); // 2bs

	CurrCurtainWhiteLevel = wl;

	if(wl == 0.0)
		return;

	int darkMode;

	if(wl < 0.0)
	{
		wl = abs(wl);
		darkMode = 1;
	}
	else
		darkMode = 0;

	DPE_SetAlpha(wl);

	if(darkMode)
		DPE_SetBright(0.0, 0.0, 0.0);

	DrawRect(P_WHITEBOX, 0.0, 0.0, (double)SCREEN_W, (double)SCREEN_H);
	DPE_Reset();
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SetCurtain(int frameMax, double destWhiteLevel, double startWhiteLevel)
{
	m_range(frameMax, 0, 3600); // 0 frame - 1 min
	m_range(destWhiteLevel, -1.0, 1.0);
	m_range(startWhiteLevel, -1.0, 1.0);

	GetCurtainQueue()->Clear();

	if(!frameMax)
	{
		GetCurtainQueue()->Enqueue(destWhiteLevel);
		return;
	}
	for(int frmcnt = 0; frmcnt <= frameMax; frmcnt++)
	{
		double wl;

		if(!frmcnt)
			wl = startWhiteLevel;
		else if(frmcnt == frameMax)
			wl = destWhiteLevel;
		else
			wl = startWhiteLevel + (destWhiteLevel - startWhiteLevel) * ((double)frmcnt / frameMax);

		GetCurtainQueue()->Enqueue(wl);
	}
}

// ---- Print ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
PrintExtra_t PE;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct PrintInfo_st
{
	int X;
	int Y;
	char *Token;
	PrintExtra_t Extra;
}
PrintInfo_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void PE_Border(int color)
{
	PE.Border = 1;
	PE.BorderColor = color;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void PE_Reset(void)
{
	memset(&PE, 0x00, sizeof(PrintExtra_t));
	PE.Color = GetColor(255, 255, 255);
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int PrintFunc(PrintInfo_t *i)
{
	if(i->Extra.Border)
	{
		for(int xc = -1; xc <= 1; xc++)
		for(int yc = -1; yc <= 1; yc++)
		{
			DrawString(i->X + xc, i->Y + yc, i->Token, i->Extra.BorderColor);
		}
	}
	DrawString(i->X, i->Y, i->Token, i->Extra.Color);
	return 0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void ReleasePrintInfo(PrintInfo_t *i)
{
	memFree(i->Token);
	memFree(i);
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int P_BaseX;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int P_BaseY;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int P_YStep;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int P_X;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int P_Y;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SetPrint(int x, int y, int yStep)
{
	errorCase(x < -IMAX || IMAX < x);
	errorCase(y < -IMAX || IMAX < y);
	errorCase(yStep < 0 || IMAX < yStep);

	P_BaseX = x;
	P_BaseY = y;
	P_YStep = yStep;
	P_X = 0;
	P_Y = 0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void PrintRet(void)
{
	P_X = 0;
	P_Y += P_YStep;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Print(char *token)
{
	Print_x(strx(token));
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Print_x(char *token)
{
	PrintInfo_t *i = nb(PrintInfo_t);

	i->X = P_BaseX + P_X;
	i->Y = P_BaseY + P_Y;
	i->Token = token; // bind
	i->Extra = PE;

	if(!PE.TL)
	{
		PrintFunc(i);
		ReleasePrintInfo(i);
	}
	else
		AddTask(PE.TL, 0, PrintFunc, i, ReleasePrintInfo);

	int w = GetDrawStringWidth(token, strlen(token));
	errorCase(w < 0 || IMAX < w);
	P_X += w;
}

// ---- UI tools ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawCurtain(double whiteLevel)
{
	m_range(whiteLevel, -1.0, 1.0);

	if(whiteLevel < 0.0)
	{
		DPE_SetAlpha(-whiteLevel);
		DPE_SetBright(0.0, 0.0, 0.0);
	}
	else
		DPE_SetAlpha(whiteLevel);

	DrawRect(P_WHITEBOX, 0, 0, SCREEN_W, SCREEN_H);
	DPE_Reset();
}

// ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int RotDir(int dir, int rot) // dir: 2468-1379-5, rot: -n == 反時計周りに n*45°, n == 時計周りに n*45°
{
	errorCase(dir < 1 || 9 < dir);

	if(dir == 5)
		return 5;

	int rotL[] = { -1, 2, 3, 6, 1, -1, 9, 4, 7, 8 };
	int rotR[] = { -1, 4, 1, 2, 7, -1, 3, 8, 9, 6 };

	while(rot < 0)
	{
		dir = rotL[dir];
		rot++;
	}
	while(0 < rot)
	{
		dir = rotR[dir];
		rot--;
	}
	return dir;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsOut(double x, double y, double l, double t, double r, double b, double margin)
{
	return
		x < l - margin || r + margin < x ||
		y < t - margin || b + margin < y;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsOutOfScreen(double x, double y, double margin)
{
	return IsOut(x, y, 0, 0, SCREEN_W, SCREEN_H, margin);
}
