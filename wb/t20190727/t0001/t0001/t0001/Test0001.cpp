#include "all.h"

void Test0001(void)
{
	char buff[100] = "";

	SetCurtain();
	FreezeInput();

	int inputHdl = MakeKeyInput(30, 1, 0, 0); // ハンドル生成

	SetActiveKeyInput(inputHdl); // 入力開始
	
	for(; ; )
	{
		if(CheckKeyInput(inputHdl)) // 入力終了
			break;

		DrawCurtain();

		DrawKeyInputModeString(100, 100); // IMEモードとか表示

		GetKeyInputString(buff, inputHdl); // 現状の文字列を取得

		SetPrint(100, 200);
		Print(buff);
		
		DrawKeyInputString(100, 300, inputHdl); // 入力中の文字列の描画

		EachFrame();
	}
	DeleteKeyInput(inputHdl); // ハンドル開放

	forscene(120)
	{
		DrawCurtain();

		SetPrint();
		Print(buff);

		EachFrame();
	}
	sceneLeave();

	FreezeInput();
}
