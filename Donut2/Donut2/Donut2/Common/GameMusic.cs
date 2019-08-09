using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class GameMusic
	{
		public GameSound Sound;
		public double Volume = 0.5; // 0.0 ～ 1.0

		public GameMusic(string file)
			: this(new GameSound(file, 1))
		{ }

		public GameMusic(Func<byte[]> getFileData)
			: this(new GameSound(getFileData, 1))
		{ }

		public GameMusic(GameSound sound_binding)
		{
			this.Sound = sound_binding;
			this.Sound.PostLoaded = () => GameSoundUtils.SetVolume(this.Sound.GetHandle(0), 0.0); // ロードしたらミュートしておく。

			GameMusicUtils.Add(this);
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
