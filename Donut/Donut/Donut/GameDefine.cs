using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class GameDefine
	{
#if DEBUG
		public const bool DEBUG_MODE = true;
#else
		public const bool DEBUG_MODE = false;
#endif

		public const int SCREEN_W_MIN = 100;
		public const int SCREEN_H_MIN = 100;
		public const int SCREEN_W_MAX = 4000;
		public const int SCREEN_H_MAX = 3000;

		// 音楽と効果音の初期ボリューム
		// 0.0 - 1.0
		//
		public const double DEFAULT_VOLUME = 0.45;

		public static void CountDown(ref int count)
		{
			if (count < 0)
				count++;
			else if (0 < count)
				count--;
		}

		public static OneObject<T> GetOneObject<T>(Func<T> getter)
		{
			return new OneObject<T>(getter);
		}

		// 終了時の counter ... (-1): 放した, 0: 放している, 1: 押した, 2-: 押している
		//
		public static void UpdateInput(ref int counter, bool status)
		{
			if (status)
				counter = counter < 0 ? 1 : counter + 1;
			else
				counter = 0 < counter ? -1 : 0;
		}
	}
}
