﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	public static class DDPrint
	{
		// Extra >

		private class ExtraInfo
		{
			public I3Color Color = new I3Color(255, 255, 255);
			public I3Color BorderColor = new I3Color(-1, 0, 0);
			public int BorderWidth = 0;
		}

		private static ExtraInfo Extra = new ExtraInfo();

		public static void Reset()
		{
			Extra = new ExtraInfo();
		}

		public static void SetColor(I3Color color)
		{
			Extra.Color = color;
		}

		public static void SetBorder(I3Color color, int width = 1)
		{
			Extra.BorderColor = color;
			Extra.BorderWidth = width;
		}

		// < Extra

		private static int P_BaseX;
		private static int P_BaseY;
		private static int P_YStep;
		private static int P_X;
		private static int P_Y;

		public static void SetPrint(int x = 0, int y = 0, int yStep = 16)
		{
			P_BaseX = x;
			P_BaseY = y;
			P_YStep = yStep;
			P_X = 0;
			P_Y = 0;
		}

		public static void PrintRet()
		{
			P_X = 0;
			P_Y += P_YStep;
		}

		public static void Print(string line)
		{
			if (line == null)
				throw new DDError();

			{
				int x = P_BaseX + P_X;
				int y = P_BaseY + P_Y;

				if (Extra.BorderWidth != 0)
					for (int xc = -Extra.BorderWidth; xc <= Extra.BorderWidth; xc++)
						for (int yc = -Extra.BorderWidth; yc <= Extra.BorderWidth; yc++)
							DX.DrawString(x + xc, y + yc, line, DDUtils.GetColor(Extra.BorderColor));

				DX.DrawString(x, y, line, DDUtils.GetColor(Extra.Color));
			}

			{
				int w = DX.GetDrawStringWidth(line, StringTools.ENCODING_SJIS.GetByteCount(line));

				if (w < 0 || IntTools.IMAX < w)
					throw new DDError();

				P_X += w;
			}
		}

		public static void PrintLine(string line)
		{
			Print(line);
			PrintRet();
		}
	}
}
