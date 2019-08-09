using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameSoundUtils
	{
		public static List<GameSound> Sounds = new List<GameSound>();

		public static void Add(GameSound sound)
		{
			Sounds.Add(sound);
		}

		public static void UnloadAll()
		{
			foreach (GameSound sound in Sounds)
				sound.Unload();
		}

		public static void Play(int handle, bool once = true, bool resume = false)
		{
			switch (DX.CheckSoundMem(handle))
			{
				case 1: // ? 再生中
					return;

				case 0: // ? 再生されていない。
					break;

				case -1: // ? エラー
					throw new GameError();

				default: // ? 不明
					throw new GameError();
			}
			if (DX.PlaySoundMem(handle, once ? DX.DX_PLAYTYPE_BACK : DX.DX_PLAYTYPE_LOOP, resume ? 0 : 1) != 0) // ? 失敗
				throw new GameError();
		}

		public static void Stop(int handle)
		{
			if (DX.StopSoundMem(handle) != 0) // ? 失敗
				throw new GameError();
		}

		public static void SetVolume(int handle, double volume)
		{
			volume = DoubleTools.Range(volume, 0.0, 1.0);

			int pal = DoubleTools.ToInt(volume * 255.0);

			if (pal < 0 || 255 < pal)
				throw new GameError(); // 2bs

			if (DX.ChangeVolumeSoundMem(pal, handle) != 0) // ? 失敗
				throw new GameError();
		}

		public static double MixVolume(double volume1, double volume2)
		{
			volume1 = DoubleTools.Range(volume1, 0.0, 1.0);
			volume2 = DoubleTools.Range(volume2, 0.0, 1.0);

			double mixedVolume = volume1 * volume2 * 2.0;

			mixedVolume = DoubleTools.Range(mixedVolume, 0.0, 1.0);

			return mixedVolume;
		}
	}
}
