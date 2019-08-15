using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	public static class DDDraw
	{
		// Extra >

		private class ExtraInfo
		{
			public DDTaskList TL = null;
			public bool BlendInv = false;
			public bool Mosaic = false;
			public bool IntPos = false;
			public bool IgnoreError = false;
			public int A = -1; // -1 == 無効
			public int BlendAdd = -1; // -1 == 無効
			public I3Color Bright = null;
		};

		private static ExtraInfo Extra = new ExtraInfo();

		public static void Reset()
		{
			Extra = new ExtraInfo();
		}

		public static void SetTaskList(DDTaskList tl)
		{
			Extra.TL = tl;
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

			pal = IntTools.Range(pal, 0, 255);

			Extra.A = pal;
		}

		public static void SetBlendAdd(double a)
		{
			int pal = DoubleTools.ToInt(a * 255.0);

			pal = IntTools.Range(pal, 0, 255);

			Extra.BlendAdd = pal;
		}

		public static void SetBright(double r, double g, double b)
		{
			int pR = DoubleTools.ToInt(r * 255.0);
			int pG = DoubleTools.ToInt(g * 255.0);
			int pB = DoubleTools.ToInt(b * 255.0);

			pR = IntTools.Range(pR, 0, 255);
			pG = IntTools.Range(pG, 0, 255);
			pB = IntTools.Range(pB, 0, 255);

			Extra.Bright = new I3Color(pR, pG, pB);
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

		private class DrawInfo
		{
			public DDPicture Picture;
			public ILayoutInfo Layout;
			public ExtraInfo Extra;
		}

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

		private static void DrawPicMain(DrawInfo info)
		{
			// app > @ enter DrawPicMain

			// < app

			if (info.Extra.A != -1)
			{
				SetBlend(DX.DX_BLENDMODE_ALPHA, info.Extra.A);
			}
			else if (info.Extra.BlendAdd != -1)
			{
				SetBlend(DX.DX_BLENDMODE_ADD, info.Extra.BlendAdd);
			}
			else if (info.Extra.BlendInv)
			{
				SetBlend(DX.DX_BLENDMODE_INVSRC, 255);
			}

			if (info.Extra.Mosaic)
			{
				DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
			}
			if (info.Extra.Bright != null)
			{
				SetBright(info.Extra.Bright.R, info.Extra.Bright.G, info.Extra.Bright.B);
			}

			{
				FreeInfo u = info.Layout as FreeInfo;

				if (u != null)
				{
					if (
						info.Extra.IntPos ?
						DX.DrawModiGraph(
							DoubleTools.ToInt(u.LTX),
							DoubleTools.ToInt(u.LTY),
							DoubleTools.ToInt(u.RTX),
							DoubleTools.ToInt(u.RTY),
							DoubleTools.ToInt(u.RBX),
							DoubleTools.ToInt(u.RBY),
							DoubleTools.ToInt(u.LBX),
							DoubleTools.ToInt(u.LBY),
							info.Picture.GetHandle(),
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
							info.Picture.GetHandle(),
							1
							)
							!= 0
						)
					// ? 失敗
					{
						if (info.Extra.IgnoreError == false)
							throw new DDError();
					}
					goto endDraw;
				}
			}

			{
				RectInfo u = info.Layout as RectInfo;

				if (u != null)
				{
					if (
						info.Extra.IntPos ?
						DX.DrawExtendGraph(
							DoubleTools.ToInt(u.L),
							DoubleTools.ToInt(u.T),
							DoubleTools.ToInt(u.R),
							DoubleTools.ToInt(u.B),
							info.Picture.GetHandle(),
							1
							)
							!= 0
						:
						DX.DrawExtendGraphF(
							(float)u.L,
							(float)u.T,
							(float)u.R,
							(float)u.B,
							info.Picture.GetHandle(),
							1
							)
							!= 0
						)
					// ? 失敗
					{
						if (info.Extra.IgnoreError == false)
							throw new DDError();
					}
					goto endDraw;
				}
			}

			{
				SimpleInfo u = info.Layout as SimpleInfo;

				if (u != null)
				{
					if (
						info.Extra.IntPos ?
						DX.DrawGraph(
							DoubleTools.ToInt(u.X),
							DoubleTools.ToInt(u.Y),
							info.Picture.GetHandle(),
							1
							)
							!= 0
						:
						DX.DrawGraphF(
							(float)u.X,
							(float)u.Y,
							info.Picture.GetHandle(),
							1
							)
							!= 0
						)
					// ? 失敗
					{
						if (info.Extra.IgnoreError == false)
							throw new DDError();
					}
					goto endDraw;
				}
			}

			throw new DDError(); // ? 不明なレイアウト
		endDraw:

			if (info.Extra.A != -1 || info.Extra.BlendAdd != -1 || info.Extra.BlendInv)
			{
				ResetBlend();
			}
			if (info.Extra.Mosaic)
			{
				DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
			}
			if (info.Extra.Bright != null)
			{
				ResetBright();
			}

			// app > @ leave DrawPicMain

			// < app
		}

		private class DrawPicTask : IDDTask
		{
			public DrawInfo Info;

			public bool Routine()
			{
				DrawPicMain(this.Info);
				return false;
			}

			public void Dispose()
			{
				// noop
			}
		}

		private static void DrawPic(DDPicture picture, ILayoutInfo layout_binding)
		{
			DrawInfo info = new DrawInfo()
			{
				Picture = picture,
				Layout = layout_binding,
				Extra = Extra,
			};

			if (Extra.TL == null)
			{
				DrawPicMain(info);
			}
			else
			{
				Extra.TL.Add(new DrawPicTask()
				{
					Info = info,
				});
			}
		}

		public static void DrawFree(DDPicture picture, double ltx, double lty, double rtx, double rty, double rbx, double rby, double lbx, double lby)
		{
			FreeInfo u = new FreeInfo()
			{
				LTX = ltx,
				LTY = lty,
				RTX = rtx,
				RTY = rty,
				RBX = rbx,
				RBY = rby,
				LBX = lbx,
				LBY = lby,
			};

			DrawPic(picture, u);
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


			RectInfo u = new RectInfo()
			{
				L = l,
				T = t,
				R = r,
				B = b,
			};

			DrawPic(picture, u);
		}

		public static void DrawRect(DDPicture picture, double l, double t, double w, double h)
		{
			DrawRect_LTRB(picture, l, t, l + w, t + h);
		}

		public static void DrawSimple(DDPicture picture, double x, double y)
		{
			if (
				x < -(double)IntTools.IMAX || (double)IntTools.IMAX < x ||
				y < -(double)IntTools.IMAX || (double)IntTools.IMAX < y
				)
				throw new DDError();

			SimpleInfo u = new SimpleInfo()
			{
				X = x,
				Y = y,
			};

			DrawPic(picture, u);
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

		private class DBInfo
		{
			public DDPicture Picture;
			public double X;
			public double Y;
			public FreeInfo Layout;
		}

		private static DBInfo DB = null;

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
			if (DB != null)
				throw new DDError();

			double w = picture.Get_W();
			double h = picture.Get_H();

			w /= 2.0;
			h /= 2.0;

			DB = new DBInfo()
			{
				Picture = picture,
				X = x,
				Y = y,
				Layout = new FreeInfo()
				{
					LTX = -w,
					LTY = -h,
					RTX = w,
					RTY = -h,
					RBX = w,
					RBY = h,
					LBX = -w,
					LBY = h,
				},
			};
		}

		public static void DrawSlide(double x, double y)
		{
			if (DB == null)
				throw new DDError();

			DB.Layout.LTX += x;
			DB.Layout.LTY += y;
			DB.Layout.RTX += x;
			DB.Layout.RTY += y;
			DB.Layout.RBX += x;
			DB.Layout.RBY += y;
			DB.Layout.LBX += x;
			DB.Layout.LBY += y;
		}

		public static void DrawRotate(double rot)
		{
			if (DB == null)
				throw new DDError();

			DDUtils.Rotate(ref DB.Layout.LTX, ref DB.Layout.LTY, rot);
			DDUtils.Rotate(ref DB.Layout.RTX, ref DB.Layout.RTY, rot);
			DDUtils.Rotate(ref DB.Layout.RBX, ref DB.Layout.RBY, rot);
			DDUtils.Rotate(ref DB.Layout.LBX, ref DB.Layout.LBY, rot);
		}

		public static void DrawZoom_X(double z)
		{
			if (DB == null)
				throw new DDError();

			DB.Layout.LTX *= z;
			DB.Layout.RTX *= z;
			DB.Layout.RBX *= z;
			DB.Layout.LBX *= z;
		}

		public static void DrawZoom_Y(double z)
		{
			if (DB == null)
				throw new DDError();

			DB.Layout.LTY *= z;
			DB.Layout.RTY *= z;
			DB.Layout.RBY *= z;
			DB.Layout.LBY *= z;
		}

		public static void DrawZoom(double z)
		{
			DrawZoom_X(z);
			DrawZoom_Y(z);
		}

		public static void DrawSetSize_W(double w)
		{
			if (DB == null)
				throw new DDError();

			w /= 2.0;

			DB.Layout.LTX = -w;
			DB.Layout.RTX = w;
			DB.Layout.RBX = w;
			DB.Layout.LBX = -w;
		}

		public static void DrawSetSize_H(double h)
		{
			if (DB == null)
				throw new DDError();

			h /= 2.0;

			DB.Layout.LTY = -h;
			DB.Layout.RTY = -h;
			DB.Layout.RBY = h;
			DB.Layout.LBY = h;
		}

		public static void DrawSetSize(double w, double h)
		{
			DrawSetSize_W(w);
			DrawSetSize_H(h);
		}

		public static void DrawEnd()
		{
			if (DB == null)
				throw new DDError();

			DB.Layout.LTX += DB.X;
			DB.Layout.LTY += DB.Y;
			DB.Layout.RTX += DB.X;
			DB.Layout.RTY += DB.Y;
			DB.Layout.RBX += DB.X;
			DB.Layout.RBY += DB.Y;
			DB.Layout.LBX += DB.X;
			DB.Layout.LBY += DB.Y;

			DrawPic(DB.Picture, DB.Layout);
			DB = null;
		}

		// < DrawBegin ～ DrawEnd
	}
}
