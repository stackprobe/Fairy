using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameKeyboard
	{
		public const int KEY_MAX = 256;

		private static int[] KeyStatus = new int[KEY_MAX];
		private static byte[] StatusMap = new byte[KEY_MAX];

		public static void KeyEachFrame()
		{
			if (GameEngine.WindowIsActive)
			{
				if (DX.GetHitKeyStateAll(StatusMap) != 0) // ? 失敗
					throw new GameError();

				for (int keyId = 0; keyId < KEY_MAX; keyId++)
				{
					GameDefine.UpdateInput(ref KeyStatus[keyId], StatusMap[keyId] != 0);
				}
			}
			else // ? 非アクティブ -> 無入力
			{
				for (int keyId = 0; keyId < KEY_MAX; keyId++)
				{
					GameDefine.UpdateInput(ref KeyStatus[keyId], false);
				}
			}
		}

		public const int DUMMY_KEY_ID = DX.KEY_INPUT_BACK; // keyId, 他のキーは F12 で飛んでって確認してね。

		public static int GetKeyInput(int keyId)
		{
			if (keyId < 0 || KEY_MAX <= keyId)
				throw new GameError();

			return GameEngine.FreezeInputFrame != 0 ? 0 : KeyStatus[keyId];
		}

		public static bool GetKeyPound(int keyId)
		{
			return GameToolkit2.IsPound(GetKeyInput(keyId));
		}
	}
}
