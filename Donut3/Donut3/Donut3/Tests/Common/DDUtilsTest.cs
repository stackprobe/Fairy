using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Tests.Common
{
	public class DDUtilsTest
	{
		public void Test01()
		{
			Test01_a(new D2Size(50.0, 100.0), new D4Rect(1000.0, 1000.0, 300.0, 200.0));
			Test01_a(new D2Size(100.0, 50.0), new D4Rect(1000.0, 1000.0, 300.0, 200.0));
			Test01_a(new D2Size(100.0, 100.0), new D4Rect(1000.0, 1000.0, 300.0, 200.0));
			Test01_a(new D2Size(500.0, 1000.0), new D4Rect(1000.0, 1000.0, 300.0, 200.0));
			Test01_a(new D2Size(1000.0, 500.0), new D4Rect(1000.0, 1000.0, 300.0, 200.0));
			Test01_a(new D2Size(1000.0, 1000.0), new D4Rect(1000.0, 1000.0, 300.0, 200.0));
			Test01_a(new D2Size(100.0, 100.0), new D4Rect(1000.0, 1000.0, 300.0, 300.0));
			Test01_a(new D2Size(1000.0, 1000.0), new D4Rect(1000.0, 1000.0, 300.0, 300.0));
			Test01_a(new D2Size(1.0, 5.0), new D4Rect(1000.0, 1000.0, 0.001, 0.001));
			Test01_a(new D2Size(5.0, 1.0), new D4Rect(1000.0, 1000.0, 0.001, 0.001));
		}

		private void Test01_a(D2Size size, D4Rect rect)
		{
			D4Rect interior;
			D4Rect exterior;

			DDUtils.AdjustRect(size, rect, out interior, out exterior);

			Test01_a2(size, rect, "INTERIOR", interior);
			Test01_a2(size, rect, "EXTERIOR", exterior);
		}

		private void Test01_a2(D2Size size, D4Rect rect, string destName, D4Rect dest)
		{
			ProcMain.WriteLog(string.Format("({0}) -> ({1}) {2} -> {3}"
				, string.Join(", ", size.W, size.H)
				, string.Join(", ", rect.L, rect.T, rect.W, rect.H)
				, destName
				, string.Join(", ", dest.L, dest.T, dest.W, dest.H)
				));
		}
	}
}
