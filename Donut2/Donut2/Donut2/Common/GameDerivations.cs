using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	public static class GameDerivations
	{
		public static GamePicture GetPicture(GamePicture picture, int l, int t, int w, int h)
		{
			if (
				l < 0 || IntTools.IMAX < l ||
				t < 0 || IntTools.IMAX < t ||
				w < 1 || IntTools.IMAX - l < w ||
				h < 1 || IntTools.IMAX - t < h
				)
				throw new GameError();

			// ? 範囲外
			if (
				picture.Get_W() < l + w ||
				picture.Get_H() < t + h
				)
				throw new GameError();

			return new GamePicture(
				() =>
				{
					int handle = DX.DerivationGraph(l, t, w, h, picture.GetHandle());

					if (handle == -1) // ? 失敗
						throw new GameError();

					return new GamePicture.PictureInfo()
					{
						Handle = handle,
						W = w,
						H = h,
					};
				},
				GamePictureLoaderUtils.ReleaseInfo, // やる事同じなので共用しちゃう。
				GameDerivationUtils.Add
				);
		}
	}
}
