using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameEngine
	{
		public static bool IgnoreEscapeKey = false;

		// 他のファイルからは read only {
		public static long FrameStartTime = 0L;
		public static int ProcFrame = 0;
		public static int FreezeInputFrame = 0;
		public static bool WindowIsActive = false;
		public static int FrameRateDropCount = 0;
		public static int NoFrameRateDropCount = 0;
		// }

		public static void EachFrame()
		{
			// TODO
			/*
			if (SEEachFrame())
			{
				MusicEachFrame();
			}
			*/
			GameGround.I.EL.ExecuteAllTask();
			GameHelper.CurtainEachFrame();

			if (GameGround.I.MainScreen != null && SubScreen.CurrDrawScreenHandle == GameGround.I.MainScreen.GetHandle())
			{
				SubScreen.ChangeDrawScreen(DX.DX_SCREEN_BACK);

				if (GameGround.I.RealScreenDrawRect.W == -1)
				{
					if (DX.DrawExtendGraph(0, 0, GameGround.I.RealScreenSize.W, GameGround.I.RealScreenSize.H, GameGround.I.MainScreen.GetHandle(), 0) != 0) // ? 失敗
						throw new GameError();
				}
				else
				{
					if (DX.DrawBox(0, 0, GameGround.I.RealScreenSize.W, GameGround.I.RealScreenSize.H, DX.GetColor(0, 0, 0), 1) != 0) // ? 失敗
						throw new GameError();

					if (DX.DrawExtendGraph(
						GameGround.I.RealScreenDrawRect.L,
						GameGround.I.RealScreenDrawRect.T,
						GameGround.I.RealScreenDrawRect.L + GameGround.I.RealScreenDrawRect.W,
						GameGround.I.RealScreenDrawRect.T + GameGround.I.RealScreenDrawRect.H, GameGround.I.MainScreen.GetHandle(), 0) != 0) // ? 失敗
						throw new GameError();
				}
			}

			// DxLib >

			DX.ScreenFlip();

			if ((IgnoreEscapeKey == false && DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == 1) || DX.ProcessMessage() == -1)
			{
				throw new GameEndProc();
			}

			// < DxLib

			CheckHz();

			ProcFrame++;
			if (IntTools.IMAX < ProcFrame) // 192.9日程度でカンスト
			{
				throw new GameError();
			}
			GameHelper.CountDown(ref FreezeInputFrame);
			WindowIsActive = GameHelper.IsWindowActive();

			GamePad.PadEachFrame();
			GameKeyboard.KeyEachFrame();
			GameInput.InputEachFrame();
			//MouseEachFrame(); // TODO

			if (GameGround.I.RealScreenSize.W != GameGround.I.ScreenSize.W || GameGround.I.RealScreenSize.H != GameGround.I.ScreenSize.H)
			{
				if (GameGround.I.MainScreen == null)
					GameGround.I.MainScreen = new SubScreen(GameGround.I.ScreenSize.W, GameGround.I.ScreenSize.H);

				SubScreen.ChangeDrawScreen(GameGround.I.MainScreen);
			}
		}

		private static void CheckHz()
		{
			long currTime = GameHelper.GetCurrTime();
			long diffTime = currTime - FrameStartTime;

			if (diffTime < 15 || 18 < diffTime) // ? frame rate drop
				FrameRateDropCount++;
			else
				NoFrameRateDropCount++;

			FrameStartTime = currTime;
		}
	}
}
