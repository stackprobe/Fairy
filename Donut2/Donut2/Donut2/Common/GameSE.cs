using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class GameSE : IDisposable
	{
		public const int HANDLE_NUM = 64;

		public GameSound Sound;
		public double Volume; // 0.0 ～ 1.0
		public int HandleIndex = 0;

		public GameSE(string file)
			: this(new GameSound(file, HANDLE_NUM))
		{ }

		public GameSE(GameSound sound_binding, double volume = 0.5)
		{
			this.Sound = sound_binding;
			this.Volume = volume;

			this.UpdateVolume();

			GameSEUtils.Add(this);
		}

		public void Dispose()
		{
			if (GameSEUtils.Remove(this) == false) // ? Already disposed
				return;

			this.Sound.Dispose();
			this.Sound = null;
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
			for (int index = 0; index < HANDLE_NUM; index++)
			{
				if (this.Sound.IsHandleLoaded(index))
				{
					GameSoundUtils.SetVolume(this.Sound.GetHandle(index), GameSoundUtils.MixVolume(GameGround.SEVolume, this.Volume));
				}
			}
		}
	}
}
