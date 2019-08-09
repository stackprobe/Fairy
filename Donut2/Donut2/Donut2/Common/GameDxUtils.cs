using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	/// <summary>
	/// その他の機能の寄せ集め、そのうち DxLib に関係有るもの。関係無いものは GameUtils へ
	/// </summary>
	public static class GameDxUtils
	{
		public static bool IsWindowActive()
		{
			return DX.GetActiveFlag() != 0;
		}

		public static long GetCurrTime()
		{
			return DX.GetNowHiPerformanceCount() / 1000L;
		}

		public static bool GetMouseDispMode()
		{
			return DX.GetMouseDispFlag() != 0;
		}

		public static void SetMouseDispMode(bool mode)
		{
			DX.SetMouseDispFlag(mode ? 1 : 0);
		}
	}
}
