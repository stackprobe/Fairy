using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.Common
{
	public class GameSystem
	{
		public static bool IsWindowActive()
		{
			return DX.GetActiveFlag() != 0;
		}

		public static long GetCurrTime()
		{
			return DX.GetNowHiPerformanceCount() / 1000L;
		}
	}
}
