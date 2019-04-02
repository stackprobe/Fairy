#include "all.h"

static void Pause(void)
{
	SetCurtain();
	FreezeInput();

	for(; ; )
	{
		if(GetInput(INP_PAUSE) == 1)
		{
			break;
		}

		DrawCurtain();

		SetPrint();
		Print("Pause now...");
		
		EachFrame();
	}
	FreezeInput();
}
void Test0001(void)
{
	SetMouseDispMode(0);

	SetCurtain();
	FreezeInput();

	int mx = SCREEN_CENTER_X;
	int my = SCREEN_CENTER_Y;

	for(; ; )
	{
		if(1 <= GetInput(INP_A))
		{
			break;
		}
		if(GetInput(INP_PAUSE) == 1)
		{
			SetMouseDispMode(1);
			Pause();
			SetMouseDispMode(0);
		}

		UpdateMouseMove();

		mx += MouseMoveX;
		my += MouseMoveY;

		m_range(mx, 0, SCREEN_W - 1);
		m_range(my, 0, SCREEN_H - 1);

		DrawCurtain();

		DrawBegin(P_WHITECIRCLE, mx, my);
		DrawEnd();

		SetPrint();
		Print("Move mouse !");
		PrintRet();
		Print("Press PAUSE to pause.");
		PrintRet();
		Print("Press [Z] to exit.");

		EachFrame();
	}
	FreezeInput();
}
