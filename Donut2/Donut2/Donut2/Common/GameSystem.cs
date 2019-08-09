using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.Common
{
	/// <summary>
	/// その他の機能の寄せ集め、そのうち DxLib に関係有るもの。関係無いものは GameUtils へ
	/// </summary>
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
