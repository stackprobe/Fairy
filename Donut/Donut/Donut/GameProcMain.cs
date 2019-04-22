using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameProcMain
	{
		private static bool LogWrote = false;

		public static void GPMain(IGameMain gameMain)
		{
#if DEBUG
			ProcMain.WriteLog = message =>
			{
				using (StreamWriter writer = new StreamWriter(@"C:\tmp\Game.log", LogWrote, Encoding.UTF8))
				{
					writer.WriteLine("[" + DateTime.Now + "] " + message);
				}
				LogWrote = true;
			};
#else
			ProcMain.WriteLog = message => { };
#endif

			HandleDam.Transaction(hDam =>
			{
				GameGround.I = new GameGround();
				GameGround.I.Handles = hDam;

				GPMain2(gameMain);
			});
		}

		private static void GPMain2(IGameMain gameMain)
		{
			gameMain.Init();

			//Gnd_INIT(); // Only in Codevil
			GameGround.I.LoadFromDatFile();
			GameGround.I.Config.LoadConfig();

			// DxLib 初期化 ...

#if DEBUG
			DX.SetApplicationLogSaveDirectory(@"C:\tmp");
#endif
			DX.SetOutApplicationLogValidFlag(GameDefine.DEBUG_MODE ? 1 : 0); // DxLib のログを出力 1: する 0: しない

			DX.SetAlwaysRunFlag(1); // ? 非アクティブ時に 1: 動く 0: 止まる

			DX.SetMainWindowText(
				(GameDefine.DEBUG_MODE ? "(DEBUGGING_MODE) " : "") +
				ProcMain.APP_TITLE + " " +
				GameGround.I.Config.Version
				);

			//DX.SetGraphMode(GameDefine.SCREEN_SIZE.W, GameDefine.SCREEN_SIZE.H, 32);
			DX.SetGraphMode(GameGround.I.ScreenSize.W, GameGround.I.ScreenSize.H, 32);
			DX.ChangeWindowMode(1); // 1: ウィンドウ 0: フルスクリーン

			//DX.SetFullSceneAntiAliasingMode(4, 2); // 適当な値が分からん。フルスクリーン廃止したので不要

			DX.SetWindowIconHandle(gameMain.GetIcon()); // ウィンドウ左上のアイコン

			if (GameGround.I.Config.DisplayIndex != -1)
				DX.SetUseDirectDrawDeviceIndex(GameGround.I.Config.DisplayIndex);

			if (DX.DxLib_Init() != 0)
				throw new GameError("Error on DxLib_Init()");

			SetMouseDispMode(GameGround.I.RO_MouseDispMode); // ? マウスを表示 true: する false: しない
			DX.SetWindowSizeChangeEnableFlag(0); // ウィンドウの右下をドラッグで伸縮 1: する 0: しない

			DX.SetDrawScreen(DX.DX_SCREEN_BACK);
			DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR); // これをデフォルトとする。

			// ... DxLib 初期化

			GameGround.I.MonitorRect = new I4Rect();

			{
				int dummy1;
				int dummy2;
				int dummy3;
				int dummy4;

				DX.GetDefaultState(
					out GameGround.I.MonitorRect.W,
					out GameGround.I.MonitorRect.H,
					out dummy1,
					out dummy2,
					out GameGround.I.MonitorRect.L,
					out GameGround.I.MonitorRect.T,
					out dummy3,
					out dummy4
					);
			}

			if (
				GameGround.I.MonitorRect.W < 1 || IntTools.IMAX < GameGround.I.MonitorRect.W ||
				GameGround.I.MonitorRect.H < 1 || IntTools.IMAX < GameGround.I.MonitorRect.H ||
				GameGround.I.MonitorRect.L < -IntTools.IMAX || IntTools.IMAX < GameGround.I.MonitorRect.L ||
				GameGround.I.MonitorRect.T < -IntTools.IMAX || IntTools.IMAX < GameGround.I.MonitorRect.T
				)
				throw new GameError();

			gameMain.Main();

			// Codevil の EndProc ...

			GameGround.I.SaveToDatFile();
			GameGround.I.GameHandles.Burst();
			//Gnd_FNLZ(); // Only in Codevil

			if (DX.DxLib_End() != 0)
				throw new GameError("Erred on DxLib_End()");

			// ... Codevil の EndProc
		}

		private static void PostSetScreenSize(int w, int h)
		{
			// TODO
		}

		// DxPrv_ >

		private static void DxPrv_SetMouseDispMode(bool mode)
		{
			DX.SetMouseDispFlag(mode ? 1 : 0);
		}

		// < DxPrv_

		public static void SetMouseDispMode(bool mode)
		{
			DxPrv_SetMouseDispMode(mode);
		}
	}
}
