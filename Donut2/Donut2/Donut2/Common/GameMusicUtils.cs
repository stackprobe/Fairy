using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameMusicUtils
	{
		public static List<GameMusic> Musics = new List<GameMusic>();

		public static void Add(GameMusic music)
		{
			Musics.Add(music);
		}

		public static bool Remove(GameMusic music) // ret: ? ! Already removed
		{
			return GameUtils.FastDesertElement(Musics, i => i == music) != null;
		}

		private class PlayInfo
		{
			public enum Command_e
			{
				PLAY = 1,
				CHANGE_VOLUME,
				STOP,
			}

			public Command_e Command;
			public GameMusic Music;
			public bool Once;
			public bool Resume;
			public double Volume;

			public PlayInfo(Command_e command, GameMusic music, bool once, bool resume, double volume)
			{
				this.Command = command;
				this.Music = music;
				this.Once = once;
				this.Resume = resume;
				this.Volume = volume;
			}
		}

		private static Queue<PlayInfo> PlayInfos = new Queue<PlayInfo>();

		public static void EachFrame()
		{
			throw null; // TODO
		}

		public static GameMusic CurrDestMusic = null;
		public static double CurrDestVolume = 0.0;

		public static void Play(GameMusic music, bool once, bool resume, double volume, int fadeFrameMax)
		{
			if (CurrDestMusic != null) // ? 再生中
			{
				if (CurrDestMusic == music)
					return;

				if (1 <= fadeFrameMax)
					Fade(fadeFrameMax, 0.0, CurrDestVolume);
				else
					Stop();
			}
			PlayInfos.Enqueue(new PlayInfo(PlayInfo.Command_e.PLAY, music, once, resume, 0.0));
			PlayInfos.Enqueue(null);
			PlayInfos.Enqueue(new PlayInfo(PlayInfo.Command_e.CHANGE_VOLUME, music, false, false, volume));
			PlayInfos.Enqueue(null);
			PlayInfos.Enqueue(null);
			PlayInfos.Enqueue(null);

			CurrDestMusic = music;
			CurrDestVolume = volume;
		}

		public static void Fade(int frameMax = 30, double destVolume = 0.0)
		{
			Fade(frameMax, destVolume, CurrDestVolume);
		}

		public static void Fade(int frameMax, double destVolume, double startVolume)
		{
			if (CurrDestMusic == null)
				return;

			frameMax = IntTools.Range(frameMax, 1, 3600); // 1 frame ～ 1 min
			destVolume = DoubleTools.Range(destVolume, 0.0, 1.0);
			startVolume = DoubleTools.Range(startVolume, 0.0, 1.0);

			for (int frmcnt = 0; frmcnt <= frameMax; frmcnt++)
			{
				double volume;

				if (frmcnt == 0)
					volume = startVolume;
				else if (frmcnt == frameMax)
					volume = destVolume;
				else
					volume = startVolume + ((destVolume - startVolume) * frmcnt) / frameMax;

				PlayInfos.Enqueue(new PlayInfo(PlayInfo.Command_e.CHANGE_VOLUME, CurrDestMusic, false, false, volume));
			}
			CurrDestVolume = destVolume;

			if (destVolume == 0.0)
			{
				Stop();
			}
		}

		public static void Stop()
		{
			if (CurrDestMusic == null)
				return;

			PlayInfos.Enqueue(new PlayInfo(PlayInfo.Command_e.CHANGE_VOLUME, CurrDestMusic, false, false, 0.0));
			PlayInfos.Enqueue(null);
			PlayInfos.Enqueue(null);
			PlayInfos.Enqueue(null);
			PlayInfos.Enqueue(new PlayInfo(PlayInfo.Command_e.STOP, CurrDestMusic, false, false, 0.0));
			PlayInfos.Enqueue(null);
			PlayInfos.Enqueue(null);
			PlayInfos.Enqueue(null);

			CurrDestMusic = null;
			CurrDestVolume = 0.0;
		}
	}
}
