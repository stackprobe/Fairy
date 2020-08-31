using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class DDDraw
	{
		// Extra >

		private class ExtraInfo
		{
			public bool BlendInv = false;
			public bool Mosaic = false;
			public bool IntPos = false;
			public bool IgnoreError = false;
			public int A = -1; // -1 == 無効
			public int BlendAdd = -1; // -1 == 無効
			public I3Color Bright = new I3Color(-1, 0, 0);
		};

		private static ExtraInfo Extra = new ExtraInfo();

		public static void Reset()
		{
			Extra = new ExtraInfo();
		}

		public static void SetBlendInv()
		{
			Extra.BlendInv = true;
		}

		public static void SetMosaic()
		{
			Extra.Mosaic = true;
		}

		public static void SetIntPos()
		{
			Extra.IntPos = true;
		}

		public static void SetIgnoreError()
		{
			Extra.IgnoreError = true;
		}

		public static void SetAlpha(double a)
		{
			int pal = DoubleTools.ToInt(a * 255.0);

			pal = IntTools.ToRange(pal, 0, 255);

			Extra.A = pal;
		}

		public static void SetBlendAdd(double a)
		{
			int pal = DoubleTools.ToInt(a * 255.0);

			pal = IntTools.ToRange(pal, 0, 255);

			Extra.BlendAdd = pal;
		}

		public static void SetBright(double r, double g, double b)
		{
			int pR = DoubleTools.ToInt(r * 255.0);
			int pG = DoubleTools.ToInt(g * 255.0);
			int pB = DoubleTools.ToInt(b * 255.0);

			pR = IntTools.ToRange(pR, 0, 255);
			pG = IntTools.ToRange(pG, 0, 255);
			pB = IntTools.ToRange(pB, 0, 255);

			Extra.Bright = new I3Color(pR, pG, pB);
		}

		public static void SetBright(I3Color color)
		{
			color.R = IntTools.ToRange(color.R, 0, 255);
			color.G = IntTools.ToRange(color.G, 0, 255);
			color.B = IntTools.ToRange(color.B, 0, 255);

			Extra.Bright = color;
		}

		// < Extra

		private interface ILayoutInfo
		{ }

		private class FreeInfo : ILayoutInfo
		{
			public double LTX;
			public double LTY;
			public double RTX;
			public double RTY;
			public double RBX;
			public double RBY;
			public double LBX;
			public double LBY;
		}

		private class RectInfo : ILayoutInfo
		{
			public double L;
			public double T;
			public double R;
			public double B;
		}

		private class SimpleInfo : ILayoutInfo
		{
			public double X;
			public double Y;
		}

		private static FreeInfo FreeLayout = new FreeInfo();
		private static RectInfo RectLayout = new RectInfo();
		private static SimpleInfo SimpleLayout = new SimpleInfo();

		private static void SetBlend(int mode, int pal)
		{
			if (DX.SetDrawBlendMode(mode, pal) != 0) // ? 失敗
				throw new DDError();
		}

		private static void ResetBlend()
		{
			if (DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0) != 0) // ? 失敗
				throw new DDError();
		}

		private static void SetBright(int r, int g, int b)
		{
			if (DX.SetDrawBright(r, g, b) != 0) // ? 失敗
				throw new DDError();
		}

		private static void ResetBright()
		{
			if (DX.SetDrawBright(255, 255, 255) != 0) // ? 失敗
				throw new DDError();
		}

		private static void DrawPic(DDPicture picture, ILayoutInfo layout)
		{
			// app > @ enter DrawPic

			// < app

			if (Extra.A != -1)
			{
				SetBlend(DX.DX_BLENDMODE_ALPHA, Extra.A);
			}
			else if (Extra.BlendAdd != -1)
			{
				SetBlend(DX.DX_BLENDMODE_ADD, Extra.BlendAdd);
			}
			else if (Extra.BlendInv)
			{
				SetBlend(DX.DX_BLENDMODE_INVSRC, 255);
			}

			if (Extra.Mosaic)
			{
				DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
			}
			if (Extra.Bright.R != -1)
			{
				SetBright(Extra.Bright.R, Extra.Bright.G, Extra.Bright.B);
			}

			{
				FreeInfo u = layout as FreeInfo;

				if (u != null)
				{
					// ? 失敗
					if (
						Extra.IntPos ?
						DX.DrawModiGraph(
							DoubleTools.ToInt(u.LTX),
							DoubleTools.ToInt(u.LTY),
							DoubleTools.ToInt(u.RTX),
							DoubleTools.ToInt(u.RTY),
							DoubleTools.ToInt(u.RBX),
							DoubleTools.ToInt(u.RBY),
							DoubleTools.ToInt(u.LBX),
							DoubleTools.ToInt(u.LBY),
							picture.GetHandle(),
							1
							)
							!= 0
						:
						DX.DrawModiGraphF(
							(float)u.LTX,
							(float)u.LTY,
							(float)u.RTX,
							(float)u.RTY,
							(float)u.RBX,
							(float)u.RBY,
							(float)u.LBX,
							(float)u.LBY,
							picture.GetHandle(),
							1
							)
							!= 0
						)
					{
						if (Extra.IgnoreError == false)
							throw new DDError();
					}
					goto endDraw;
				}
			}

			{
				RectInfo u = layout as RectInfo;

				if (u != null)
				{
					// ? 失敗
					if (
						Extra.IntPos ?
						DX.DrawExtendGraph(
							DoubleTools.ToInt(u.L),
							DoubleTools.ToInt(u.T),
							DoubleTools.ToInt(u.R),
							DoubleTools.ToInt(u.B),
							picture.GetHandle(),
							1
							)
							!= 0
						:
						DX.DrawExtendGraphF(
							(float)u.L,
							(float)u.T,
							(float)u.R,
							(float)u.B,
							picture.GetHandle(),
							1
							)
							!= 0
						)
					{
						if (Extra.IgnoreError == false)
							throw new DDError();
					}
					goto endDraw;
				}
			}

			{
				SimpleInfo u = layout as SimpleInfo;

				if (u != null)
				{
					// ? 失敗
					if (
						Extra.IntPos ?
						DX.DrawGraph(
							DoubleTools.ToInt(u.X),
							DoubleTools.ToInt(u.Y),
							picture.GetHandle(),
							1
							)
							!= 0
						:
						DX.DrawGraphF(
							(float)u.X,
							(float)u.Y,
							picture.GetHandle(),
							1
							)
							!= 0
						)
					{
						if (Extra.IgnoreError == false)
							throw new DDError();
					}
					goto endDraw;
				}
			}

			throw new DDError(); // ? 不明なレイアウト
		endDraw:

			if (Extra.A != -1 || Extra.BlendAdd != -1 || Extra.BlendInv)
			{
				ResetBlend();
			}
			if (Extra.Mosaic)
			{
				DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
			}
			if (Extra.Bright.R != -1)
			{
				ResetBright();
			}

			// app > @ leave DrawPic

			// < app
		}

		public static void DrawFree(DDPicture picture, double ltx, double lty, double rtx, double rty, double rbx, double rby, double lbx, double lby)
		{
			FreeLayout.LTX = ltx;
			FreeLayout.LTY = lty;
			FreeLayout.RTX = rtx;
			FreeLayout.RTY = rty;
			FreeLayout.RBX = rbx;
			FreeLayout.RBY = rby;
			FreeLayout.LBX = lbx;
			FreeLayout.LBY = lby;

			DrawPic(picture, FreeLayout);
		}

		public static void DrawFree(DDPicture picture, D2Point lt, D2Point rt, D2Point rb, D2Point lb)
		{
			DrawFree(picture, lt.X, lt.Y, rt.X, rt.Y, rb.X, rb.Y, lb.X, lb.Y);
		}

		public static void DrawFree(DDPicture picture, P4Poly poly)
		{
			DrawFree(picture, poly.LT, poly.RT, poly.RB, poly.LB);
		}

		public static void DrawRect_LTRB(DDPicture picture, double l, double t, double r, double b)
		{
			if (
				l < -(double)IntTools.IMAX || (double)IntTools.IMAX - 1.0 < l ||
				t < -(double)IntTools.IMAX || (double)IntTools.IMAX - 1.0 < t ||
				r < l + 1.0 || (double)IntTools.IMAX < r ||
				b < t + 1.0 || (double)IntTools.IMAX < b
				)
				throw new DDError();

			RectLayout.L = l;
			RectLayout.T = t;
			RectLayout.R = r;
			RectLayout.B = b;

			DrawPic(picture, RectLayout);
		}

		public static void DrawRect(DDPicture picture, double l, double t, double w, double h)
		{
			DrawRect_LTRB(picture, l, t, l + w, t + h);
		}

		public static void DrawRect(DDPicture picture, D4Rect rect)
		{
			DrawRect(picture, rect.L, rect.T, rect.W, rect.H);
		}

		public static void DrawSimple(DDPicture picture, double x, double y)
		{
			if (
				x < -(double)IntTools.IMAX || (double)IntTools.IMAX < x ||
				y < -(double)IntTools.IMAX || (double)IntTools.IMAX < y
				)
				throw new DDError();

			SimpleLayout.X = x;
			SimpleLayout.Y = y;

			DrawPic(picture, SimpleLayout);
		}

		public static void DrawCenter(DDPicture picture, double x, double y)
		{
			if (
				x < -(double)IntTools.IMAX || (double)IntTools.IMAX < x ||
				y < -(double)IntTools.IMAX || (double)IntTools.IMAX < y
				)
				throw new DDError();

			DrawBegin(picture, x, y);
			DrawEnd();
		}

		// DrawBegin ～ DrawEnd >

		private struct DBInfo
		{
			public DDPicture Picture; // null == 無効
			public double X;
			public double Y;
		}

		private static DBInfo DB = new DBInfo();

		public static void DrawBeginRect_LTRB(DDPicture picture, double l, double t, double r, double b)
		{
			DrawBeginRect(picture, l, t, r - l, b - t);
		}

		public static void DrawBeginRect(DDPicture picture, double l, double t, double w, double h)
		{
			DrawBegin(picture, l + w / 2.0, t + h / 2.0);
			DrawSetSize(w, h);
		}

		public static void DrawBegin(DDPicture picture, double x, double y)
		{
			if (DB.Picture != null)
				throw new DDError();

			double w = picture.Get_W();
			double h = picture.Get_H();

			w /= 2.0;
			h /= 2.0;

			DB.Picture = picture;
			DB.X = x;
			DB.Y = y;

			FreeLayout.LTX = -w;
			FreeLayout.LTY = -h;
			FreeLayout.RTX = w;
			FreeLayout.RTY = -h;
			FreeLayout.RBX = w;
			FreeLayout.RBY = h;
			FreeLayout.LBX = -w;
			FreeLayout.LBY = h;
		}

		public static void DrawSlide(double x, double y)
		{
			if (DB.Picture == null)
				throw new DDError();

			FreeLayout.LTX += x;
			FreeLayout.LTY += y;
			FreeLayout.RTX += x;
			FreeLayout.RTY += y;
			FreeLayout.RBX += x;
			FreeLayout.RBY += y;
			FreeLayout.LBX += x;
			FreeLayout.LBY += y;
		}

		public static void DrawRotate(double rot)
		{
			if (DB.Picture == null)
				throw new DDError();

			DDUtils.Rotate(ref FreeLayout.LTX, ref FreeLayout.LTY, rot);
			DDUtils.Rotate(ref FreeLayout.RTX, ref FreeLayout.RTY, rot);
			DDUtils.Rotate(ref FreeLayout.RBX, ref FreeLayout.RBY, rot);
			DDUtils.Rotate(ref FreeLayout.LBX, ref FreeLayout.LBY, rot);
		}

		public static void DrawZoom_X(double z)
		{
			if (DB.Picture == null)
				throw new DDError();

			FreeLayout.LTX *= z;
			FreeLayout.RTX *= z;
			FreeLayout.RBX *= z;
			FreeLayout.LBX *= z;
		}

		public static void DrawZoom_Y(double z)
		{
			if (DB.Picture == null)
				throw new DDError();

			FreeLayout.LTY *= z;
			FreeLayout.RTY *= z;
			FreeLayout.RBY *= z;
			FreeLayout.LBY *= z;
		}

		public static void DrawZoom(double z)
		{
			DrawZoom_X(z);
			DrawZoom_Y(z);
		}

		public static void DrawSetSize_W(double w)
		{
			if (DB.Picture == null)
				throw new DDError();

			w /= 2.0;

			FreeLayout.LTX = -w;
			FreeLayout.RTX = w;
			FreeLayout.RBX = w;
			FreeLayout.LBX = -w;
		}

		public static void DrawSetSize_H(double h)
		{
			if (DB.Picture == null)
				throw new DDError();

			h /= 2.0;

			FreeLayout.LTY = -h;
			FreeLayout.RTY = -h;
			FreeLayout.RBY = h;
			FreeLayout.LBY = h;
		}

		public static void DrawSetSize(double w, double h)
		{
			DrawSetSize_W(w);
			DrawSetSize_H(h);
		}

		public static void DrawEnd()
		{
			if (DB.Picture == null)
				throw new DDError();

			FreeLayout.LTX += DB.X;
			FreeLayout.LTY += DB.Y;
			FreeLayout.RTX += DB.X;
			FreeLayout.RTY += DB.Y;
			FreeLayout.RBX += DB.X;
			FreeLayout.RBY += DB.Y;
			FreeLayout.LBX += DB.X;
			FreeLayout.LBY += DB.Y;

			DrawPic(DB.Picture, FreeLayout);

			DB.Picture = null;
		}

		// < DrawBegin ～ DrawEnd
	}
}
