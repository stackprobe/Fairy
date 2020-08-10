using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Common.Options
{
	public static class CrashUtils
	{
		public enum Kind_e
		{
			NONE = 1,
			POINT,
			CIRCLE,
			RECT,
			MULTI,
		}

		public static Crash None()
		{
			return new Crash()
			{
				Kind = Kind_e.NONE,
			};
		}

		public static Crash Point(D2Point pt)
		{
			return new Crash()
			{
				Kind = Kind_e.POINT,
				Pt = pt,
			};
		}

		public static Crash Circle(D2Point pt, double r)
		{
			return new Crash()
			{
				Kind = Kind_e.CIRCLE,
				Pt = pt,
				R = r,
			};
		}

		public static Crash Rect_CenterSize(D2Point centerPt, D2Size size)
		{
			return Rect(new D4Rect(centerPt.X - size.W / 2.0, centerPt.Y - size.H / 2.0, size.W, size.H));
		}

		public static Crash Rect(D4Rect rect)
		{
			return new Crash()
			{
				Kind = Kind_e.RECT,
				Rect = rect,
			};
		}

		public static Crash Multi(params Crash[] crashes)
		{
			return Multi((IEnumerable<Crash>)crashes);
		}

		public static Crash Multi(IEnumerable<Crash> crashes)
		{
			return new Crash()
			{
				Kind = Kind_e.MULTI,
				Cs = crashes is Crash[] ? (Crash[])crashes : crashes.ToArray(),
			};
		}

		public static bool IsCrashed(Crash a, Crash b)
		{
			if ((int)b.Kind < (int)a.Kind)
			{
				Crash tmp = a;
				a = b;
				b = tmp;
			}
			if (a.Kind == Kind_e.NONE)
				return false;

			if (b.Kind == Kind_e.MULTI)
				return IsCrashed_Any_Multi(a, b);

			if (a.Kind == Kind_e.POINT)
			{
				if (b.Kind == Kind_e.POINT)
					return false;

				if (b.Kind == Kind_e.CIRCLE)
					return DDUtils.IsCrashed_Circle_Point(b.Pt, b.R, a.Pt);

				if (b.Kind == Kind_e.RECT)
					return DDUtils.IsCrashed_Rect_Point(b.Rect, a.Pt);

				if (b.Kind == Kind_e.MULTI)

					throw new DDError();
			}
			if (a.Kind == Kind_e.CIRCLE)
			{
				if (b.Kind == Kind_e.CIRCLE)
					return DDUtils.IsCrashed_Circle_Circle(a.Pt, a.R, b.Pt, b.R);

				if (b.Kind == Kind_e.RECT)
					return DDUtils.IsCrashed_Circle_Rect(a.Pt, a.R, b.Rect);

				throw new DDError();
			}
			if (a.Kind == Kind_e.RECT)
			{
				if (b.Kind == Kind_e.RECT)
					return DDUtils.IsCrashed_Rect_Rect(a.Rect, b.Rect);

				throw new DDError();
			}
			throw new DDError();
		}

		private static bool IsCrashed_Any_Multi(Crash a, Crash b)
		{
			//if (b.Kind != Kind_e.MULTI) throw null; // never

			if (a.Kind == Kind_e.MULTI)
				return IsCrashed_Multi_Multi(a, b);

			foreach (Crash crash in b.Cs)
				if (IsCrashed(a, crash))
					return true;

			return false;
		}

		private static bool IsCrashed_Multi_Multi(Crash a, Crash b)
		{
			//if (a.Kind != Kind_e.MULTI) throw null; // never
			//if (b.Kind != Kind_e.MULTI) throw null; // never

			foreach (Crash ac in a.Cs)
				foreach (Crash bc in b.Cs)
					if (IsCrashed(ac, bc))
						return true;

			return false;
		}
	}
}
