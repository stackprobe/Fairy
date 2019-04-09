/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern int sc_numer; // 0 - sc_denom
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern int sc_denom; // 1 - *
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern double sc_rate; // 0.0 - 1.0

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void sceneEnter(void);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void sceneLeave(void);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
/*
ex.
	forscene(100)
	{
		// sc_numer == (0 - 100), 101 回処理される。
		EachFrame();
	}
	sceneLeave();

注意
	forscene ブロック内から return するときは sceneLeave(); すること。

	ex.
		forscene(123)
		{
			...
			if(...)
			{
				sceneLeave();
				return;
			}
			...
		}
		sceneLeave();
*/
// frameMax: (1 ～ *)
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define forscene(frameMax) \
	for(sceneEnter(), sc_denom = (frameMax), sc_numer = 0; sc_numer <= (frameMax) && (sc_rate = (double)sc_numer / sc_denom, 1); sc_numer++)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
/*
ex.
	ifsceneBegin(60, count)
	{
		// count == (0 - 60), sc_numer == (0 - 60), sc_denom == 60, 61 回処理される。
		EachFrame();
	}
	ifscene(90)
	{
		// count == (61 - 151), sc_numer == (0 - 90), sc_denom == 90, 91 回処理される。
		EachFrame();
	}
	ifscene(50)
	{
		// count == (152 - 202), sc_numer == (0 - 50), sc_denom == 50, 51 回処理される。
		EachFrame();
	}
	ifsceneEnd
	{
		// count == (203 - *), sc_numer == (0 - *), sc_denom == 保証しない
		EachFrame();
	}
	sceneLeave();

シーケンス
	ifsceneBegin (1) -> ifscene (0*) -> ifsceneEnd (0*1) -> sceneLeave (1)

注意
	ifsceneBegin, ifscene, ifsceneEnd ブロック内から continue, break, return するときは sceneLeave(); すること。

	ex.
		...
		ifsceneEnd
		{
			sceneLeave();
			continue; // break, return
		}
		sceneLeave();
*/
// frameMax: (1 ～ *), count: (0 ～ *)
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define ifsceneBegin(frameMax, count) \
	if(sceneEnter(), sc_numer = (count), 0) { } ifscene((frameMax))

// frameMax: (1 ～ *)
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define ifscene(frameMax) \
	else if(sc_numer <= (frameMax) ? (sc_denom = (frameMax), sc_rate = (double)sc_numer / sc_denom, 1) : (sc_numer -= (frameMax) + 1, 0))

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define ifsceneEnd \
	else

// ---- Curtain ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern double CurrCurtainWhiteLevel;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void CurtainEachFrame(int oncePerFrame = 1);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SetCurtain(int frameMax = 30, double destWhiteLevel = 0.0, double startWhiteLevel = CurrCurtainWhiteLevel);

// ---- Print ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct PrintExtra_st
{
	taskList *TL;
	int Color;

	// change by PE_*() -->

	int Border; // 0 == 無効
	int BorderColor;
}
PrintExtra_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern PrintExtra_t PE;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void PE_Border(int color = GetColor(0, 0, 0));
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void PE_Reset(void);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SetPrint(int x = 0, int y = 0, int yStep = 16);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void PrintRet(void);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Print(char *token);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Print_x(char *token);

// ---- ui tools ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void DrawCurtain(double whiteLevel = -1.0);

// ----

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int RotDir(int dir, int rot);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsOut(double x, double y, double l, double t, double r, double b, double margin = 0.0);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsOutOfScreen(double x, double y, double margin = 0.0);
