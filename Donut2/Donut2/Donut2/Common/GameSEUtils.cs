using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameSEUtils
	{
		public static List<GameSE> SEList = new List<GameSE>();

		public static void Add(GameSE se)
		{
			SEList.Add(se);
		}

		private class PlayInfo
		{
			public enum AlterCommand_e
			{
				NORMAL = 1,
				STOP,
				LOOP,
			}

			public GameSE SE;
			public AlterCommand_e AlterCommand;

			public PlayInfo(GameSE se, AlterCommand_e alterCommand = AlterCommand_e.NORMAL)
			{
				this.SE = se;
				this.AlterCommand = alterCommand;
			}
		}

		private static Queue<PlayInfo> PlayInfos = new Queue<PlayInfo>();

		public static bool EachFrame() // ret: ? 処理した。
		{
			if (1 <= PlayInfos.Count)
			{
				PlayInfo info = PlayInfos.Dequeue();

				if (info != null)
				{
					switch (info.AlterCommand)
					{
						case PlayInfo.AlterCommand_e.NORMAL:
							info.SE.HandleIndex %= GameSE.HANDLE_COUNT;
							GameSoundUtils.Play(info.SE.Sound.GetHandle(info.SE.HandleIndex++));
							break;

						case PlayInfo.AlterCommand_e.STOP:
							for (int index = 0; index < GameSE.HANDLE_COUNT; index++)
							{
								GameSoundUtils.Stop(info.SE.Sound.GetHandle(index));
							}
							break;

						case PlayInfo.AlterCommand_e.LOOP:
							GameSoundUtils.Play(info.SE.Sound.GetHandle(0), false);
							break;

						default:
							throw new GameError();
					}
					return true;
				}
			}
			return false;
		}

		public static void Play(GameSE se)
		{
			int count = 0;

			foreach (PlayInfo info in PlayInfos.ToArray())
				if (info.SE == se && 2 <= ++count)
					return;

			PlayInfos.Enqueue(new PlayInfo(se));
			PlayInfos.Enqueue(null);
		}

		public static void Stop(GameSE se)
		{
			PlayInfos.Enqueue(new PlayInfo(se, PlayInfo.AlterCommand_e.STOP));
			PlayInfos.Enqueue(null);
		}

		public static void PlayLoop(GameSE se)
		{
			PlayInfos.Enqueue(new PlayInfo(se, PlayInfo.AlterCommand_e.LOOP));
			PlayInfos.Enqueue(null);
		}

		public static void UpdateVolume()
		{
			foreach (GameSE se in SEList)
				se.UpdateVolume();
		}
	}
}
