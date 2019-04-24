using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using DxLibDLL;

namespace Charlotte.Donut
{
	/// <summary>
	/// 少なくとも Codevil の GameTools, GameTools2, System, System2 をここにまとめる。
	/// Define
	/// </summary>
	public class GameHelper
	{
		public static void PinOn<T>(T data, Action<IntPtr> routine)
		{
			GCHandle pinnedData = GCHandle.Alloc(data, GCHandleType.Pinned);
			try
			{
				routine(pinnedData.AddrOfPinnedObject());
			}
			finally
			{
				pinnedData.Free();
			}
		}

		// ---- Define ----

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

		// ---- GameTools ----

		public static void CurtainEachFrame()
		{
			// TODO
		}

		// ---- GameTools2 ----

		private const int POUND_FIRST_DELAY = 17;
		private const int POUND_DELAY = 4;

		public static bool IsPound(int counter)
		{
			return counter == 1 || (POUND_FIRST_DELAY < counter && (counter - POUND_FIRST_DELAY) % POUND_DELAY == 1);
		}

		// ---- System ----

		public static bool IsWindowActive() // ret: ? このウィンドウはアクティブ
		{
			return DX.GetActiveFlag() != 0;
		}

		public static long GetCurrTime()
		{
			return (long)GameWin32.GetTickCount64();
		}

		// ----
	}
}
