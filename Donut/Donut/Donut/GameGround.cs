using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameGround
	{
		public static GameGround I = null;

		public GameGround()
		{ }

		public void Init()
		{
			this.RealScreenSize = this.ScreenSize;

			this.PadBtnId.Dir_2 = (int)GamePad.SNWPB.DIR_2;
			this.PadBtnId.Dir_4 = (int)GamePad.SNWPB.DIR_4;
			this.PadBtnId.Dir_6 = (int)GamePad.SNWPB.DIR_6;
			this.PadBtnId.Dir_8 = (int)GamePad.SNWPB.DIR_8;
			this.PadBtnId.A = (int)GamePad.SNWPB.A1;
			this.PadBtnId.B = (int)GamePad.SNWPB.B1;
			this.PadBtnId.C = (int)GamePad.SNWPB.A2;
			this.PadBtnId.D = (int)GamePad.SNWPB.B2;
			this.PadBtnId.E = (int)GamePad.SNWPB.A3;
			this.PadBtnId.F = (int)GamePad.SNWPB.B3;
			this.PadBtnId.L = (int)GamePad.SNWPB.L;
			this.PadBtnId.R = (int)GamePad.SNWPB.R;
			this.PadBtnId.Pause = (int)GamePad.SNWPB.DSTART;
			this.PadBtnId.Start = (int)GamePad.SNWPB.USTART;

			this.KbdKeyId.Dir_2 = DX.KEY_INPUT_DOWN;
			this.KbdKeyId.Dir_4 = DX.KEY_INPUT_LEFT;
			this.KbdKeyId.Dir_6 = DX.KEY_INPUT_RIGHT;
			this.KbdKeyId.Dir_8 = DX.KEY_INPUT_UP;
			this.KbdKeyId.A = DX.KEY_INPUT_Z;
			this.KbdKeyId.B = DX.KEY_INPUT_X;
			this.KbdKeyId.C = DX.KEY_INPUT_C;
			this.KbdKeyId.D = DX.KEY_INPUT_V;
			this.KbdKeyId.E = DX.KEY_INPUT_A;
			this.KbdKeyId.F = DX.KEY_INPUT_S;
			this.KbdKeyId.L = DX.KEY_INPUT_D;
			this.KbdKeyId.R = DX.KEY_INPUT_F;
			this.KbdKeyId.Pause = DX.KEY_INPUT_SPACE;
			this.KbdKeyId.Start = DX.KEY_INPUT_RETURN;
		}

		public void Fnlz()
		{
			this.EL.Clear();
		}

		public void LoadFromDatFile()
		{
			// TODO
		}

		public void SaveToDatFile()
		{
			// TODO
		}

		public GameConfig Config = new GameConfig();

		public HandleDam Handles; // Codevil の GetFinalizers() に相当する。
		public HandleDam GameHandles = new HandleDam(); // Codevil の GetEndProcFinalizers() に相当する。

		public TaskList EL = new TaskList();
		public int PrimaryPadId = -1; // -1 == 未設定
		public SubScreen MainScreen = null; // null == 不使用
		public I4Rect MonitorRect;

		// SaveData {

		public I2Size ScreenSize = new I2Size(800, 600);
		public I2Size RealScreenSize;
		public I4Rect RealScreenDrawRect = null; // null == 不使用

		// 音量
		// 0.0 - 1.0
		// def: DEFAULT_VOLUME
		//
		public double MouseVolume = GameDefine.DEFAULT_VOLUME;
		public double SEVolume = GameDefine.DEFAULT_VOLUME;

		public class PadBtnId_t
		{
			public int Dir_2;
			public int Dir_4;
			public int Dir_6;
			public int Dir_8;
			public int A;
			public int B;
			public int C;
			public int D;
			public int E;
			public int F;
			public int L;
			public int R;
			public int Pause;
			public int Start;
		}

		// -1 == 割り当てナシ
		// 0 - (PAD_BUTTON_MAX - 1) == 割り当てボタンID
		// def: SNWPB_* を適当に配置
		//
		public PadBtnId_t PadBtnId = new PadBtnId_t();

		// -1 == 割り当てナシ
		// 0 - (KEY_MAX - 1) == 割り当てキーID
		// def: KEY_INPUT_* を適当に配置
		//
		public PadBtnId_t KbdKeyId = new PadBtnId_t();

		public bool RO_MouseDispMode = false;

		// } SaveData
	}
}
