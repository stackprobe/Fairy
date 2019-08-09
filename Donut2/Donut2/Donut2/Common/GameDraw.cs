using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	public static class GameDraw
	{
		// Extra >

		private class ExtraInfo
		{
			public GameTaskList TL = null;
			public bool BlendInv = false;
			public bool Mosaic = false;
			public bool IntPos = false;
			public bool IgnoreError = false;
			public int A = -1;
			public int BlendAdd = -1;
			public I3Color Bright = null;
		};

		private static ExtraInfo Extra = new ExtraInfo();

		public static void Reset()
		{
			Extra = new ExtraInfo();
		}

		public static void SetTaskList(GameTaskList tl)
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
			public GamePicture Picture;
			public ILayoutInfo Layout;
			public ExtraInfo Extra;
		}

		private static void SetBlend(int mode, int pal)
		{
			if (DX.SetDrawBlendMode(mode, pal) != 0) // ? 失敗
				throw new GameError();
		}

		private static void ResetBlend()
		{
			if (DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0) != 0) // ? 失敗
				throw new GameError();
		}

		private static void SetBright(int r, int g, int b)
		{
			if (DX.SetDrawBright(r, g, b) != 0) // ? 失敗
				throw new GameError();
		}

		private static void ResetBright()
		{
			if (DX.SetDrawBright(255, 255, 255) != 0) // ? 失敗
				throw new GameError();
		}

		private static void DrawPicMain(DrawInfo info)
		{
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
			if (Extra.Bright != null)
			{
				SetBright(Extra.Bright.R, Extra.Bright.G, Extra.Bright.B);
			}

			{
				FreeInfo u = info.Layout as FreeInfo;

				if (u != null)
				{
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
						if (Extra.IgnoreError == false)
							throw new GameError();
					}
					goto endDraw;
				}
			}

			{
				RectInfo u = info.Layout as RectInfo;

				if (u != null)
				{
					if (
						Extra.IntPos ?
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
						if (Extra.IgnoreError == false)
							throw new GameError();
					}
					goto endDraw;
				}
			}

			{
				SimpleInfo u = info.Layout as SimpleInfo;

				if (u != null)
				{
					if (
						Extra.IntPos ?
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
						if (Extra.IgnoreError == false)
							throw new GameError();
					}
					goto endDraw;
				}
			}

			throw new GameError(); // ? 不明なレイアウト
		endDraw:

			if (Extra.A != -1 || Extra.BlendAdd != -1 || Extra.BlendInv)
			{
				ResetBlend();
			}
			if (Extra.Mosaic)
			{
				DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
			}
			if (Extra.Bright != null)
			{
				ResetBright();
			}
		}

		private class DrawPicTask : IGameTask
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

		private static void DrawPic(GamePicture picture, ILayoutInfo layout_binding)
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

		public static void DrawFree(GamePicture picture, double ltx, double lty, double rtx, double rty, double rbx, double rby, double lbx, double lby)
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

		public static void DrawRect_LTRB(GamePicture picture, double l, double t, double r, double b)
		{
			if (
				l < -(double)IntTools.IMAX || (double)IntTools.IMAX - 1.0 < l ||
				t < -(double)IntTools.IMAX || (double)IntTools.IMAX - 1.0 < t ||
				r < l + 1.0 || (double)IntTools.IMAX < r ||
				b < t + 1.0 || (double)IntTools.IMAX < b
				)
				throw new GameError();


			RectInfo u = new RectInfo()
			{
				L = l,
				T = t,
				R = r,
				B = b,
			};

			DrawPic(picture, u);
		}

		public static void DrawRect(GamePicture picture, double l, double t, double w, double h)
		{
			DrawRect_LTRB(picture, l, t, l + w, t + h);
		}

		public static void DrawSimple(GamePicture picture, double x, double y)
		{
			if (
				x < -(double)IntTools.IMAX || (double)IntTools.IMAX < x ||
				y < -(double)IntTools.IMAX || (double)IntTools.IMAX < y
				)
				throw new GameError();

			SimpleInfo u = new SimpleInfo()
			{
				X = x,
				Y = y,
			};

			DrawPic(picture, u);
		}

		public static void DrawCenter(GamePicture picture, double x, double y)
		{
			if (
				x < -(double)IntTools.IMAX || (double)IntTools.IMAX < x ||
				y < -(double)IntTools.IMAX || (double)IntTools.IMAX < y
				)
				throw new GameError();

			DrawBegin(picture, x, y);
			DrawEnd();
		}

		// DrawBegin ～ DrawEnd >

		private class DBInfo
		{
			public GamePicture Picture;
			public double X;
			public double Y;
			public FreeInfo Layout;
		}

		private static DBInfo DB = null;

		public static void DrawBeginRect_LTRB(GamePicture picture, double l, double t, double r, double b)
		{
			DrawBeginRect(picture, l, t, r - l, b - t);
		}

		public static void DrawBeginRect(GamePicture picture, double l, double t, double w, double h)
		{
			DrawBegin(picture, l + w / 2.0, t + h / 2.0);
			DrawSetSize(w, h);
		}

		public static void DrawBegin(GamePicture picture, double x, double y)
		{
			if (DB != null)
				throw new GameError();

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
				throw new GameError();

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
				throw new GameError();

			GameUtils.Rotate(ref DB.Layout.LTX, ref DB.Layout.LTY, rot);
			GameUtils.Rotate(ref DB.Layout.RTX, ref DB.Layout.RTY, rot);
			GameUtils.Rotate(ref DB.Layout.RBX, ref DB.Layout.RBY, rot);
			GameUtils.Rotate(ref DB.Layout.LBX, ref DB.Layout.LBY, rot);
		}

		public static void DrawZoom_X(double z)
		{
			if (DB == null)
				throw new GameError();

			DB.Layout.LTX *= z;
			DB.Layout.RTX *= z;
			DB.Layout.RBX *= z;
			DB.Layout.LBX *= z;
		}

		public static void DrawZoom_Y(double z)
		{
			if (DB == null)
				throw new GameError();

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
				throw new GameError();

			w /= 2.0;

			DB.Layout.LTX = -w;
			DB.Layout.RTX = w;
			DB.Layout.RBX = w;
			DB.Layout.LBX = -w;
		}

		public static void DrawSetSize_H(double h)
		{
			if (DB == null)
				throw new GameError();

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
				throw new GameError();

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
