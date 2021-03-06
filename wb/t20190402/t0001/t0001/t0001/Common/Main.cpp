/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static char *GetVersionString(void)
{
	const char *CONCERT_PTN = "{a9a54906-791d-4e1a-8a71-a4c69359cf68}:0.00"; // shared_uuid@g
	return (char *)strchr(CONCERT_PTN, ':') + 1;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int ProcMtxHdl;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int DxLibInited;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void ReleaseProcMtxHdl(void)
{
	mutexRelease(ProcMtxHdl);
	handleClose(ProcMtxHdl);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void PostSetScreenSize(int w, int h)
{
	if(Gnd.MonitorRect.W == w && Gnd.MonitorRect.H == h)
	{
		SetScreenPosition(Gnd.MonitorRect.L, Gnd.MonitorRect.T);
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void EndProc(void)
{
	SaveToDatFile();
	GetEndProcFinalizers()->Flush();
	Gnd_FNLZ();

	if(DxLibInited)
	{
		DxLibInited = 0;
		errorCase(DxLib_End()); // ? 失敗
	}
	termination();
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	memAlloc_INIT();

	/*
		APP_TEMP_DIR_UUID は shared_uuid
	*/
	{
		ProcMtxHdl = mutexOpen("{c808da96-120f-4de5-846a-8c1c120b4a69}"); // shared_uuid@g -- 全ゲーム同時プレイ禁止のため、@g(global)指定

		if(!waitForMillis(ProcMtxHdl, 0))
		{
			handleClose(ProcMtxHdl);
			return 0;
		}
		GetFinalizers()->AddFunc(ReleaseProcMtxHdl);
	}

	initRnd((int)time(NULL));

	if(argIs("/L"))
	{
		termination(LOG_ENABLED ? 1 : 0);
	}

	Gnd_INIT();
	LoadFromDatFile();
	LoadConfig();

	// DxLib >

#if LOG_ENABLED
	SetApplicationLogSaveDirectory("C:\\tmp");
#endif
	SetOutApplicationLogValidFlag(LOG_ENABLED); // DxLib のログを出力 1: する 0: しない

	SetAlwaysRunFlag(1); // ? 非アクティブ時に 1: 動く 0: 止まる

	SetMainWindowText(xcout(
#if LOG_ENABLED
		"(LOG_ENABLED, DEBUGGING_MODE) %s %s"
#else
		GetDatString(DATSTR_PCT_S_SPC_PCT_S)//"%s %s"
#endif
		,GetDatString(DATSTR_APPNAME)
		,GetVersionString()
		));

//	SetGraphMode(SCREEN_W, SCREEN_H, 32);
	SetGraphMode(Gnd.RealScreen_W, Gnd.RealScreen_H, 32);
	ChangeWindowMode(1); // 1: ウィンドウ 0: フルスクリーン

//	SetFullSceneAntiAliasingMode(4, 2); // 適当な値が分からん。フルスクリーン廃止したので不要

	SetWindowIconID(333); // ウィンドウ左上のアイコン

	if(Conf_DisplayIndex != -1)
		SetUseDirectDrawDeviceIndex(Conf_DisplayIndex);

	errorCase(DxLib_Init()); // ? 失敗

	DxLibInited = 1;

	SetMouseDispMode(Gnd.RO_MouseDispMode); // ? マウスを表示 1: する 0: しない
	SetWindowSizeChangeEnableFlag(0); // ウィンドウの右下をドラッグで伸縮 1: する 0: しない

	SetDrawScreen(DX_SCREEN_BACK);
	SetDrawMode(DX_DRAWMODE_BILINEAR); // これをデフォルトとする。

	// < DxLib

	// *_Reset
	{
		DPE_Reset();
		CEE_Reset();
		PE_Reset();
	}

	GetDefaultState(&Gnd.MonitorRect.W, &Gnd.MonitorRect.H, NULL, NULL, &Gnd.MonitorRect.L, &Gnd.MonitorRect.T);

	errorCase(!m_isRange(Gnd.MonitorRect.W, 1, IMAX));
	errorCase(!m_isRange(Gnd.MonitorRect.H, 1, IMAX));
	errorCase(!m_isRange(Gnd.MonitorRect.L, -IMAX, IMAX));
	errorCase(!m_isRange(Gnd.MonitorRect.T, -IMAX, IMAX));

	PostSetScreenSize(Gnd.RealScreen_W, Gnd.RealScreen_H);

#if LOG_ENABLED // 鍵の確認
	{
		char *b = na_(char, 32);
		int s = 32;
		aes128_decrypt_extend(b, s, 1);
		memFree(b);
	}
#endif

	// app > @ INIT

	// < app

	LOGPOS();
	ProcMain();
	LOGPOS();

	EndProc();
	return 0; // dummy
}

// DxPrv_ >

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int DxPrv_GetMouseDispMode(void)
{
	return GetMouseDispFlag() ? 1 : 0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void DxPrv_SetMouseDispMode(int mode)
{
	SetMouseDispFlag(mode ? 1 : 0);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void DxPrv_SetScreenSize(int w, int h)
{
	int mdm = GetMouseDispMode();

	UnloadAllPicResHandle();
	UnloadAllSubScreenHandle();
	ReleaseAllFontHandle();

	errorCase(SetGraphMode(w, h, 32) != DX_CHANGESCREEN_OK); // ? 失敗

	SetDrawScreen(DX_SCREEN_BACK);
	SetDrawMode(DX_DRAWMODE_BILINEAR);

	SetMouseDispMode(mdm);
}

// < DxPrv_

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int GetMouseDispMode(void)
{
	return DxPrv_GetMouseDispMode();
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SetMouseDispMode(int mode)
{
	DxPrv_SetMouseDispMode(mode);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ApplyScreenSize(void)
{
	DxPrv_SetScreenSize(Gnd.RealScreen_W, Gnd.RealScreen_H);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SetScreenSize(int w, int h)
{
	m_range(w, SCREEN_W, SCREEN_W_MAX);
	m_range(h, SCREEN_H, SCREEN_H_MAX);

	Gnd.RealScreenDraw_W = -1; // 無効化

	if(Gnd.RealScreen_W != w || Gnd.RealScreen_H != h)
	{
		Gnd.RealScreen_W = w;
		Gnd.RealScreen_H = h;

		ApplyScreenSize();

		PostSetScreenSize(w, h);
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SetScreenPosition(int l, int t)
{
	SetWindowPosition(l, t);

	POINT p;

	p.x = 0;
	p.y = 0;

	ClientToScreen(GetMainWindowHandle(), &p);

	int pToTrgX = l - (int)p.x;
	int pToTrgY = t - (int)p.y;

	SetWindowPosition(l + pToTrgX, t + pToTrgY);
}
