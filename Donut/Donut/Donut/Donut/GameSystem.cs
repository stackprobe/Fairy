using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameSystem
	{
		public static bool IsWindowActive() // ret: ? このウィンドウはアクティブ
		{
			return DX.GetActiveFlag() != 0;
		}

		public static long GetCurrTime()
		{
			return (long)GameWin32.GetTickCount64(); // TODO 精度がイマイチ
		}
	}
}
