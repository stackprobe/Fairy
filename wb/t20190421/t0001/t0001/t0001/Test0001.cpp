// smpl
#include "all.h"

// ゲーム処理テンプレート

static void DrawWall(void)
{
	DrawCurtain();
	DrawRect(P_WHITEBOX, 100, 100, SCREEN_W - 200, SCREEN_H - 200);
}
static void XXXSubMain(void)
{
	FreezeInput();

	for(; ; )
	{
		// 1. 入力受付・処理 -- ループを抜ける場合はここから

		if(GetPound(INP_PAUSE))
		{
			break;
		}

		// 2. 描画

		DrawWall();
		SetPrint();
		Print("SPACE(INP_PAUSE) = END");
		DrawCenter(P_DUMMY, SCREEN_CENTER_X, SCREEN_CENTER_Y);

		// 3. EachFrame

		EachFrame();
	}
	FreezeInput();
}
void XXXMain(void)
{
	SetCurtain(0, -1.0);

	forscene(40)
	{
		DrawWall();
		EachFrame();
	}
	sceneLeave();

	SetCurtain();
	FreezeInput();

//	MusicPlay(MUS_XXX);

	for(; ; )
	{
		if(GetPound(INP_A))
		{
			break;
		}
		if(GetPound(INP_PAUSE))
		{
			XXXSubMain();
		}

		DrawWall();
		SetPrint();
		Print("SPACE(INP_PAUSE) = SUB, Z(INP_A) = END");

		EachFrame();
	}
	FreezeInput();
//	MusicFade();
	SetCurtain(30, -1.0);

	forscene(40)
	{
		DrawWall();
		EachFrame();
	}
	sceneLeave();
}
