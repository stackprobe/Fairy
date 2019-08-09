using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public static class GamePicture
	{
		public static int FileData2SoftImage(byte[] fileData) // fileData: unbind
		{
			int handle = -1;

			GameHelper.PinOn(fileData, p => handle = DX.LoadSoftImageToMem(p, fileData.Length));

			if (handle == -1)
				throw new GameError();

			int w;
			int h;

			DX.GetSoftImageSize(handle, out w, out h);

			// RGB -> RGBA
			{
				int h2 = DX.MakeARGB8ColorSoftImage(w, h);

				if (h2 == -1)
					throw new GameError();

				if (DX.BltSoftImage(0, 0, w, h, handle, 0, 0, h2) != 0)
					throw new GameError();

				if (DX.DeleteSoftImage(handle) != 0)
					throw new GameError();

				handle = h2;
			}

			return handle;
		}

		public static int SoftImage2GraphicHandle(int si_h) // si_h: bind
		{
			int h = DX.CreateGraphFromSoftImage(si_h);

			if (h == -1)
				throw new GameError();

			if (DX.DeleteSoftImage(si_h) != 0)
				throw new GameError();

			return h;
		}

		public class PicInfo : IDisposable
		{
			public int Handle;
			public int W;
			public int H;

			public void Dispose() // Codevil の Pic_ReleasePicInfo
			{
				if (DX.DeleteGraph(this.Handle) != 0)
					throw new GameError();
			}
		}

		public static PicInfo GraphicHandle2PicInfo(int handle) // handle: bind
		{
			int w;
			int h;

			GetGraphicHandleSize(handle, out w, out h);

			return new PicInfo()
			{
				Handle = handle,
				W = w,
				H = h,
			};
		}

		public static void GetSoftImageSize(int si_h, out int w, out int h)
		{
			if (DX.GetSoftImageSize(si_h, out w, out h) != 0)
				throw new GameError();

			if (
				w < 1 || IntTools.IMAX < w ||
				h < 1 || IntTools.IMAX < h
				)
				throw new GameError();
		}

		public static void GetGraphicHandleSize(int handle, out int w, out int h)
		{
			if (DX.GetGraphSize(handle, out w, out h) != 0)
				throw new GameError();

			if (
				w < 1 || IntTools.IMAX < w ||
				h < 1 || IntTools.IMAX < h
				)
				throw new GameError();
		}

		public class SIPixel
		{
			public int R;
			public int G;
			public int B;
			public int A;
		}

		public static SIPixel GetSIPixel(int si_h, int x, int y)
		{
			SIPixel i = new SIPixel();

			if (DX.GetPixelSoftImage(si_h, x, y, out i.R, out i.G, out i.B, out i.A) != 0)
				throw new GameError();

			if (
				i.R < 0 || 255 < i.R ||
				i.G < 0 || 255 < i.G ||
				i.B < 0 || 255 < i.B ||
				i.A < 0 || 255 < i.A
				)
				throw new GameError();

			return i;
		}

		public static void SetSIPixel(int si_h, int x, int y, SIPixel i)
		{
			i.R = IntTools.Range(i.R, 0, 255);
			i.G = IntTools.Range(i.G, 0, 255);
			i.B = IntTools.Range(i.B, 0, 255);
			i.A = IntTools.Range(i.A, 0, 255);

			if (DX.DrawPixelSoftImage(si_h, x, y, i.R, i.G, i.B, i.A) != 0)
				throw new GameError();
		}

		public static int CreateSoftImage(int w, int h)
		{
			if (
				w < 1 || IntTools.IMAX < w ||
				h < 1 || IntTools.IMAX < h
				)
				throw new GameError();

			int handle = DX.MakeARGB8ColorSoftImage(w, h);

			if (handle == -1)
				throw new GameError();

			return handle;
		}

		public static void ReleaseSoftImage(int si_h)
		{
			if (DX.DeleteSoftImage(si_h) != 0)
				throw new GameError();
		}

		public static void ReleaseGraphicHandle(int handle)
		{
			if (DX.DeleteGraph(handle) != 0)
				throw new GameError();
		}

		// Codevil の Pic_* ここまで

		private static List<ResourceCluster<PicInfo>> PicResList = new List<ResourceCluster<PicInfo>>();

		public static ResourceCluster<PicInfo> CreatePicRes(Func<byte[], PicInfo> handleLoader, Action<PicInfo> handleUnloader)
		{
			var res = new ResourceCluster<PicInfo>("Picture.dat", @"..\..\..\..\Picture.txt", handleLoader, handleUnloader);
			PicResList.Add(res);
			return res;
		}

		public static void UnloadAllPicResHandle() // スクリーンモード切り替え直前に呼ぶこと。
		{
			foreach (var res in PicResList)
			{
				res.UnloadAllHandle();
			}
		}

		private static ResourceCluster<PicInfo> CurrPicRes = null;

		public static void SetPicRes(ResourceCluster<PicInfo> resclu) // resclu: null == reset
		{
			CurrPicRes = resclu;
		}

		public static ResourceCluster<PicInfo> GetPicRes()
		{
			if (CurrPicRes == null)
				return GamePictureResource.StdPicRes.I;

			return CurrPicRes;
		}

		public static void ResetPicRes()
		{
			CurrPicRes = null;
		}

		public const int DTP = 0x40000000;

		public static int Pic(int picId)
		{
			if ((picId & DTP) != 0)
				return GameDerivation.Der(picId & ~DTP);

			return GetPicRes().GetHandle(picId).Handle;
		}

		public static int Pic_W(int picId)
		{
			if ((picId & DTP) != 0)
				return GameDerivation.Der_W(picId & ~DTP);

			return GetPicRes().GetHandle(picId).W;
		}

		public static int Pic_H(int picId)
		{
			if ((picId & DTP) != 0)
				return GameDerivation.Der_H(picId & ~DTP);

			return GetPicRes().GetHandle(picId).H;
		}
	}
}
