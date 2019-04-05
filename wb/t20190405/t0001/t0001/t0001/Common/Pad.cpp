/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
/*
	padId: 0 - (PAD_MAX - 1) -> DX_INPUT_PAD1 ...
*/
#define PadId2InputType(padId) \
	((padId) + 1)

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int ButtonStatus[PAD_MAX][PAD_BUTTON_MAX];
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static uint PadStatus[PAD_MAX];

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int GetPadCount(void)
{
	static int num = -1;

	if(num == -1)
	{
		num = GetJoypadNum();
		errorCase(num < 0 || PAD_MAX < num);
	}
	return num;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void PadEachFrame(void)
{
	for(int padId = 0; padId < GetPadCount(); padId++)
	{
		uint status;

		if(WindowIsActive)
		{
			status = (uint)GetJoypadInputState(PadId2InputType(padId));
		}
		else // ? 非アクティブ
		{
			status = 0u; // 無入力
		}
		if(status)
		{
			for(int btnId = 0; btnId < PAD_BUTTON_MAX; btnId++)
			{
				updateInput(ButtonStatus[padId][btnId], status & 1u << btnId);
			}
		}
		else
		{
			for(int btnId = 0; btnId < PAD_BUTTON_MAX; btnId++)
			{
				updateInput(ButtonStatus[padId][btnId], 0);
			}
		}

		if(Gnd.PrimaryPadId == -1 && 10 < ProcFrame && !PadStatus[padId] && status) // 最初にボタンを押下したパッドを PrimaryPadId にセット
			Gnd.PrimaryPadId = padId;

		PadStatus[padId] = status;
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int GetPadInput(int padId, int btnId)
{
	if(padId == -1) // ? 未割り当て
		padId = 0;

	if(btnId == -1) // ? 割り当てナシ
		return 0;

	errorCase(padId < 0 || PAD_MAX <= padId);
	errorCase(btnId < 0 || PAD_BUTTON_MAX <= btnId);

	return FreezeInputFrame ? 0 : ButtonStatus[padId][btnId];
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int GetPadPound(int padId, int btnId)
{
	return isPound(GetPadInput(padId, btnId));
}
