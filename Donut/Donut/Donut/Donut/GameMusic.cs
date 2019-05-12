using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameMusic
	{
		public class MusicInfo : IDisposable
		{
			public int Handle;
			public double Volume; // 0.0 - 1.0, def: 0.5

			public void Dispose() // Codevil の UnloadMusic()
			{
				// reset {
				while (1 <= PlayList.Count)
					PlayList.Dequeue().Dispose();

				CurrDestMusic = null;
				CurrDestMusicVolumeRate = 0.0;
				// }

				GameSound.UnloadSound(this.Handle);
			}
		}

		private class PlayInfo : IDisposable
		{
			public char Command; // "PVS"
			public MusicInfo Music;
			public bool OnceMode;
			public bool ResumeMode;
			public double VolumeRate;

			public void Dispose() // Codevil の ReleasePI()
			{
				// noop
			}
		}

		private static PlayInfo CreatePI(char command, MusicInfo music, bool once_mode, bool resume_mode, double volume_rate)
		{
			PlayInfo i = new PlayInfo();

			i.Command = command;
			i.Music = music;
			i.OnceMode = once_mode;
			i.ResumeMode = resume_mode;
			i.VolumeRate = volume_rate;

			return i;
		}

		private static Queue<PlayInfo> PlayList = new Queue<PlayInfo>();

		public static MusicInfo CurrDestMusic = null;
		public static double CurrDestMusicVolumeRate = 0.0;

		private static MusicInfo LoadMusic(byte[] fileData)
		{
			MusicInfo i = new MusicInfo();

			i.Handle = GameSound.LoadSound(fileData);
			i.Volume = 0.5; // 個別の音量のデフォルト 0.0 - 1.0

			// TODO ??? post LoadSound

			GameSound.SetVolume(i.Handle, 0.0); // ミュートしておく。
			return i;
		}

		private static void UnloadMusic(MusicInfo i)
		{
			i.Dispose();
		}

		private static OneObject<ResourceCluster<MusicInfo>> MusRes =
			new OneObject<ResourceCluster<MusicInfo>>(
				() => new ResourceCluster<MusicInfo>("Music.dat", @"..\..\..\..\Music.txt", LoadMusic, UnloadMusic)
				);

		public static void MusicEachFrame()
		{
			if (1 <= PlayList.Count)
			{
				PlayInfo i = PlayList.Dequeue();

				switch (i.Command)
				{
					case 'P':
						GameSound.SoundPlay(i.Music.Handle, i.OnceMode, i.ResumeMode);
						break;

					case 'V':
						GameSound.SetVolume(i.Music.Handle, GameSound.MixVolume(GameGround.I.MusicVolume, i.Music.Volume) * i.VolumeRate);
						break;

					case 'S':
						GameSound.SoundStop(i.Music.Handle);
						break;

					default:
						throw new GameError();
				}
			}
		}

		public static void MusicPlay(int musId, bool once_mode = false, bool resume_mode = false, double volumeRate = 1.0, int fadeFrameMax = 30)
		{
			MusicInfo i = MusRes.I.GetHandle(musId);

			if (CurrDestMusic != null)
			{
				if (CurrDestMusic == i)
					return;

				if (fadeFrameMax != 0)
					MusicFade(fadeFrameMax);
				else
					MusicStop();
			}
			PlayList.Enqueue(CreatePI('P', i, once_mode, resume_mode, 0.0));
			//PlayList.Enqueue(null);
			PlayList.Enqueue(CreatePI('V', i, false, false, volumeRate));
			PlayList.Enqueue(null);
			PlayList.Enqueue(null);
			PlayList.Enqueue(null);

			CurrDestMusic = i;
			CurrDestMusicVolumeRate = volumeRate;
		}

		public static void MusicFade(int frameMax = 30, double destVRate = 0.0)
		{
			MusicFade(frameMax, destVRate, CurrDestMusicVolumeRate);
		}

		public static void MusicFade(int frameMax, double destVRate, double startVRate)
		{
			if (CurrDestMusic == null)
				return;

			frameMax = IntTools.Range(frameMax, 1, 3600); // 1 frame - 1 min
			destVRate = DoubleTools.Range(destVRate, 0.0, 1.0);
			startVRate = DoubleTools.Range(startVRate, 0.0, 1.0);

			for (int frmcnt = 0; frmcnt <= frameMax; frmcnt++)
			{
				double vRate;

				if (frmcnt == 0)
					vRate = startVRate;
				else if (frmcnt == frameMax)
					vRate = destVRate;
				else
					vRate = startVRate + ((destVRate - startVRate) * frmcnt) / frameMax;

				PlayList.Enqueue(CreatePI('V', CurrDestMusic, false, false, vRate));
			}
			CurrDestMusicVolumeRate = destVRate;

			if (destVRate == 0.0) // ? フェード目標音量ゼロ -> 曲停止
			{
				MusicStop();
			}
		}

		public static void MusicStop()
		{
			if (CurrDestMusic == null)
				return;

			PlayList.Enqueue(CreatePI('V', CurrDestMusic, false, false, 0.0));
			PlayList.Enqueue(null);
			PlayList.Enqueue(null);
			PlayList.Enqueue(null);
			PlayList.Enqueue(CreatePI('S', CurrDestMusic, false, false, 0.0));
			PlayList.Enqueue(null);
			PlayList.Enqueue(null);
			PlayList.Enqueue(null);

			CurrDestMusic = null;
			CurrDestMusicVolumeRate = 0.0;
		}

		public static void MusicTouch(int musId)
		{
			MusRes.I.GetHandle(musId);
		}

		public static void UpdateMusicVolume()
		{
			MusicFade(0, 1.0);
		}
	}
}
