using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.Common
{
	public static class GameKey
	{
		private const int KEY_MAX = 256;

		private static int[] KeyStatus = new int[KEY_MAX];
		private static byte[] StatusMap = new byte[KEY_MAX];

		public static void EachFrame()
		{
			if (GameEngine.WindowIsActive)
			{
				// FIXME これでいいのか？

				if (DX.GetHitKeyStateAll(StatusMap) != 0) // ? 失敗
					throw new GameError();

				for (int keyId = 0; keyId < 256; keyId++)
					GameUtils.UpdateInput(ref KeyStatus[keyId], StatusMap[keyId] != 0);
			}
			else
			{
				for (int keyId = 0; keyId < 256; keyId++)
					GameUtils.UpdateInput(ref KeyStatus[keyId], false);
			}
		}

		public static int GetInput(int keyId)
		{
			return 1 <= GameEngine.FreezeInputFrame ? 0 : KeyStatus[keyId];
		}

		public static bool IsPound(int keyId)
		{
			return GameUtils.IsPound(GetInput(keyId));
		}
	}
}
