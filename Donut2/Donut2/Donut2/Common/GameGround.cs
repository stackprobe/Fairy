using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	public static class GameGround
	{
		public static GameTaskList EL = new GameTaskList();
		public static int PrimaryPadId = -1; // -1 == 未設定
		public static GameSubScreen MainScreen = null; // null == 不使用
		public static I4Rect MonitorRect;

		public static int RealScreen_W = GameConsts.Screen_W;
		public static int RealScreen_H = GameConsts.Screen_H;

		public static int RealScreenDraw_L;
		public static int RealScreenDraw_T;
		public static int RealScreenDraw_W = -1; // -1 == RealScreenDraw_LTWH 不使用
		public static int RealScreenDraw_H;

		public static double MusicVolume = GameConsts.DefaultVolume;
		public static double SEVolume = GameConsts.DefaultVolume;

		public static bool RO_MouseDispMode = false;

		public static GameResourceCommon CommonResource;

		public static D2Point Camera;
		public static I2Point ICamera;

		public static void INIT()
		{
			GameInput.DIR_2.BtnId = 0;
			GameInput.DIR_4.BtnId = 1;
			GameInput.DIR_6.BtnId = 2;
			GameInput.DIR_8.BtnId = 3;
			GameInput.A.BtnId = 4;
			GameInput.B.BtnId = 7;
			GameInput.C.BtnId = 5;
			GameInput.D.BtnId = 8;
			GameInput.E.BtnId = 6;
			GameInput.F.BtnId = 9;
			GameInput.L.BtnId = 10;
			GameInput.R.BtnId = 11;
			GameInput.PAUSE.BtnId = 13;
			GameInput.START.BtnId = 12;

			GameInput.DIR_2.KeyId = DX.KEY_INPUT_DOWN;
			GameInput.DIR_4.KeyId = DX.KEY_INPUT_LEFT;
			GameInput.DIR_6.KeyId = DX.KEY_INPUT_RIGHT;
			GameInput.DIR_8.KeyId = DX.KEY_INPUT_UP;
			GameInput.A.KeyId = DX.KEY_INPUT_Z;
			GameInput.B.KeyId = DX.KEY_INPUT_X;
			GameInput.C.KeyId = DX.KEY_INPUT_C;
			GameInput.D.KeyId = DX.KEY_INPUT_V;
			GameInput.E.KeyId = DX.KEY_INPUT_A;
			GameInput.F.KeyId = DX.KEY_INPUT_S;
			GameInput.L.KeyId = DX.KEY_INPUT_D;
			GameInput.R.KeyId = DX.KEY_INPUT_F;
			GameInput.PAUSE.KeyId = DX.KEY_INPUT_SPACE;
			GameInput.START.KeyId = DX.KEY_INPUT_RETURN;

			// app > @ INIT

			//RO_MouseDispMode = true;

			// < app
		}

		public static void FNLZ()
		{
			// app > @ FNLZ

			// < app
		}
	}
}
