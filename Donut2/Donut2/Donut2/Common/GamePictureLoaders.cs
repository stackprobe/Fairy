using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GamePictureLoaders
	{
		public static GamePicture Standard(string file)
		{
			return new GamePicture(
				() => GamePictureLoaderUtils.GraphicHandle2Info(GamePictureLoaderUtils.SoftImage2GraphicHandle(GamePictureLoaderUtils.FileData2SoftImage(GamePictureLoaderUtils.File2FileData(file)))),
				GamePictureLoaderUtils.ReleaseInfo,
				GamePictureUtils.Add
				);
		}

		public static GamePicture Inverse(string file)
		{
			return new GamePicture(
				() =>
				{
					int siHandle = GamePictureLoaderUtils.FileData2SoftImage(GamePictureLoaderUtils.File2FileData(file));
					int w;
					int h;

					GamePictureLoaderUtils.GetSoftImageSize(siHandle, out w, out h);

					for (int x = 0; x < w; x++)
					{
						for (int y = 0; y < h; y++)
						{
							GamePictureLoaderUtils.Dot dot = GamePictureLoaderUtils.GetSoftImageDot(siHandle, x, y);

							dot.R ^= 0xff;
							dot.G ^= 0xff;
							dot.B ^= 0xff;

							GamePictureLoaderUtils.SetSoftImageDot(siHandle, x, y, dot);
						}
					}
					return GamePictureLoaderUtils.GraphicHandle2Info(GamePictureLoaderUtils.SoftImage2GraphicHandle(siHandle));
				},
				GamePictureLoaderUtils.ReleaseInfo,
				GamePictureUtils.Add
				);
		}

		public static GamePicture Mirror(string file)
		{
			return new GamePicture(
				() =>
				{
					int siHandle = GamePictureLoaderUtils.FileData2SoftImage(GamePictureLoaderUtils.File2FileData(file));
					int w;
					int h;

					GamePictureLoaderUtils.GetSoftImageSize(siHandle, out w, out h);

					{
						int h2 = GamePictureLoaderUtils.CreateSoftImage(w * 2, h);

						for (int x = 0; x < w; x++)
						{
							for (int y = 0; y < h; y++)
							{
								GamePictureLoaderUtils.Dot dot = GamePictureLoaderUtils.GetSoftImageDot(siHandle, x, y);

								GamePictureLoaderUtils.SetSoftImageDot(h2, x, y, dot);
								GamePictureLoaderUtils.SetSoftImageDot(h2, w * 2 - 1 - x, y, dot);
							}
						}
						GamePictureLoaderUtils.ReleaseSoftImage(siHandle);
						siHandle = h2;
					}

					return GamePictureLoaderUtils.GraphicHandle2Info(GamePictureLoaderUtils.SoftImage2GraphicHandle(siHandle));
				},
				GamePictureLoaderUtils.ReleaseInfo,
				GamePictureUtils.Add
				);
		}

		public static GamePicture BgTrans(string file)
		{
			return new GamePicture(
				() =>
				{
					int siHandle = GamePictureLoaderUtils.FileData2SoftImage(GamePictureLoaderUtils.File2FileData(file));
					int w;
					int h;

					GamePictureLoaderUtils.GetSoftImageSize(siHandle, out w, out h);

					GamePictureLoaderUtils.Dot targetDot = GamePictureLoaderUtils.GetSoftImageDot(siHandle, 0, 0); // 左上隅のピクセル

					for (int x = 0; x < w; x++)
					{
						for (int y = 0; y < h; y++)
						{
							GamePictureLoaderUtils.Dot dot = GamePictureLoaderUtils.GetSoftImageDot(siHandle, x, y);

							if (
								targetDot.R == dot.R &&
								targetDot.G == dot.G &&
								targetDot.B == dot.B
								)
							{
								dot.A = 0;

								GamePictureLoaderUtils.SetSoftImageDot(siHandle, x, y, dot);
							}
						}
					}
					return GamePictureLoaderUtils.GraphicHandle2Info(GamePictureLoaderUtils.SoftImage2GraphicHandle(siHandle));
				},
				GamePictureLoaderUtils.ReleaseInfo,
				GamePictureUtils.Add
				);
		}

		// 新しい画像ローダーをここへ追加...
	}
}
