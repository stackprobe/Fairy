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

			GameGround.I.Init();
			GameGround.I.LoadFromDatFile();
			GameGround.I.Config.LoadConfig();

			// DxLib 初期化 ...

#if DEBUG
			DX.SetApplicationLogSaveDirectory(@"C:\tmp");
#endif
			DX.SetOutApplicationLogValidFlag(GameDefine.DEBUG_MODE ? 1 : 0); // DxLib のログを出力 1: する 0: しない

			DX.SetAlwaysRunFlag(1); // ? 非アクティブ時に 1: 動く 0: 止まる

			SetMainWindowTitle();

			//DX.SetGraphMode(GameDefine.SCREEN_SIZE.W, GameDefine.SCREEN_SIZE.H, 32);
			DX.SetGraphMode(GameGround.I.RealScreenSize.W, GameGround.I.RealScreenSize.H, 32);
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

			try
			{
				gameMain.Main();
			}
			catch (GameEndProc)
			{ }

			// Codevil の EndProc() ...

			GameGround.I.SaveToDatFile();
			GameGround.I.GameHandles.Burst();
			GameGround.I.Fnlz();

			if (DX.DxLib_End() != 0)
				throw new GameError("Erred on DxLib_End()");

			// ... Codevil の EndProc()
		}

		public static void SetMainWindowTitle()
		{
			DX.SetMainWindowText(
				(GameDefine.DEBUG_MODE ? "(DEBUGGING_MODE) " : "") +
				ProcMain.APP_TITLE + " " +
				GameGround.I.Config.Version
				);
		}

		private static void PostSetScreenSize(int w, int h)
		{
			if (GameGround.I.MonitorRect.W == w && GameGround.I.MonitorRect.H == h)
			{
				SetScreenPosition(GameGround.I.MonitorRect.L, GameGround.I.MonitorRect.T);
			}
		}

		// DxPrv_ >

		private static bool DxPrv_GetMouseDispMode()
		{
			return DX.GetMouseDispFlag() != 0;
		}

		private static void DxPrv_SetMouseDispMode(bool mode)
		{
			DX.SetMouseDispFlag(mode ? 1 : 0);
		}

		private static void DxPrv_SetScreenSize(int w, int h)
		{
			bool mdm = GetMouseDispMode();

			GamePicture.UnloadAllPicResHandle();
			//UnloadAllSubScreenHandle(); // TODO
			//ReleaseAllFontHandle(); // TODO

			if (DX.SetGraphMode(w, h, 32) != DX.DX_CHANGESCREEN_OK)
				throw new GameError();

			DX.SetDrawScreen(DX.DX_SCREEN_BACK);
			DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);

			SetMouseDispMode(mdm);
		}

		// < DxPrv_

		public static bool GetMouseDispMode()
		{
			return DxPrv_GetMouseDispMode();
		}

		public static void SetMouseDispMode(bool mode)
		{
			DxPrv_SetMouseDispMode(mode);
		}

		public static void ApplyScreenSize()
		{
			DxPrv_SetScreenSize(GameGround.I.RealScreenSize.W, GameGround.I.RealScreenSize.H);
		}

		public static void SetScreenSize(int w, int h)
		{
			if (
				w < GameDefine.SCREEN_W_MIN || GameDefine.SCREEN_W_MAX < w ||
				h < GameDefine.SCREEN_H_MIN || GameDefine.SCREEN_H_MAX < h
				)
				throw new GameError();

			GameGround.I.RealScreenDrawRect.W = -1; // 無効化

			if (GameGround.I.RealScreenSize.W != w || GameGround.I.RealScreenSize.H != h)
			{
				GameGround.I.RealScreenSize.W = w;
				GameGround.I.RealScreenSize.H = h;

				ApplyScreenSize();

				PostSetScreenSize(w, h);
			}
		}

		public static void SetScreenPosition(int l, int t)
		{
			DX.SetWindowPosition(l, t);

			GameWin32.POINT p;

			p.X = 0;
			p.Y = 0;

			GameWin32.ClientToScreen(GameWin32.GetMainWindowHandle(), out p);

			int pToTrgX = l - (int)p.X;
			int pToTrgY = t - (int)p.Y;

			DX.SetWindowPosition(l + pToTrgX, t + pToTrgY);
		}
	}
}
