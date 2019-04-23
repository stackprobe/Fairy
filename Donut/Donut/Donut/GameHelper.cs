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

		// ---- GameTools ----

		public static void CurtainEachFrame()
		{
			// TODO
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
