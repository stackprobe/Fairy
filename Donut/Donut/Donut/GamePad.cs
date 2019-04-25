using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GamePad
	{
		public enum SNWPB // SANWA Pad Btnno
		{
			DIR_2,
			DIR_4,
			DIR_6,
			DIR_8,
			A1, // 下段左
			A2, // 下段中
			A3, // 下段右
			B1, // 上段左
			B2, // 上段中
			B3, // 上段右
			L,
			R,
			USTART, // 上スタート
			DSTART, // 下スタート
		}

		public const int PAD_MAX = 16;
		public const int PAD_BUTTON_MAX = 32;

		private class Pad
		{
			public int[] ButtonStatus = new int[PAD_BUTTON_MAX];
			public uint Status;
		}

		private static int PadId2InputType(int padId)
		{
			return padId + 1;
		}

		private static Pad[] Pads = new Pad[PAD_MAX];
		private static int PadCount = -1;

		static GamePad()
		{
			for (int padId = 0; padId < PAD_MAX; padId++)
			{
				Pads[padId] = new Pad();
			}
		}

		public static int GetPadCount()
		{
			if (PadCount == -1)
			{
				PadCount = DX.GetJoypadNum();

				if (PadCount < 0 || PAD_MAX < PadCount)
					throw new DD.Error();
			}
			return PadCount;
		}

		public static void PadEachFrame()
		{
			int padCount = GetPadCount();

			for (int padId = 0; padId < padCount; padId++)
			{
				uint status;

				if (GameEngine.WindowIsActive)
				{
					status = (uint)DX.GetJoypadInputState(PadId2InputType(padId));
				}
				else // ? 非アクティブ
				{
					status = 0u; // 無入力
				}
				if (status != 0u)
				{
					for (int btnId = 0; btnId < PAD_BUTTON_MAX; btnId++)
					{
						GameDefine.UpdateInput(ref Pads[padId].ButtonStatus[btnId], (status & (1u << btnId)) != 0u);
					}
				}
				else
				{
					for (int btnId = 0; btnId < PAD_BUTTON_MAX; btnId++)
					{
						GameDefine.UpdateInput(ref Pads[padId].ButtonStatus[btnId], false);
					}
				}

				if (GameGround.I.PrimaryPadId == -1 && 10 < GameEngine.ProcFrame && Pads[padId].Status == 0u && status != 0u) // 最初にボタンを押下したパッドを PrimaryPadId にセット
					GameGround.I.PrimaryPadId = padId;

				Pads[padId].Status = status;
			}
		}

		public static int GetPadInput(int padId, int btnId)
		{
			if (padId == -1) // ? 未割り当て
				padId = 0;

			if (btnId == -1) // ? 割り当てナシ
				return 0;

			if (
				padId < 0 || PAD_MAX <= padId ||
				btnId < 0 || PAD_BUTTON_MAX <= btnId
				)
				throw new DD.Error();

			return GameEngine.FreezeInputFrame != 0 ? 0 : Pads[padId].ButtonStatus[btnId];
		}

		public static bool GetPadPound(int padId, int btnId)
		{
			return GameToolkit2.IsPound(GetPadInput(padId, btnId));
		}
	}
}
