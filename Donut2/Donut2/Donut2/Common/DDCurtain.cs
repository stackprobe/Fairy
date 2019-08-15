﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class DDCurtain
	{
		private static Queue<double> WhiteLevels = new Queue<double>();

		public static double CurrWhiteLevel = 0.0;
		public static int LastFrame = -1;

		public static void EachFrame(bool oncePerFrame = true) // EachFrame()前に呼び出しても可
		{
			if (oncePerFrame)
			{
				if (DDEngine.ProcFrame <= LastFrame)
					return;

				LastFrame = DDEngine.ProcFrame;
			}
			double wl;

			if (1 <= WhiteLevels.Count)
				wl = WhiteLevels.Dequeue();
			else
				wl = CurrWhiteLevel;

			DrawCurtain(wl);
			CurrWhiteLevel = wl;
		}

		public static void SetCurtain(int frameMax = 30, double destWhiteLevel = 0.0)
		{
			SetCurtain(frameMax, destWhiteLevel, CurrWhiteLevel);
		}

		public static void SetCurtain(int frameMax, double destWhiteLevel, double startWhiteLevel)
		{
			WhiteLevels.Clear();

			if (frameMax == 0)
			{
				WhiteLevels.Enqueue(destWhiteLevel);
			}
			else
			{
				for (int frame = 0; frame <= frameMax; frame++)
				{
					double wl;

					if (frame == 0)
						wl = startWhiteLevel;
					else if (frame == frameMax)
						wl = destWhiteLevel;
					else
						wl = startWhiteLevel + (((destWhiteLevel - startWhiteLevel) * frame) / frameMax);

					WhiteLevels.Enqueue(wl);
				}
			}
		}

		public static void DrawCurtain(double whiteLevel = -1.0)
		{
			if (whiteLevel == 0.0)
				return;

			whiteLevel = DoubleTools.Range(whiteLevel, -1.0, 1.0);

			if (whiteLevel < 0.0)
			{
				DDDraw.SetAlpha(-whiteLevel);
				DDDraw.SetBright(0.0, 0.0, 0.0);
			}
			else
				DDDraw.SetAlpha(whiteLevel);

			DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, 0, 0, DDConsts.Screen_W, DDConsts.Screen_H);
			DDDraw.Reset();
		}
	}
}