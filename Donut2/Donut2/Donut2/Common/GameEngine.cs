using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class GameEngine
	{
		public static long FrameStartTime;
		public static long LangolierTime;
		public static int ProcFrame;
		public static int FreezeInputFrame;
		public static bool WindowIsActive;

		private static void CheckHz()
		{
			long currTime = GameSystem.GetCurrTime();

			LangolierTime += 16L;
			LangolierTime = LongTools.Range(LangolierTime, currTime - 100L, currTime + 100L);

			while (currTime < LangolierTime)
			{
				Thread.Sleep(1);

				// DxLib >

				DX.ScreenFlip();

				if (DX.ProcessMessage() == -1)
				{
					throw new Exception("End");
				}

				// < DxLib

				currTime = GameSystem.GetCurrTime();
			}
		}

		public static void EachFrame()
		{
			if (GameSE.EachFrame() == false)
			{
				GameMusic.EachFrame();
			}
			GameGround.EL.ExecuteAllTask();
			GameCurtain.EachFrame();

			// TODO
		}
	}
}
