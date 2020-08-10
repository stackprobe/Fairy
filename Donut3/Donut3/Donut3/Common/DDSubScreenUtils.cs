﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using System.Drawing;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class DDSubScreenUtils
	{
		public static List<DDSubScreen> SubScreens = new List<DDSubScreen>();

		public static void Add(DDSubScreen subScreen)
		{
			SubScreens.Add(subScreen);
		}

		public static void Remove(DDSubScreen subScreen)
		{
			if (DDUtils.FastDesertElement(SubScreens, i => i == subScreen) == null) // ? Already removed
				throw new DDError();
		}

		public static void UnloadAll()
		{
			foreach (DDSubScreen subScreen in SubScreens)
				subScreen.Unload();
		}

		public static int CurrDrawScreenHandle = DX.DX_SCREEN_BACK;

		public static void ChangeDrawScreen(int handle)
		{
			if (DX.SetDrawScreen(handle) != 0) // ? 失敗
				throw new DDError();

			CurrDrawScreenHandle = handle;
		}

		public static void ChangeDrawScreen(DDSubScreen subScreen)
		{
			ChangeDrawScreen(subScreen.GetHandle());
		}

		public static void RestoreDrawScreen()
		{
			ChangeDrawScreen(DDGround.MainScreen != null ? DDGround.MainScreen.GetHandle() : DX.DX_SCREEN_BACK);
		}

		public static Size GetDrawScreenSize() // ret: 描画領域のサイズ？
		{
			int w;
			int h;
			int cbd;

			if (DX.GetScreenState(out w, out h, out cbd) != 0)
				throw new DDError();

			if (w < 1 || IntTools.IMAX < w)
				throw new DDError("w: " + w);

			if (h < 1 || IntTools.IMAX < h)
				throw new DDError("h: " + h);

			return new Size(w, h);
		}
	}
}
