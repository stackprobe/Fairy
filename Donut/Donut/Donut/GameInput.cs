using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class GameInput
	{
		public enum INP
		{
			DIR_2,
			DIR_4,
			DIR_6,
			DIR_8,
			A,
			B,
			C,
			D,
			E,
			F,
			L,
			R,
			PAUSE,
			START,

			MAX, // num of INP.*
		}

		private static int[] InputStatus = new int[(int)INP.MAX];

		private static void MixInput(INP inpId, int keyId, int btnId)
		{
			int freezeInputFrame_BKUP = GameEngine.FreezeInputFrame;
			GameEngine.FreezeInputFrame = 0;

			bool keyDown = 1 <= GameKeyboard.GetKeyInput(keyId);
			bool btnDown = 1 <= GamePad.GetPadInput(GameGround.I.PrimaryPadId, btnId);

			GameEngine.FreezeInputFrame = freezeInputFrame_BKUP;

			GameDefine.UpdateInput(ref InputStatus[(int)inpId], keyDown || btnDown);
		}

		public static void InputEachFrame()
		{
			MixInput(INP.DIR_2, GameGround.I.KbdKeyId.Dir_2, GameGround.I.PadBtnId.Dir_2);
			MixInput(INP.DIR_4, GameGround.I.KbdKeyId.Dir_4, GameGround.I.PadBtnId.Dir_4);
			MixInput(INP.DIR_6, GameGround.I.KbdKeyId.Dir_6, GameGround.I.PadBtnId.Dir_6);
			MixInput(INP.DIR_8, GameGround.I.KbdKeyId.Dir_8, GameGround.I.PadBtnId.Dir_8);
			MixInput(INP.A, GameGround.I.KbdKeyId.A, GameGround.I.PadBtnId.A);
			MixInput(INP.B, GameGround.I.KbdKeyId.B, GameGround.I.PadBtnId.B);
			MixInput(INP.C, GameGround.I.KbdKeyId.C, GameGround.I.PadBtnId.C);
			MixInput(INP.D, GameGround.I.KbdKeyId.D, GameGround.I.PadBtnId.D);
			MixInput(INP.E, GameGround.I.KbdKeyId.E, GameGround.I.PadBtnId.E);
			MixInput(INP.F, GameGround.I.KbdKeyId.F, GameGround.I.PadBtnId.F);
			MixInput(INP.L, GameGround.I.KbdKeyId.L, GameGround.I.PadBtnId.L);
			MixInput(INP.R, GameGround.I.KbdKeyId.R, GameGround.I.PadBtnId.R);
			MixInput(INP.PAUSE, GameGround.I.KbdKeyId.Pause, GameGround.I.PadBtnId.Pause);
			MixInput(INP.START, GameGround.I.KbdKeyId.Start, GameGround.I.PadBtnId.Start);
		}

		public static int GetInput(INP inpId)
		{
			return GameEngine.FreezeInputFrame != 0 ? 0 : InputStatus[(int)inpId];
		}

		public static bool GetPound(INP inpId)
		{
			return GameToolkit2.IsPound(GetInput(inpId));
		}
	}
}
