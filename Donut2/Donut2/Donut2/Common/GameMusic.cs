using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class GameMusic
	{
		public const int HANDLE_COUNT = 64;

		public GameSound Sound;
		public double Volume; // 0.0 ～ 1.0

		public GameMusic(string file)
			: this(new GameSound(file, 1))
		{ }

		public GameMusic(GameSound sound_binding, double volume = 0.5)
		{
			sound_binding.PostLoaded = () => GameSoundUtils.SetVolume(this.Sound.GetHandle(0), 0.0); // ロードしたらミュートしておく。

			this.Sound = sound_binding;
			this.Volume = volume;

			GameMusicUtils.Add(this);
		}

		public void Dispose()
		{
			if (GameMusicUtils.Remove(this) == false) // ? Already disposed
				return;

			this.Sound.Dispose();
			this.Sound = null;
		}

		public void Play(bool once = false, bool resume = false, double volume = 1.0, int fadeFrameMax = 30)
		{
			GameMusicUtils.Play(this, once, resume, volume, fadeFrameMax);
		}

		public void Touch()
		{
			this.Sound.GetHandle(0);
		}
	}
}
