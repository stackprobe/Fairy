using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameDerivation
	{
		public class DerInfo
		{
			public int ParentPicId;
			public int X;
			public int Y;
			public int W;
			public int H;
		}

		private static List<DerInfo> DerList = new List<DerInfo>();

		public static void AddDer(int parentPicId, int x, int y, int w, int h)
		{
			DerList.Add(new DerInfo()
			{
				ParentPicId = parentPicId,
				X = x,
				Y = y,
				W = w,
				H = h,
			});
		}

		public class CreateDerList
		{
			public static void AddAnimation(int parentPicId, int x, int y, int w, int h, int xNum, int yNum, int xStep = -1, int yStep = -1)
			{
				if (xStep == -1) xStep = w;
				if (yStep == -1) yStep = h;

				for (int yc = 0; yc < yNum; yc++)
				{
					for (int xc = 0; xc < xNum; xc++)
					{
						AddDer(parentPicId, x + xc * xStep, y + yc * yStep, w, h);
					}
				}
			}
		}

		private static int LoadDer(GamePicture.PicInfo parent, int x, int y, int w, int h)
		{
			int der_h;

			if (
				x < 0 || IntTools.IMAX < x ||
				y < 0 || IntTools.IMAX < y ||
				w < 1 || IntTools.IMAX - x < w ||
				h < 1 || IntTools.IMAX - y < h
				)
				throw new DD.Error();

			// ? 範囲外
			if (
				parent.W < x + w ||
				parent.H < y + h
				)
				throw new DD.Error();

			der_h = DX.DerivationGraph(x, y, w, h, parent.Handle);

			if (der_h == -1) // ? 失敗
				throw new DD.Error();

			return der_h;
		}

		private static void UnloadDer(int handle)
		{
			if (handle == -1) // ? 未オープン
				return;

			if (DX.DeleteGraph(handle) != 0) // ? 失敗
				throw new DD.Error();
		}

		public static int Der(int derId)
		{
			return Der(derId, GamePicture.GetPicRes());
		}

		public static int Der(int derId, ResourceCluster<GamePicture.PicInfo> resclu)
		{
			if (derId < 0 || DerList.Count <= derId)
				throw new DD.Error();

			while (resclu.DerHandleList.Count <= derId)
				resclu.DerHandleList.Add(-1);

			int handle = resclu.DerHandleList[derId];

			if (handle == -1)
			{
				DerInfo i = DerList[derId];

				handle = LoadDer(
					resclu.GetHandle(i.ParentPicId),
					i.X,
					i.Y,
					i.W,
					i.H
					);

				resclu.DerHandleList[derId] = handle;
			}
			return handle;
		}

		public static int Der_W(int derId)
		{
			return DerList[derId].W;
		}

		public static int Der_H(int derId)
		{
			return DerList[derId].H;
		}

		public static void UnloadAllDer(List<int> derHandleList)
		{
			while (1 <= derHandleList.Count)
			{
				UnloadDer(ExtraTools.UnaddElement(derHandleList));
			}
		}
	}
}
