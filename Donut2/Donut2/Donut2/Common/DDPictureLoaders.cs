using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class DDPictureLoaders
	{
		public static DDPicture Standard(string file)
		{
			return new DDPicture(
				() => DDPictureLoaderUtils.GraphicHandle2Info(DDPictureLoaderUtils.SoftImage2GraphicHandle(DDPictureLoaderUtils.FileData2SoftImage(DDPictureLoaderUtils.File2FileData(file)))),
				DDPictureLoaderUtils.ReleaseInfo,
				DDPictureUtils.Add
				);
		}

		public static DDPicture Inverse(string file)
		{
			return new DDPicture(
				() =>
				{
					int siHandle = DDPictureLoaderUtils.FileData2SoftImage(DDPictureLoaderUtils.File2FileData(file));
					int w;
					int h;

					DDPictureLoaderUtils.GetSoftImageSize(siHandle, out w, out h);

					for (int x = 0; x < w; x++)
					{
						for (int y = 0; y < h; y++)
						{
							DDPictureLoaderUtils.Dot dot = DDPictureLoaderUtils.GetSoftImageDot(siHandle, x, y);

							dot.R ^= 0xff;
							dot.G ^= 0xff;
							dot.B ^= 0xff;

							DDPictureLoaderUtils.SetSoftImageDot(siHandle, x, y, dot);
						}
					}
					return DDPictureLoaderUtils.GraphicHandle2Info(DDPictureLoaderUtils.SoftImage2GraphicHandle(siHandle));
				},
				DDPictureLoaderUtils.ReleaseInfo,
				DDPictureUtils.Add
				);
		}

		public static DDPicture Mirror(string file)
		{
			return new DDPicture(
				() =>
				{
					int siHandle = DDPictureLoaderUtils.FileData2SoftImage(DDPictureLoaderUtils.File2FileData(file));
					int w;
					int h;

					DDPictureLoaderUtils.GetSoftImageSize(siHandle, out w, out h);

					{
						int h2 = DDPictureLoaderUtils.CreateSoftImage(w * 2, h);

						for (int x = 0; x < w; x++)
						{
							for (int y = 0; y < h; y++)
							{
								DDPictureLoaderUtils.Dot dot = DDPictureLoaderUtils.GetSoftImageDot(siHandle, x, y);

								DDPictureLoaderUtils.SetSoftImageDot(h2, x, y, dot);
								DDPictureLoaderUtils.SetSoftImageDot(h2, w * 2 - 1 - x, y, dot);
							}
						}
						DDPictureLoaderUtils.ReleaseSoftImage(siHandle);
						siHandle = h2;
					}

					return DDPictureLoaderUtils.GraphicHandle2Info(DDPictureLoaderUtils.SoftImage2GraphicHandle(siHandle));
				},
				DDPictureLoaderUtils.ReleaseInfo,
				DDPictureUtils.Add
				);
		}

		public static DDPicture BgTrans(string file)
		{
			return new DDPicture(
				() =>
				{
					int siHandle = DDPictureLoaderUtils.FileData2SoftImage(DDPictureLoaderUtils.File2FileData(file));
					int w;
					int h;

					DDPictureLoaderUtils.GetSoftImageSize(siHandle, out w, out h);

					DDPictureLoaderUtils.Dot targetDot = DDPictureLoaderUtils.GetSoftImageDot(siHandle, 0, 0); // 左上隅のピクセル

					for (int x = 0; x < w; x++)
					{
						for (int y = 0; y < h; y++)
						{
							DDPictureLoaderUtils.Dot dot = DDPictureLoaderUtils.GetSoftImageDot(siHandle, x, y);

							if (
								targetDot.R == dot.R &&
								targetDot.G == dot.G &&
								targetDot.B == dot.B
								)
							{
								dot.A = 0;

								DDPictureLoaderUtils.SetSoftImageDot(siHandle, x, y, dot);
							}
						}
					}
					return DDPictureLoaderUtils.GraphicHandle2Info(DDPictureLoaderUtils.SoftImage2GraphicHandle(siHandle));
				},
				DDPictureLoaderUtils.ReleaseInfo,
				DDPictureUtils.Add
				);
		}

		// 新しい画像ローダーをここへ追加...
	}
}
