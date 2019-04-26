using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class GameCommonEffect
	{
		public class ExtraInfo
		{
			public int EndPicId; // PicId から EndPicId まで表示して終了する, 0 == 無効, PicIdFrmPerIncと連動
			public int ModPicId; // PicId から (PicId + ModPicId - 1) までループする, 0 == 無効, PicIdFrmPerIncと連動
			public int PicIdFrmPerInc;
			public double SlideX;
			public double SlideY;
			public double SlideX_B;
			public double SlideY_B;
			public double R_B;
			public double Z_B;
			public double SlideX_F;
			public double SlideY_F;
			public double R_F;
			public double Z_F;
			public double SpeedRate;
			public int IgnoreCamera;
			public int BlendAddOn;

			// change by this.*() -->

			public bool BrightOn;
			public double Bright_R;
			public double Bright_G;
			public double Bright_B;
			public bool GrphHdlFlag;
			public I2Size GrphSize;

			public void SetBright(double r, double g, double b)
			{
				this.BrightOn = true;
				this.Bright_R = r;
				this.Bright_G = g;
				this.Bright_B = b;
			}

			public void SetGraphicSize(I2Size size)
			{
				this.GrphHdlFlag = true;
				this.GrphSize = size;
			}

			public void Reset()
			{
				CEE = new ExtraInfo();
			}
		}

		public static ExtraInfo CEE = new ExtraInfo();

		public static double CameraX;
		public static double CameraY;

		public int Frame;
		public int PicId;
		public ExtraInfo Extra;
		public double X;
		public double Y;
		public double R;
		public double Z;
		public double A;
		public double XAdd;
		public double YAdd;
		public double RAdd;
		public double ZAdd;
		public double AAdd;
		public double XAdd2;
		public double YAdd2;
		public double RAdd2;
		public double ZAdd2;
		public double AAdd2;
		public int OutOfScreenFrame;

		private static bool CommonEffectFunc(object prm)
		{
			GameCommonEffect i = (GameCommonEffect)prm;

			// TODO

			return false;
		}

		private static void CommonEffectReleaseParam(object prm)
		{
			// noop
		}

		public static void AddCommonEffect(
			TaskList tl,
			bool topMode,
			int picId,
			double x, double y, double r, double z, double a,
			double x_add, double y_add, double r_add, double z_add, double a_add,
			double x_add2, double y_add2, double r_add2, double z_add2, double a_add2
			)
		{
			GameCommonEffect i = new GameCommonEffect();

			i.Frame = 0;
			i.PicId = picId;
			i.Extra = CEE;

			i.X = x;
			i.Y = y;
			i.R = r;
			i.Z = z;
			i.A = a;

			i.XAdd = x_add;
			i.YAdd = y_add;
			i.RAdd = r_add;
			i.ZAdd = z_add;
			i.AAdd = a_add;

			i.XAdd2 = x_add2;
			i.YAdd2 = y_add2;
			i.RAdd2 = r_add2;
			i.ZAdd2 = z_add2;
			i.AAdd2 = a_add2;

			i.OutOfScreenFrame = 0;

			TaskList.AddTask(tl, topMode, CommonEffectFunc, i, CommonEffectReleaseParam);
		}
	}
}
