#include "all.h"

void Test0001(void)
{
	char buff[100] = "";

	SetCurtain();
	FreezeInput();

	int inputHdl = MakeKeyInput(30, 1, 0, 0); // �n���h������

	SetActiveKeyInput(inputHdl); // ���͊J�n
	
	for(; ; )
	{
		if(CheckKeyInput(inputHdl)) // ���͏I��
			break;

		DrawCurtain();

		DrawKeyInputModeString(100, 100); // IME���[�h�Ƃ��\��

		GetKeyInputString(buff, inputHdl); // ����̕�������擾

		SetPrint(100, 200);
		Print(buff);
		
		DrawKeyInputString(100, 300, inputHdl); // ���͒��̕�����̕`��

		EachFrame();
	}
	DeleteKeyInput(inputHdl); // �n���h���J��

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
