using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Charlotte.Donut
{
	public class GameWin32
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int x;
			public int y;
		}

		[DllImport("user32.dll")]
		public static extern bool ClientToScreen(IntPtr hwnd, out POINT lpPoint);

		public static IntPtr GetMainWindowHandle()
		{
			throw null; // TODO
		}
	}
}
