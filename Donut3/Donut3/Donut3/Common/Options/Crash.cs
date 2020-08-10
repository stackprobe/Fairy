using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common.Options
{
	public struct Crash
	{
		public CrashUtils.Kind_e Kind;
		public D2Point Pt;
		public double R;
		public D4Rect Rect;
		public Crash[] Cs;

		public bool IsCrashed(Crash other)
		{
			return CrashUtils.IsCrashed(this, other);
		}
	}
}
