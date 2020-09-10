using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common.Options
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class DDSlideUtils
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static double Slide(double start, double end, double rate)
		{
			return start + (end - start) * rate;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static D2Point Slide(D2Point start, D2Point end, double rate)
		{
			return new D2Point(
				Slide(start.X, end.X, rate),
				Slide(start.Y, end.Y, rate)
				);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static D4Rect Slide(D4Rect start, D4Rect end, double rate)
		{
			return new D4Rect(
				Slide(start.L, end.L, rate),
				Slide(start.T, end.T, rate),
				Slide(start.W, end.W, rate),
				Slide(start.H, end.H, rate)
				);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static P4Poly Slide(P4Poly start, P4Poly end, double rate)
		{
			return new P4Poly(
				Slide(start.LT, end.LT, rate),
				Slide(start.RT, end.RT, rate),
				Slide(start.RB, end.RB, rate),
				Slide(start.LB, end.LB, rate)
				);
		}
	}
}
