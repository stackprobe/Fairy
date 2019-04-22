using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GamePictureResource
	{
		private static GamePicture.PicInfo LoadPic(byte[] rawData)
		{
			return GamePicture.GraphicHandle2PicInfo(GamePicture.SoftImage2GraphicHandle(GamePicture.FileData2SoftImage(rawData)));
		}

		private static void UnloadPic(GamePicture.PicInfo handle)
		{
			handle.Dispose();
		}

		public static OneObject<ResourceCluster<GamePicture.PicInfo>> StdPicRes = GameHelper.GetOneObject(() =>
		{
			return GamePicture.CreatePicRes(LoadPic, UnloadPic);
		});

		private static GamePicture.PicInfo LoadInvPic(byte[] rawData)
		{
			int si_h = GamePicture.FileData2SoftImage(rawData);
			int w;
			int h;

			GamePicture.GetSoftImageSize(si_h, out w, out h);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					GamePicture.SIPixel pixel = GamePicture.GetSIPixel(si_h, x, y);

					pixel.R ^= 0xff;
					pixel.G ^= 0xff;
					pixel.B ^= 0xff;

					GamePicture.SetSIPixel(si_h, x, y, pixel);
				}
			}
			return GamePicture.GraphicHandle2PicInfo(GamePicture.SoftImage2GraphicHandle(si_h));
		}

		public static OneObject<ResourceCluster<GamePicture.PicInfo>> InvPicRes = GameHelper.GetOneObject(() =>
		{
			return GamePicture.CreatePicRes(LoadInvPic, UnloadPic);
		});

		private static GamePicture.PicInfo LoadMirrorPic(byte[] rawData)
		{
			int si_h = GamePicture.FileData2SoftImage(rawData);
			int w;
			int h;

			GamePicture.GetSoftImageSize(si_h, out w, out h);

			int new_si_h = GamePicture.CreateSoftImage(w * 2, h);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					GamePicture.SIPixel pixel = GamePicture.GetSIPixel(si_h, x, y);

					GamePicture.SetSIPixel(new_si_h, x, y, pixel);
					GamePicture.SetSIPixel(new_si_h, x * 2 - 1 - x, y, pixel);
				}
			}
			GamePicture.ReleaseSoftImage(si_h);
			return GamePicture.GraphicHandle2PicInfo(GamePicture.SoftImage2GraphicHandle(new_si_h));
		}

		public static OneObject<ResourceCluster<GamePicture.PicInfo>> MirrorPicRes = GameHelper.GetOneObject(() =>
		{
			return GamePicture.CreatePicRes(LoadMirrorPic, UnloadPic);
		});
	}
}
