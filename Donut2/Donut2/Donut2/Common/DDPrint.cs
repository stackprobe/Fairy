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
		private class PrintInfo
		{
			public DDTaskList TL = null;
			public I3Color Color = new I3Color(255, 255, 255);
			public I3Color BorderColor = new I3Color(-1, 0, 0);
			public int BorderWidth = 0;

			// Print() --->

			public int X;
			public int Y;
			public string Line;
		}

		private static PrintInfo P_Info = new PrintInfo();

		public static void Reset()
		{
			P_Info = new PrintInfo();
		}

		public static void SetTaskList(DDTaskList tl)
		{
			P_Info.TL = tl;
		}

		public static void SetColor(I3Color color)
		{
			P_Info.Color = color;
		}

		public static void SetBorder(I3Color color, int width = 1)
		{
			P_Info.BorderColor = color;
			P_Info.BorderWidth = width;
		}

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

		private static void PrintMain(PrintInfo info)
		{
			if (info.BorderWidth != 0)
				for (int xc = -info.BorderWidth; xc <= info.BorderWidth; xc++)
					for (int yc = -info.BorderWidth; yc <= info.BorderWidth; yc++)
						DX.DrawString(info.X + xc, info.Y + yc, info.Line, DDUtils.GetColor(info.BorderColor));

			DX.DrawString(info.X, info.Y, info.Line, DDUtils.GetColor(info.Color));
		}

		public static void Print(string line)
		{
			if (line == null)
				throw new DDError();

			P_Info.X = P_BaseX + P_X;
			P_Info.Y = P_BaseY + P_Y;
			P_Info.Line = line;

			if (P_Info.TL == null)
			{
				PrintMain(P_Info);
			}
			else
			{
				PrintInfo info = P_Info;

				P_Info.TL.Add(() =>
				{
					PrintMain(info);
					return false;
				});
			}

			int w = DX.GetDrawStringWidth(line, StringTools.ENCODING_SJIS.GetByteCount(line));

			if (w < 0 || IntTools.IMAX < w)
				throw new DDError();

			P_X += w;
		}

		public static void PrintLine(string line)
		{
			Print(line);
			PrintRet();
		}
	}
}
