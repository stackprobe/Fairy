
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using System.Drawing;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameSubScreenUtils
	{
		public static List<GameSubScreen> SubScreens = new List<GameSubScreen>();

		public static void Add(GameSubScreen subScreen)
		{
			SubScreens.Add(subScreen);
		}

		public static bool Remove(GameSubScreen subScreen) // ret: ? ! Already removed
		{
			return GameUtils.FastDesertElement(SubScreens, i => i == subScreen) != null;
		}

		public static void UnloadAll()
		{
			foreach (GameSubScreen subScreen in SubScreens)
			{
				subScreen.Unload();
			}
		}

		public static int CurrDrawScreenHandle = DX.DX_SCREEN_BACK;

		public static void ChangeDrawScreen(int handle)
		{
			if (DX.SetDrawScreen(handle) != 0) // ? 失敗
				throw new Exception("Failed to DX.SetDrawScreen()");

			CurrDrawScreenHandle = handle;
		}

		public static void ChangeDrawScreen(GameSubScreen subScreen)
		{
			ChangeDrawScreen(subScreen.GetHandle());
		}

		public static void RestoreDrawScreen()
		{
			ChangeDrawScreen(GameGround.MainScreen != null ? GameGround.MainScreen.GetHandle() : DX.DX_SCREEN_BACK);
		}

		public static Size GetDrawScreenSize() // ret: 描画領域のサイズ？
		{
			int w;
			int h;
			int cbd;

			if (DX.GetScreenState(out w, out h, out cbd) != 0)
				throw new Exception("Failed to DX.GetScreenState()");

			if (w < 1 || IntTools.IMAX < w)
				throw new Exception("Bad w: " + w);

			if (h < 1 || IntTools.IMAX < h)
				throw new Exception("Bad h: " + h);

			return new Size(w, h);
		}
	}
}
