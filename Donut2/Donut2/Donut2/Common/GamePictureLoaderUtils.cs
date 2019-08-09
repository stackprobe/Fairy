using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	public static class GamePictureLoaderUtils
	{
		public static byte[] File2FileData(string file)
		{
			return GameResource.Load(file);
		}

		public static int FileData2SoftImage(byte[] fileData)
		{
			int siHandle = -1;

			GameSystem.PinOn(fileData, p => siHandle = DX.LoadSoftImageToMem(p, fileData.Length));

			if (siHandle == -1)
				throw new GameError();

			int w;
			int h;

			GetSoftImageSize(siHandle, out w, out h);

			// RGB -> RGBA
			{
				int h2 = DX.MakeARGB8ColorSoftImage(w, h);

				if (h2 == -1) // ? 失敗
					throw new GameError();

				if (DX.BltSoftImage(0, 0, w, h, siHandle, 0, 0, h2) != 0) // ? 失敗
					throw new GameError();

				if (DX.DeleteSoftImage(siHandle) != 0) // ? 失敗
					throw new GameError();

				siHandle = h2;
			}

			return siHandle;
		}

		public static int SoftImage2GraphicHandle(int siHandle_binding)
		{
			int gHandle = DX.CreateGraphFromSoftImage(siHandle_binding);

			if (gHandle == -1) // ? 失敗
				throw new GameError();

			if (DX.DeleteSoftImage(siHandle_binding) != 0) // ? 失敗
				throw new GameError();

			return gHandle;
		}

		public static GamePicture.PictureInfo GraphicHandle2Info(int gHandle_binding)
		{
			int w;
			int h;

			GetGraphicHandleSize(gHandle_binding, out w, out h);

			return new GamePicture.PictureInfo()
			{
				Handle = gHandle_binding,
				W = w,
				H = h,
			};
		}

		public static void GetSoftImageSize(int siHandle, out int w, out int h)
		{
			if (DX.GetSoftImageSize(siHandle, out w, out h) != 0)
				throw new GameError();

			if (
				w < 1 || IntTools.IMAX < w ||
				h < 1 || IntTools.IMAX < h
				)
				throw new GameError();
		}

		public static void GetGraphicHandleSize(int gHandle, out int w, out int h)
		{
			if (DX.GetGraphSize(gHandle, out w, out h) != 0)
				throw new GameError();

			if (
				w < 1 || IntTools.IMAX < w ||
				h < 1 || IntTools.IMAX < h
				)
				throw new GameError();
		}

		public class Dot
		{
			public int R;
			public int G;
			public int B;
			public int A;
		}

		public static Dot GetSoftImageDot(int siHandle, int x, int y)
		{
			Dot dot = new Dot();

			if (DX.GetPixelSoftImage(siHandle, x, y, out dot.R, out dot.G, out dot.B, out dot.A) != 0)
				throw new GameError();

			if (
				dot.R < 0 || 255 < dot.R ||
				dot.G < 0 || 255 < dot.G ||
				dot.B < 0 || 255 < dot.B ||
				dot.A < 0 || 255 < dot.A
				)
				throw new GameError();

			return dot;
		}

		public static void SetSoftImageDot(int siHandle, int x, int y, Dot dot)
		{
			dot.R = IntTools.Range(dot.R, 0, 255);
			dot.G = IntTools.Range(dot.G, 0, 255);
			dot.B = IntTools.Range(dot.B, 0, 255);
			dot.A = IntTools.Range(dot.A, 0, 255);

			if (DX.DrawPixelSoftImage(siHandle, x, y, dot.R, dot.G, dot.B, dot.A) != 0)
				throw new GameError();
		}

		public static int CreateSoftImage(int w, int h)
		{
			if (
				w < 1 || IntTools.IMAX < w ||
				h < 1 || IntTools.IMAX < h
				)
				throw new GameError();

			int siHandle = DX.MakeARGB8ColorSoftImage(w, h);

			if (siHandle == -1) // ? 失敗
				throw new GameError();

			return siHandle;
		}

		public static void ReleaseSoftImage(int siHandle)
		{
			if (DX.DeleteSoftImage(siHandle) != 0) // ? 失敗
				throw new GameError();
		}

		public static void ReleaseGraphicHandle(int gHandle)
		{
			if (DX.DeleteGraph(gHandle) != 0) // ? 失敗
				throw new GameError();
		}

		public static void ReleaseInfo(GamePicture.PictureInfo info)
		{
			ReleaseGraphicHandle(info.Handle);
		}
	}
}
