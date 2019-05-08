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

#if false // test
		private static bool GNCT_Inited = false;
		private static int GNCT_StartCount;

		private static int GetNowCount_TEST()
		{
			int currCount = DX.GetNowCount();

			if (GNCT_Inited == false)
			{
				GNCT_StartCount = currCount;
				GNCT_Inited = true;
			}
			currCount -= GNCT_StartCount;
			currCount -= 10000; // 要調整
			return currCount;
		}
#endif

		private static long GCT_LastTime = -1L;
		private static long GCT_BaseTime;
		private static uint GCT_LastCount;

		public static long GetCurrTime()
		{
#if false // 精度がいまいち
			return (long)GameWin32.GetTickCount64(); 
#else
			//uint currCount = (uint)GetNowCount_TEST(); // test
			uint currCount = (uint)DX.GetNowCount();

			if (currCount < GCT_LastCount) // ? DX.GetNowCount()のカンスト(オーバーフロー)
			{
				GCT_BaseTime += (long)uint.MaxValue + 1;
			}
			GCT_LastCount = currCount;
			long currTime = GCT_BaseTime + currCount;

			if (currTime < 0) throw null; // ? __int64のカンスト(オーバーフロー)
			if (currTime < GCT_LastTime) throw null; // ? 時間が戻った || カンスト(オーバーフロー)
			//if (GCT_LastTime != -1L && GCT_LastTime + 60000L < currTime) throw null; // ? 1分以上経過 <- 飛び過ぎ // タイトルバーを長時間掴んでいれば有り得る。

			GCT_LastTime = currTime;
			return currTime;
#endif
		}
	}
}
