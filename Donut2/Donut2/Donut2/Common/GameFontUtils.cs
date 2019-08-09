using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameFontUtils
	{
		public static List<GameFont> Fonts = new List<GameFont>();

		public static void Add(GameFont font)
		{
			Fonts.Add(font);
		}

		public static void UnloadAll()
		{
			foreach (GameFont font in Fonts)
				font.Unload();
		}

		public static GameFont GetFont(string fontName, int fontSize, int fontThick = 6, bool antiAliasing = true, int edgeSize = 0, bool italicFlag = false)
		{
			GameFont font = Fonts.FirstOrDefault(v =>
				v.FontName == fontName &&
				v.FontSize == fontSize &&
				v.FontThick == fontThick &&
				v.AntiAliasing == antiAliasing &&
				v.EdgeSize == edgeSize &&
				v.ItalicFlag == italicFlag
				);

			if (font == null)
				font = new GameFont(fontName, fontSize, fontThick, antiAliasing, edgeSize, italicFlag);

			return font;
		}
	}
}
