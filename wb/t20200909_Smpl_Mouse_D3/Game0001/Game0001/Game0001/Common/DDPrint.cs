using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class DDPrint
	{
		// Extra >

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class ExtraInfo
		{
			public DDTaskList TL = null;
			public I3Color Color = new I3Color(255, 255, 255);
			public I3Color BorderColor = new I3Color(-1, 0, 0);
			public int BorderWidth = 0;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static ExtraInfo Extra = new ExtraInfo();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Reset()
		{
			Extra = new ExtraInfo();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetTaskList(DDTaskList tl)
		{
			Extra.TL = tl;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetColor(I3Color color)
		{
			Extra.Color = color;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetBorder(I3Color color, int width = 1)
		{
			Extra.BorderColor = color;
			Extra.BorderWidth = width;
		}

		// < Extra

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int P_BaseX;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int P_BaseY;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int P_YStep;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int P_X;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int P_Y;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetPrint(int x = 0, int y = 0, int yStep = 16)
		{
			P_BaseX = x;
			P_BaseY = y;
			P_YStep = yStep;
			P_X = 0;
			P_Y = 0;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void PrintRet()
		{
			P_X = 0;
			P_Y += P_YStep;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void Print_Main(string line, int x, int y)
		{
			if (Extra.BorderWidth != 0)
				for (int xc = -Extra.BorderWidth; xc <= Extra.BorderWidth; xc++)
					for (int yc = -Extra.BorderWidth; yc <= Extra.BorderWidth; yc++)
						DX.DrawString(x + xc, y + yc, line, DDUtils.GetColor(Extra.BorderColor));

			DX.DrawString(x, y, line, DDUtils.GetColor(Extra.Color));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Print(string line)
		{
			if (line == null)
				throw new DDError();

			int x = P_BaseX + P_X;
			int y = P_BaseY + P_Y;

			if (Extra.TL == null)
			{
				Print_Main(line, x, y);
			}
			else
			{
				ExtraInfo storedExtra = Extra;

				Extra.TL.Add(() =>
				{
					ExtraInfo currExtra = Extra;

					Extra = storedExtra;
					Print_Main(line, x, y);
					Extra = currExtra;

					return false;
				});
			}

			int w = DX.GetDrawStringWidth(line, StringTools.ENCODING_SJIS.GetByteCount(line));

			if (w < 0 || IntTools.IMAX < w)
				throw new DDError();

			P_X += w;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void PrintLine(string line)
		{
			Print(line);
			PrintRet();
		}
	}
}
