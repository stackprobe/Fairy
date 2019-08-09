using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	public class GameFont
	{
		public string FontName;
		public int FontSize;
		public int FontThick;
		public bool AntiAliasing;
		public int EdgeSize;
		public bool ItalicFlag;

		private int Handle = -1; // -1 == Unloaded

		public GameFont(string fontName, int fontSize, int fontThick = 6, bool antiAliasing = true, int edgeSize = 0, bool italicFlag = false)
		{
			if (string.IsNullOrEmpty(fontName)) throw new GameError();
			if (fontSize < 1 || IntTools.IMAX < fontSize) throw new GameError();
			if (fontThick < 1 || IntTools.IMAX < fontThick) throw new GameError();
			// antiAliasing
			if (edgeSize < 0 || IntTools.IMAX < edgeSize) throw new GameError();
			// italicFlag

			this.FontName = fontName;
			this.FontSize = fontSize;
			this.FontThick = fontThick;
			this.AntiAliasing = antiAliasing;
			this.EdgeSize = edgeSize;
			this.ItalicFlag = italicFlag;

			GameFontUtils.Add(this);
		}

		public int GetHandle()
		{
			if (this.Handle == -1)
			{
				int fontType = DX.DX_FONTTYPE_NORMAL;

				if (this.AntiAliasing)
					fontType |= DX.DX_FONTTYPE_ANTIALIASING;

				if (this.EdgeSize != 0)
					fontType |= DX.DX_FONTTYPE_ANTIALIASING;

				this.Handle = DX.CreateFontToHandle(
					this.FontName,
					this.FontSize,
					this.FontThick,
					fontType,
					-1,
					this.EdgeSize
					);

				if (this.Handle == -1) // ? 失敗
					throw new GameError();
			}
			return this.Handle;
		}

		public void Unload()
		{
			if (this.Handle != -1)
			{
				if (DX.DeleteFontToHandle(this.Handle) != 0) // ? 失敗
					throw new GameError();

				this.Handle = -1;
			}
		}
	}
}
