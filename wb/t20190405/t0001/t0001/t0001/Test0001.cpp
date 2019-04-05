#include "all.h"

#define ANIME_NUM (lengthof(AnimeList) - 1)

static int AnimeList[] =
{
	P_YOUMU_STAND_00,
	P_YOUMU_DASH_00,
	P_YOUMU_JUMP_00,
	P_YOUMU_SIT_00,
	P_YOUMU_HIT_00,
	P_YOUMU_ATTACK_00,
	P_YOUMU_SITATTACK_00,
	P_YOUMU_JUMPATTACK_00,
	P_YOUMU_JUMPFRONT_00,
	P_YOUMU_JUMPFRONT_00_END + 1,
};

static int GetKomaNum(int animeIndex)
{
	return AnimeList[animeIndex + 1] - AnimeList[animeIndex];
}
void Test0001(void)
{
	int animeIndex = 0;
	int komaIndex = 0;

	SetCurtain();
	FreezeInput();

	for(; ; )
	{
		if(1 <= GetInput(INP_A))
		{
			break;
		}
		if(GetPound(INP_DIR_4))
		{
			komaIndex--;
		}
		if(GetPound(INP_DIR_6))
		{
			komaIndex++;
		}
		if(GetPound(INP_DIR_8))
		{
			animeIndex--;
		}
		if(GetPound(INP_DIR_2))
		{
			animeIndex++;
		}
		m_range(animeIndex, 0, ANIME_NUM - 1);
		m_maxim(komaIndex, 0);
		komaIndex %= GetKomaNum(animeIndex);

		DrawCurtain();

		int picId = AnimeList[animeIndex] + komaIndex;
		int w = Pic_W(picId);
		int h = Pic_H(picId);
		int l = SCREEN_CENTER_X - w / 2;
		int t = SCREEN_CENTER_Y - h / 2;

		DrawRect(picId, l, t, w, h);

		SetPrint();
		Print("カーソルキーで操作");
		PrintRet();
		Print_x(xcout("%d / %d, %d / %d", animeIndex, ANIME_NUM, komaIndex, GetKomaNum(animeIndex)));

		EachFrame();
	}
	FreezeInput();
}
