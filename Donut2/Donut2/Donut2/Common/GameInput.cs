using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameInput
	{
		public class Button
		{
			public int BtnId = -1; // -1 == 未割り当て
			public int KeyId = -1; // -1 == 未割り当て

			// <---- prm

			public int Status = 0;

			public bool IsPress()
			{
				return this.Status == 1;
			}

			public bool IsHold()
			{
				return 1 <= this.Status;
			}

			public bool IsRelease()
			{
				return this.Status == -1;
			}
		}

		public static Button DIR_2 = new Button();
		public static Button DIR_4 = new Button();
		public static Button DIR_6 = new Button();
		public static Button DIR_8 = new Button();
		public static Button A = new Button();
		public static Button B = new Button();
		public static Button C = new Button();
		public static Button D = new Button();
		public static Button E = new Button();
		public static Button F = new Button();
		public static Button L = new Button();
		public static Button R = new Button();
		public static Button PAUSE = new Button();
		public static Button START = new Button();

		private static void MixInput(Button button)
		{
			bool keyDown = 1 <= GameKey.GetInput(button.KeyId);
			bool btnDown = 1 <= GamePad.GetInput(GameGround.PrimaryPadId, button.BtnId);

			GameUtils.UpdateInput(ref button.Status, keyDown || btnDown);
		}

		public static void EachFrame()
		{
			int freezeInputFrame_BKUP = GameEngine.FreezeInputFrame;
			GameEngine.FreezeInputFrame = 0;

			MixInput(DIR_2);
			MixInput(DIR_4);
			MixInput(DIR_6);
			MixInput(DIR_8);
			MixInput(A);
			MixInput(B);
			MixInput(C);
			MixInput(D);
			MixInput(E);
			MixInput(F);
			MixInput(L);
			MixInput(R);
			MixInput(PAUSE);
			MixInput(START);

			GameEngine.FreezeInputFrame = freezeInputFrame_BKUP;
		}
	}
}
