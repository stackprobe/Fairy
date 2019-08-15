using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class DDMouse
	{
		public static int Rot;

		private static int L;
		private static int R;
		private static int M;

		public static void EachFrame()
		{
			uint status;

			if (DDEngine.WindowIsActive)
			{
				Rot = DX.GetMouseHWheelRotVol();
				status = (uint)DX.GetMouseInput();
			}
			else
			{
				Rot = 0;
				status = 0u;
			}
			Rot = IntTools.Range(Rot, -IntTools.IMAX, IntTools.IMAX);

			DDUtils.UpdateInput(ref L, (status & (uint)DX.MOUSE_INPUT_LEFT) != 0u);
			DDUtils.UpdateInput(ref R, (status & (uint)DX.MOUSE_INPUT_RIGHT) != 0u);
			DDUtils.UpdateInput(ref M, (status & (uint)DX.MOUSE_INPUT_MIDDLE) != 0u);
		}

		private static int GetInput(int status)
		{
			return 1 <= DDEngine.FreezeInputFrame ? 0 : status;
		}

		public static int Get_L()
		{
			return GetInput(L);
		}

		public static int Get_R()
		{
			return GetInput(R);
		}

		public static int Get_M()
		{
			return GetInput(M);
		}

		public static int X = (int)(DDConsts.Screen_W / 2.0);
		public static int Y = (int)(DDConsts.Screen_H / 2.0);

		public static void UpdatePos()
		{
			if (DX.GetMousePoint(out X, out Y) != 0) // ? 失敗
				throw new DDError();

			X *= DDConsts.Screen_W;
			X /= DDGround.RealScreen_W;
			Y *= DDConsts.Screen_H;
			Y /= DDGround.RealScreen_H;
		}

		public static void ApplyPos()
		{
			int mx = X;
			int my = Y;

			mx *= DDGround.RealScreen_W;
			mx /= DDConsts.Screen_W;
			my *= DDGround.RealScreen_H;
			my /= DDConsts.Screen_H;

			if (DX.SetMousePoint(mx, my) != 0) // ? 失敗
				throw new DDError();
		}

		public static int MoveX;
		public static int MoveY;

		private static int UM_LastFrame = -IntTools.IMAX;

		public static void UpdateMove()
		{
			const int centerX = (int)(DDConsts.Screen_W / 2.0);
			const int centerY = (int)(DDConsts.Screen_H / 2.0);

			if (DDEngine.ProcFrame <= UM_LastFrame) // ? 2回以上更新した。
				throw new DDError();

			UpdatePos();

			MoveX = X - centerX;
			MoveY = Y - centerY;

			X = centerX;
			Y = centerY;

			ApplyPos();

			if (UM_LastFrame + 1 < DDEngine.ProcFrame) // ? 1フレーム以上更新しなかった。
			{
				MoveX = 0;
				MoveY = 0;
			}
			UM_LastFrame = DDEngine.ProcFrame;
		}
	}
}
