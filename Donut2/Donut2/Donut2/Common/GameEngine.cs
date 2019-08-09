using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameEngine
	{
		public static long FrameStartTime;
		public static long LangolierTime;
		public static int ProcFrame;
		public static int FreezeInputFrame;
		public static bool WindowIsActive;

		private static void CheckHz()
		{
			long currTime = GameDxUtils.GetCurrTime();

			LangolierTime += 16L; // 16.666 == 60Hz
			LangolierTime = LongTools.Range(LangolierTime, currTime - 100L, currTime + 100L);

			while (currTime < LangolierTime)
			{
				Thread.Sleep(1);

				// DxLib >

				DX.ScreenFlip();

				if (DX.ProcessMessage() == -1)
				{
					throw new GameCoffeeBreak();
				}

				// < DxLib

				currTime = GameDxUtils.GetCurrTime();
			}
		}

		public static void EachFrame()
		{
			if (GameSEUtils.EachFrame() == false)
			{
				GameMusicUtils.EachFrame();
			}
			GameGround.EL.ExecuteAllTask();
			GameCurtain.EachFrame();

			if (GameGround.MainScreen != null && GameSubScreenUtils.CurrDrawScreenHandle == GameGround.MainScreen.GetHandle())
			{
				GameSubScreenUtils.ChangeDrawScreen(DX.DX_SCREEN_BACK);

				if (GameGround.RealScreenDraw_W == -1)
				{
					if (DX.DrawExtendGraph(0, 0, GameGround.RealScreen_W, GameGround.RealScreen_H, GameGround.MainScreen.GetHandle(), 0) != 0) // ? 失敗
						throw new GameError();
				}
				else
				{
					if (DX.DrawBox(0, 0, GameGround.RealScreen_W, GameGround.RealScreen_H, DX.GetColor(0, 0, 0), 1) != 0) // ? 失敗
						throw new GameError();

					if (DX.DrawExtendGraph(
						GameGround.RealScreenDraw_L,
						GameGround.RealScreenDraw_T,
						GameGround.RealScreenDraw_L + GameGround.RealScreenDraw_W,
						GameGround.RealScreenDraw_T + GameGround.RealScreenDraw_H, GameGround.MainScreen.GetHandle(), 0) != 0) // ? 失敗
						throw new GameError();
				}
			}

			// app > @ before ScreenFlip

			// < app

			// DxLib >

			DX.ScreenFlip();

			if (DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == 1 || DX.ProcessMessage() == -1)
			{
				throw new GameCoffeeBreak();
			}

			// < DxLib

			// app > @ after ScreenFlip

			// < app

			CheckHz();

			ProcFrame++;
			GameUtils.CountDown(ref FreezeInputFrame);
			WindowIsActive = GameDxUtils.IsWindowActive();

			if (IntTools.IMAX < ProcFrame) // 192.9日程度でカンスト
			{
				ProcFrame = IntTools.IMAX; // 2bs
				throw new GameError();
			}

			GamePad.EachFrame();
			GameKey.EachFrame();
			GameInput.EachFrame();
			GameMouse.EachFrame();

			if (GameGround.RealScreen_W != GameConsts.Screen_W || GameGround.RealScreen_H != GameConsts.Screen_H || GameGround.RealScreenDraw_W != -1)
			{
				if (GameGround.MainScreen == null)
					GameGround.MainScreen = new GameSubScreen(GameConsts.Screen_W, GameConsts.Screen_H);

				GameGround.MainScreen.ChangeDrawScreen();
			}
			else
			{
				if (GameGround.MainScreen != null)
				{
					GameGround.MainScreen.Dispose();
					GameGround.MainScreen = null;
				}
			}

			// app > @ post EachFrame

			// < app
		}

		public static void FreezeInput(int frame = 1) // frame: 1 == このフレームのみ, 2 == このフレームと次のフレーム ...
		{
			if (frame < 1 || IntTools.IMAX < frame)
				throw new GameError("frame: " + frame);

			FreezeInputFrame = frame;
		}
	}
}
