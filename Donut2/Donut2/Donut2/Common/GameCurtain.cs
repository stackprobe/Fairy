using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameCurtain
	{
		public static double CurrWhiteLevel = 0.0;

		public static void EachFrame()
		{
			// TODO
		}

		public static void SetCurtain(int frameMax, double destWhiteLevel)
		{
			SetCurtain(frameMax, destWhiteLevel, CurrWhiteLevel);
		}

		public static void SetCurtain(int frameMax, double destWhiteLevel, double startWhiteLevel)
		{
			// TODO
		}

		public static void DrawCurtain(double whiteLevel = -1.0)
		{
			whiteLevel = DoubleTools.Range(whiteLevel, -1.0, 1.0);

			if (whiteLevel < 0.0)
			{
				GameDraw.SetAlpha(-whiteLevel);
				GameDraw.SetBright(0.0, 0.0, 0.0);
			}
			else
				GameDraw.SetAlpha(whiteLevel);

			GameDraw.DrawRect(GameGround.CommonResource.WhiteBox, 0, 0, GameConsts.Screen_W, GameConsts.Screen_H);
			GameDraw.Reset();
		}
	}
}
