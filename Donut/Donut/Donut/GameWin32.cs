﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameWin32
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;
		}

		[DllImport("user32.dll")]
		public static extern bool ClientToScreen(IntPtr hWnd, out POINT lpPoint);

		public delegate bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern bool EnumWindows(EnumWindowsCallback callback, IntPtr lParam);

		[DllImport("user32.dll")]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder buff, int buffLenMax);

		public static string GetWindowTitleByHandle(IntPtr hWnd)
		{
			const int BUFF_SIZE = 1000;
			const int MARGIN = 10;

			StringBuilder buff = new StringBuilder(BUFF_SIZE + MARGIN);

			GetWindowText(hWnd, buff, BUFF_SIZE);

			return buff.ToString();
		}

		public static void EnumWindowsHandleTitle(Func<IntPtr, string, bool> routine)
		{
			EnumWindows((hWnd, lParam) => routine(hWnd, GetWindowTitleByHandle(hWnd)), IntPtr.Zero);
		}

		public static IntPtr GetMainWindowHandle()
		{
			string markTitle = Guid.NewGuid().ToString("B");
			IntPtr handle = IntPtr.Zero;
			bool handleFound = false;

			DX.SetMainWindowText(markTitle);

			EnumWindowsHandleTitle((hWnd, title) =>
			{
				if (title == markTitle)
				{
					handle = hWnd;
					handleFound = true;
					return false;
				}
				return true;
			});

			if (handleFound == false)
				throw new GameError();

			GameProcMain.SetMainWindowTitle();

			return handle;
		}

		[DllImport("kernel32")]
		public static extern UInt64 GetTickCount64();
	}
}
