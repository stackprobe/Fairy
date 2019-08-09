using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class GameSE
	{
		public const int HANDLE_COUNT = 64;

		public GameSound Sound;
		public double Volume = 0.5; // 0.0 ～ 1.0
		public int HandleIndex = 0;

		public GameSE(string file)
			: this(new GameSound(file, HANDLE_COUNT))
		{ }

		public GameSE(Func<byte[]> getFileData)
			: this(new GameSound(getFileData, HANDLE_COUNT))
		{ }

		public GameSE(GameSound sound_binding)
		{
			this.Sound = sound_binding;
			this.Sound.PostLoaded = this.UpdateVolume_NoCheck;

			GameSEUtils.Add(this);
		}

		public void Play()
		{
			GameSEUtils.Play(this);
		}

		public void Stop()
		{
			GameSEUtils.Stop(this);
		}

		public void SetVolume(double volume)
		{
			this.Volume = volume;
			this.UpdateVolume();
		}

		public void UpdateVolume()
		{
			if (this.Sound.IsLoaded())
				this.UpdateVolume_NoCheck();
		}

		public void UpdateVolume_NoCheck()
		{
			double mixedVolume = GameSoundUtils.MixVolume(GameGround.SEVolume, this.Volume);

			for (int index = 0; index < HANDLE_COUNT; index++)
				GameSoundUtils.SetVolume(this.Sound.GetHandle(index), mixedVolume);
		}

		public void Touch()
		{
			this.Sound.GetHandle(0);
		}
	}
}
