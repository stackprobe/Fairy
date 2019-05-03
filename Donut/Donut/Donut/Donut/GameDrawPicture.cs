using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameDrawPicture
	{
		public class ExtraInfo
		{
			public TaskList TL;
			public ResourceCluster<GamePicture.PicInfo> PicRes;
			public bool BlendInvOn;
			public bool MosaicOn;
			public bool IntPosOn;
			public bool IgnoreError;

			// change by this.*() -->

			public double A;
			public bool AlphaOn;
			public bool BlendAddOn;
			public double R;
			public double G;
			public double B;
			public bool BrightOn;
			public bool GraphicHandleFlag;
			public I2Size GraphicSize; // GraphicHandleFlag == true のとき DrawCenter(), DrawBegin() で必要。

			public void SetAlpha(double a)
			{
				this.A = a;
				this.AlphaOn = true;
			}

			public void SetBlendAdd(double a)
			{
				this.A = a;
				this.BlendAddOn = true;
			}

			public void SetBright(double cR, double cG, double cB)
			{
				this.R = cR;
				this.G = cG;
				this.B = cB;
				this.BrightOn = true;
			}

			public void SetBright(uint color)
			{
				int r = (int)((color >> 16) & 0xff);
				int g = (int)((color >> 8) & 0xff);
				int b = (int)((color >> 0) & 0xff);

				this.SetBright((double)r / 0xff, (double)g / 0xff, (double)b / 0xff);
			}

			public void SetGraphicSize(I2Size size)
			{
				this.GraphicHandleFlag = true;
				this.GraphicSize = size;
			}

			public void Reset()
			{
				DPE = new ExtraInfo();
			}
		}

		public static ExtraInfo DPE = new ExtraInfo();

		public class LayoutInfo
		{
			public class FreeInfo
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

			public class RectInfo
			{
				public double L;
				public double T;
				public double R;
				public double B;
			}

			public class SimpleInfo
			{
				public double X;
				public double Y;
			}

			public char Mode; // "FRS"
			public object Entity;
		}

		public class ParamInfo
		{
			public int PicId;
			public LayoutInfo Layout;
			public ExtraInfo Extra;
		}

		private static void SetBlend(int mode, double a)
		{
			a = DoubleTools.Range(a, 0.0, 1.0);

			int pal = DoubleTools.ToInt(a * 255.0);

			if (pal < 0 || 255 < pal)
				throw new GameError();

			if (DX.SetDrawBlendMode(mode, pal) != 0) // ? 失敗
				throw new GameError();
		}

		private static void ResetBlend()
		{
			if (DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0) != 0) // ? 失敗
				throw new GameError();
		}

		private static void SetBright(double cR, double cG, double cB) // (cR, cG, cB): 0.0 - 1.0 == 暗～明
		{
			// 0 - 255 と間違えている etc.
			//
			if (cR < -0.5 || 1.5 < cR) throw new GameError();
			if (cG < -0.5 || 1.5 < cG) throw new GameError();
			if (cB < -0.5 || 1.5 < cB) throw new GameError();

			cR = DoubleTools.Range(cR, 0.0, 1.0);
			cG = DoubleTools.Range(cG, 0.0, 1.0);
			cB = DoubleTools.Range(cB, 0.0, 1.0);

			int palR = DoubleTools.ToInt(cR * 255.0);
			int palG = DoubleTools.ToInt(cG * 255.0);
			int palB = DoubleTools.ToInt(cB * 255.0);

			if (palR < 0 || 255 < palR) throw new GameError();
			if (palG < 0 || 255 < palG) throw new GameError();
			if (palB < 0 || 255 < palB) throw new GameError();

			if (DX.SetDrawBright(palR, palG, palB) != 0) // ? 失敗
				throw new GameError();
		}

		private static void ResetBright()
		{
			if (DX.SetDrawBright(255, 255, 255) != 0) // ? 失敗
				throw new GameError();
		}

		private static bool DrawPicFunc(ParamInfo i)
		{
			if (i.Extra.PicRes != null)
			{
				GamePicture.SetPicRes(i.Extra.PicRes);
			}
			if (i.Extra.AlphaOn)
			{
				SetBlend(DX.DX_BLENDMODE_ALPHA, i.Extra.A);
			}
			else if (i.Extra.BlendAddOn)
			{
				SetBlend(DX.DX_BLENDMODE_ADD, i.Extra.A);
			}
			else if (i.Extra.BlendInvOn)
			{
				SetBlend(DX.DX_BLENDMODE_INVSRC, 1.0);
			}
			if (i.Extra.MosaicOn)
			{
				DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
			}
			if (i.Extra.BrightOn)
			{
				SetBright(i.Extra.R, i.Extra.G, i.Extra.B);
			}

			int grphHdl;

			if (i.Extra.GraphicHandleFlag)
				grphHdl = i.PicId;
			else
				grphHdl = GamePicture.Pic(i.PicId);

			switch (i.Layout.Mode)
			{
				case 'F':
					{
						LayoutInfo.FreeInfo u = (LayoutInfo.FreeInfo)i.Layout.Entity;
						const double F_ZURE = 0.0;

						if (i.Extra.IntPosOn ?
							DX.DrawModiGraph(
								DoubleTools.ToInt(u.LTX),
								DoubleTools.ToInt(u.LTY),
								DoubleTools.ToInt(u.RTX),
								DoubleTools.ToInt(u.RTY),
								DoubleTools.ToInt(u.RBX),
								DoubleTools.ToInt(u.RBY),
								DoubleTools.ToInt(u.LBX),
								DoubleTools.ToInt(u.LBY),
								grphHdl,
								1
								)
								!= 0
							:
							DX.DrawModiGraphF(
								(float)(u.LTX + F_ZURE),
								(float)(u.LTY + F_ZURE),
								(float)(u.RTX + F_ZURE),
								(float)(u.RTY + F_ZURE),
								(float)(u.RBX + F_ZURE),
								(float)(u.RBY + F_ZURE),
								(float)(u.LBX + F_ZURE),
								(float)(u.LBY + F_ZURE),
								grphHdl,
								1
								)
								!= 0
							) // ? 失敗
						{
							if (i.Extra.IgnoreError == false)
							{
								ProcMain.WriteLog(u.LTX);
								ProcMain.WriteLog(u.LTY);
								ProcMain.WriteLog(u.RTX);
								ProcMain.WriteLog(u.RTY);
								ProcMain.WriteLog(u.RBX);
								ProcMain.WriteLog(u.RBY);
								ProcMain.WriteLog(u.LBX);
								ProcMain.WriteLog(u.LBY);
								ProcMain.WriteLog(i.PicId);
								ProcMain.WriteLog(grphHdl);

								throw new GameError();
							}
						}
					}
					break;

				case 'R':
					{
						LayoutInfo.RectInfo u = (LayoutInfo.RectInfo)i.Layout.Entity;

						if (i.Extra.IntPosOn ?
							DX.DrawExtendGraph(
								DoubleTools.ToInt(u.L),
								DoubleTools.ToInt(u.T),
								DoubleTools.ToInt(u.R),
								DoubleTools.ToInt(u.B),
								grphHdl,
								1
								)
								!= 0
							:
							DX.DrawExtendGraphF(
								(float)u.L,
								(float)u.T,
								(float)u.R,
								(float)u.B,
								grphHdl,
								1
								)
								!= 0
							) // ? 失敗
						{
							if (i.Extra.IgnoreError == false)
							{
								ProcMain.WriteLog(u.L);
								ProcMain.WriteLog(u.T);
								ProcMain.WriteLog(u.R);
								ProcMain.WriteLog(u.B);
								ProcMain.WriteLog(i.PicId);
								ProcMain.WriteLog(grphHdl);

								throw new GameError();
							}
						}
					}
					break;

				case 'S':
					{
						LayoutInfo.SimpleInfo u = (LayoutInfo.SimpleInfo)i.Layout.Entity;

						if (i.Extra.IntPosOn ?
							DX.DrawGraph(
								DoubleTools.ToInt(u.X),
								DoubleTools.ToInt(u.Y),
								grphHdl,
								1
								)
								!= 0
							:
							DX.DrawGraphF(
								(float)u.X,
								(float)u.Y,
								grphHdl,
								1
								)
								!= 0
							) // ? 失敗
						{
							if (i.Extra.IgnoreError == false)
							{
								ProcMain.WriteLog(u.X);
								ProcMain.WriteLog(u.Y);
								ProcMain.WriteLog(i.PicId);
								ProcMain.WriteLog(grphHdl);

								throw new GameError();
							}
						}
					}
					break;

				default:
					throw new GameError();
			}

			if (i.Extra.PicRes != null)
			{
				GamePicture.ResetPicRes();
			}
			if (i.Extra.AlphaOn || i.Extra.BlendAddOn || i.Extra.BlendInvOn)
			{
				ResetBlend();
			}
			if (i.Extra.MosaicOn)
			{
				DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
			}
			if (i.Extra.BrightOn)
			{
				ResetBright();
			}

			return false;
		}

		private static void DrawPicReleaseParam(ParamInfo i)
		{
			// noop
		}

		private static void DrawPic(int picId, LayoutInfo layout_bind)
		{
			ParamInfo i = new ParamInfo();

			i.PicId = picId;
			i.Layout = layout_bind;
			i.Extra = DPE;

			if (i.Extra.TL == null)
			{
				DrawPicFunc(i);
				DrawPicReleaseParam(i);
			}
			else
				TaskList.AddTask(i.Extra.TL, false, DrawPicFunc, i, DrawPicReleaseParam);
		}

		public static void DrawFree(int picId, double ltx, double lty, double rtx, double rty, double rbx, double rby, double lbx, double lby)
		{
			// layout no-check

			LayoutInfo i = new LayoutInfo();

			i.Mode = 'F';
			i.Entity = new LayoutInfo.FreeInfo()
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

			DrawPic(picId, i);
		}

		public static void DrawRect_LTRB(int picId, double l, double t, double r, double b)
		{
			// layout check
			if (
				l < -(double)IntTools.IMAX || (double)IntTools.IMAX - 1.0 < l ||
				t < -(double)IntTools.IMAX || (double)IntTools.IMAX - 1.0 < t ||
				r < l + 1.0 || (double)IntTools.IMAX < r ||
				b < t + 1.0 || (double)IntTools.IMAX < b
				)
				throw new GameError();

			LayoutInfo i = new LayoutInfo();

			i.Mode = 'R';
			i.Entity = new LayoutInfo.RectInfo()
			{
				L = l,
				T = t,
				R = r,
				B = b,
			};

			DrawPic(picId, i);
		}

		public static void DrawRect(int picId, double l, double t, double w, double h)
		{
			DrawRect_LTRB(picId, l, t, l + w, t + h);
		}

		public static void DrwaSimple(int picId, double x, double y)
		{
			// layout check
			if (
				x < -(double)IntTools.IMAX || (double)IntTools.IMAX < x ||
				y < -(double)IntTools.IMAX || (double)IntTools.IMAX < y
				)
				throw new GameError();

			DrawBegin(picId, x, y);
			DrawEnd();
		}

		private static int DB_PicId;
		private static double DB_X;
		private static double DB_Y;
		private static LayoutInfo DB_L;

		public static void DrawBegin(int picId, double x, double y)
		{
			if (DB_L != null) throw new GameError();

			LayoutInfo i = new LayoutInfo();

			double w;
			double h;

			if (DPE.GraphicHandleFlag)
			{
				w = DPE.GraphicSize.W;
				h = DPE.GraphicSize.H;

				if (
					w < 1 || IntTools.IMAX < w ||
					h < 1 || IntTools.IMAX < h
					)
					throw new GameError();
			}
			else
			{
				w = GamePicture.Pic_W(picId);
				h = GamePicture.Pic_H(picId);
			}
			w /= 2.0;
			h /= 2.0;

			i.Mode = 'F';
			i.Entity = new LayoutInfo.FreeInfo()
			{
				LTX = -w,
				LTY = -h,
				RTX = w,
				RTY = -h,
				RBX = w,
				RBY = h,
				LBX = -w,
				LBY = h,
			};

			DB_PicId = picId;
			DB_X = x;
			DB_Y = y;
			DB_L = i;
		}

		public static void DrawClide(double x, double y)
		{
			if (DB_L == null) throw new GameError();

			LayoutInfo i = DB_L;
			LayoutInfo.FreeInfo u = (LayoutInfo.FreeInfo)i.Entity;

			u.LTX += x;
			u.LTY += y;
			u.RTX += x;
			u.RTY += y;
			u.RBX += x;
			u.RBY += y;
			u.LBX += x;
			u.LBY += y;
		}

		private static void Rotate(ref double x, ref double y, double rot)
		{
			double w;
			w = x * Math.Cos(rot) - y * Math.Sin(rot);
			y = x * Math.Sin(rot) + y * Math.Cos(rot);
			x = w;
		}

		public static void DrawRotate(double rot)
		{
			if (DB_L == null) throw new GameError();

			LayoutInfo i = DB_L;
			LayoutInfo.FreeInfo u = (LayoutInfo.FreeInfo)i.Entity;

			Rotate(ref u.LTX, ref u.LTY, rot);
			Rotate(ref u.RTX, ref u.RTY, rot);
			Rotate(ref u.RBX, ref u.RBY, rot);
			Rotate(ref u.LBX, ref u.LBY, rot);
		}

		public static void DrawZoom_X(double z)
		{
			if (DB_L == null) throw new GameError();

			LayoutInfo i = DB_L;
			LayoutInfo.FreeInfo u = (LayoutInfo.FreeInfo)i.Entity;

			u.LTX *= z;
			u.RTX *= z;
			u.RBX *= z;
			u.LBX *= z;
		}

		public static void DrawZoom_Y(double z)
		{
			if (DB_L == null) throw new GameError();

			LayoutInfo i = DB_L;
			LayoutInfo.FreeInfo u = (LayoutInfo.FreeInfo)i.Entity;

			u.LTY *= z;
			u.RTY *= z;
			u.RBY *= z;
			u.LBY *= z;
		}

		public static void DrawZoom(double z)
		{
			DrawZoom_X(z);
			DrawZoom_Y(z);
		}

		public static void DrawEnd()
		{
			if (DB_L == null) throw new GameError();

			LayoutInfo i = DB_L;
			LayoutInfo.FreeInfo u = (LayoutInfo.FreeInfo)i.Entity;

			u.LTX += DB_X;
			u.LTY += DB_Y;
			u.RTX += DB_X;
			u.RTY += DB_Y;
			u.RBX += DB_X;
			u.RBY += DB_Y;
			u.LBX += DB_X;
			u.LBY += DB_Y;

			DrawPic(DB_PicId, i);
			DB_L = null;
		}
	}
}
