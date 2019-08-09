using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;
using DxLibDLL;
using System.Drawing;

namespace Charlotte.Common
{
	public static class GameMain
	{
		public static void GameStart()
		{
			GameConfig.Load(); // LogFile, LOG_ENABLED を含むので真っ先に

			// Log >

			File.WriteAllBytes(GameConfig.LogFile, BinTools.EMPTY);

			ProcMain.WriteLog = message =>
			{
				using (StreamWriter writer = new StreamWriter(GameConfig.LogFile, true, Encoding.UTF8))
				{
					writer.WriteLine("[" + DateTime.Now + "] " + message);
				}
			};

			// < Log

			// *.INIT
			{
				GameGround.INIT();
				GameResource.INIT();
				GameUserDatStrings.INIT();
			}

			GameSaveData.Load();

			// DxLib >

			if (GameConfig.LOG_ENABLED)
				DX.SetApplicationLogSaveDirectory(@"C:\tmp");

			DX.SetOutApplicationLogValidFlag(GameConfig.LOG_ENABLED ? 1 : 0); // DxLib のログを出力 1: する 0: しない

			DX.SetAlwaysRunFlag(1); // ? 非アクティブ時に 1: 動く 0: 止まる

			DX.SetMainWindowText(GameDatStrings.Title + " " + GameUserDatStrings.Version);

			//DX.SetGraphMode(GameConsts.Screen_W, GameConsts.Screen_H, 32);
			DX.SetGraphMode(GameGround.RealScreen_W, GameGround.RealScreen_H, 32);
			DX.ChangeWindowMode(1); // 1: ウィンドウ 0: フルスクリーン

			//DX.SetFullSceneAntiAliasingMode(4, 2); // 適当な値が分からん。フルスクリーン廃止したので不要

			// TODO Icon

			if (GameConfig.DisplayIndex != -1)
				DX.SetUseDirectDrawDeviceIndex(GameConfig.DisplayIndex);

			if (DX.DxLib_Init() != 0) // ? 失敗
				throw new GameError();

			SetMouseDisplayMode(GameGround.RO_MouseDispMode); // ? マウスを表示する。
			DX.SetWindowSizeChangeEnableFlag(0); // ウィンドウの右下をドラッグで伸縮 1: する 0: しない

			DX.SetDrawScreen(DX.DX_SCREEN_BACK);
			DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR); // これをデフォルトとする。

			// < DxLib

			{
				int l;
				int t;
				int w;
				int h;
				int p1;
				int p2;
				int p3;
				int p4;

				DX.GetDefaultState(out w, out h, out p1, out p2, out l, out t, out p3, out p4);

				if (
					w < 1 || IntTools.IMAX < w ||
					h < 1 || IntTools.IMAX < h ||
					l < -IntTools.IMAX || IntTools.IMAX < l ||
					t < -IntTools.IMAX || IntTools.IMAX < t
					)
					throw new GameError();

				GameGround.MonitorRect = new Rectangle(l, t, w, h);
			}

			PostSetScreenSize(GameGround.RealScreen_W, GameGround.RealScreen_H);
		}

		public static void GameEnd()
		{
			GameSaveData.Save();

			// *.FNLZ
			{
				GameUserDatStrings.FNLZ();
				GameResource.FNLZ();
				GameGround.FNLZ();
			}
		}

		private static void SetMouseDisplayMode(bool mode)
		{
			throw null; // TODO
		}

		private static void PostSetScreenSize(int w, int h)
		{
			throw null; // TODO
		}
	}
}
