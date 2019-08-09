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
			public int PadBtnId = -1;
			public int KbdKeyId = -1;

			// <---- prm

			public int Status;

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

		public static void EachFrame()
		{
			// TODO
		}
	}
}
