using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public class GamePicture
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

			public void Dispose()
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

			if (w < 1 || IntTools.IMAX < w)
				throw new GameError();

			if (h < 1 || IntTools.IMAX < h)
				throw new GameError();
		}

		public static void GetGraphicHandleSize(int handle, out int w, out int h)
		{
			if (DX.GetGraphSize(handle, out w, out h) != 0)
				throw new GameError();

			if (w < 1 || IntTools.IMAX < w)
				throw new GameError();

			if (h < 1 || IntTools.IMAX < h)
				throw new GameError();
		}

		// TODO
	}
}
