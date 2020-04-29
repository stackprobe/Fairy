using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public static class GameSound
	{
		public static int LoadSound(byte[] fileData)
		{
			int handle = -1;

			GameHelper.PinOn(fileData, p => handle = DX.LoadSoundMemByMemImage(p, fileData.Length));

			if (handle == -1) // ? 失敗
				throw new GameError();

			return handle;
		}

		public static int DuplSound(int handle)
		{
			int dupl_handle = DX.DuplicateSoundMem(handle);

			if (dupl_handle == -1) // ? 失敗
				throw new GameError();

			return dupl_handle;
		}

		public static void UnloadSound(int handle)
		{
			if (DX.DeleteSoundMem(handle) != 0) // ? 失敗
				throw new GameError();
		}

		public static void SoundPlay(int handle, bool once_mode = true, bool resume_mode = false)
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
			if (DX.PlaySoundMem(handle, once_mode ? DX.DX_PLAYTYPE_BACK : DX.DX_PLAYTYPE_LOOP, resume_mode ? 0 : 1) != 0) // ? 失敗
				throw new GameError();
		}

		public static void SetVolume(int handle, double volume)
		{
			volume = DoubleTools.ToRange(volume, 0.0, 1.0);

			int pal = DoubleTools.ToInt(volume * 255.0);

			if (pal < 0 || 255 < pal)
				throw new GameError();

			if (DX.ChangeVolumeSoundMem(pal, handle) != 0) // ? 失敗
				throw new GameError();
		}

		public static void SoundStop(int handle)
		{
			if (DX.StopSoundMem(handle) != 0) // ? 失敗
				throw new GameError();
		}

		public static double MixVolume(double volume1, double volume2) // (volume1, volume2): 0.0 - 1.0, ret: 0.0 - 1.0
		{
			volume1 = DoubleTools.ToRange(volume1, 0.0, 1.0);
			volume2 = DoubleTools.ToRange(volume2, 0.0, 1.0);

			double mixedVolume = volume1 * volume2 * 2.0; // 0.0 - 2.0

			mixedVolume = DoubleTools.ToRange(mixedVolume, 0.0, 1.0);

			return mixedVolume;
		}
	}
}
