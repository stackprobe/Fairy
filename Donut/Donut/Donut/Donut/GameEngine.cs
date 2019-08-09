using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public static class GameEngine
	{
		public static bool IgnoreEscapeKey = false;

		// 他のファイルからは read only {
		public static long FrameStartTime = 0L;
		public static long LangolierTime;
		public static long LowHzTime;
		public static double EatenByLangolierEval = 0.5;
		public static double LowHzErrorRate = 0.0;
		public static int ProcFrame = 0;
		public static int FreezeInputFrame = 0;
		public static bool WindowIsActive = false;
		// }

		private static void CheckHz()
		{
			long currTime = GameSystem.GetCurrTime();

			if (ProcFrame == 0)
			{
				LangolierTime = currTime;
				LowHzTime = currTime;
			}
			else
			{
				LangolierTime += 16; // 16.666 より小さいので、60Hzならどんどん引き離されるはず。
				LowHzTime += 17;
			}

			while (currTime < LangolierTime)
			{
				Thread.Sleep(1);

				// DxLib >

				DX.ScreenFlip();

				if (DX.ProcessMessage() == -1)
				{
					throw new GameError.EndProc();
				}

				// < DxLib

				currTime = GameSystem.GetCurrTime();
				GameDefine.Approach(ref EatenByLangolierEval, 1.0, 0.9);
			}
			EatenByLangolierEval *= 0.99;

			if (LowHzTime < currTime)
			{
				LowHzTime = Math.Max(LowHzTime, currTime - 10);
				GameDefine.Approach(ref LowHzErrorRate, 1.0, 0.999);
			}
			else
			{
				LowHzTime = Math.Min(LowHzTime, currTime + 20);
				LowHzErrorRate *= 0.99;
			}

			Console.WriteLine(currTime + ", " + (currTime - FrameStartTime)); // test

			FrameStartTime = currTime;
		}

		public static void EachFrame()
		{
			//if (SEEachFrame()) // TODO
			{
				GameMusic.MusicEachFrame();
			}
			GameGround.I.EL.ExecuteAllTask();
			GameToolkit.CurtainEachFrame();

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
				throw new GameError.EndProc();
			}

			// < DxLib

			CheckHz();

			ProcFrame++;
			if (IntTools.IMAX < ProcFrame) // 192.9日程度でカンスト
			{
				throw new GameError();
			}
			GameDefine.CountDown(ref FreezeInputFrame);
			WindowIsActive = GameSystem.IsWindowActive();

			GamePad.PadEachFrame();
			GameKeyboard.KeyEachFrame();
			GameInput.InputEachFrame();
			GameMouse.I.MouseEachFrame();

			if (GameGround.I.RealScreenSize.W != GameGround.I.ScreenSize.W || GameGround.I.RealScreenSize.H != GameGround.I.ScreenSize.H)
			{
				if (GameGround.I.MainScreen == null)
					GameGround.I.MainScreen = new SubScreen(GameGround.I.ScreenSize.W, GameGround.I.ScreenSize.H);

				SubScreen.ChangeDrawScreen(GameGround.I.MainScreen);
			}
		}
	}
}
