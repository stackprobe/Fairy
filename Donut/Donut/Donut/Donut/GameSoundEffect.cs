using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public static class GameSoundEffect
	{
		public const int SE_HANDLE_MAX = 64;

		public class SEInfo : IDisposable
		{
			public int[] HandleList = new int[SE_HANDLE_MAX];
			public int HandleIndex;
			public double Volume; // 0.0 - 1.0, def: 0.5

			public void Dispose() // Codevil の UnloadSE()
			{
				PlayList.Clear();

				for (int index = 0; index < SE_HANDLE_MAX; index++)
				{
					GameSound.UnloadSound(this.HandleList[index]);
				}
			}
		}

		public class SEInfoEx
		{
			public SEInfo Info;
			public char AlterCommand;
		}

		private static Queue<SEInfoEx> PlayList = new Queue<SEInfoEx>();

		private static void UpdateSEVolumeFunc(SEInfo i)
		{
			for (int index = 0; index < SE_HANDLE_MAX; index++)
			{
				GameSound.SetVolume(i.HandleList[index], GameSound.MixVolume(GameGround.I.SEVolume, i.Volume));
			}
		}

		private static SEInfo LoadSE(byte[] fileData)
		{
			SEInfo i = new SEInfo();

			i.HandleList[0] = GameSound.LoadSound(fileData);

			for (int index = 1; index < SE_HANDLE_MAX; index++)
			{
				i.HandleList[index] = GameSound.DuplSound(i.HandleList[0]);
			}
			i.Volume = 0.5; // 個別の音量のデフォルト 0.0 - 1.0

			// TODO ??? post LoadSound

			UpdateSEVolumeFunc(i);
			return i;
		}

		private static void UnloadSE(SEInfo i)
		{
			i.Dispose();
		}

		private static OneObject<ResourceCluster<SEInfo>> SERes =
			new OneObject<ResourceCluster<SEInfo>>(
				() => new ResourceCluster<SEInfo>("SoundEffect.dat", @"..\..\..\..\SoundEffect.txt", LoadSE, UnloadSE)
				);

		public static bool SEEachFrame() // ret: 効果音を処理した。
		{
			if (1 <= PlayList.Count)
			{
				SEInfoEx i = PlayList.Dequeue();

				switch (i.AlterCommand)
				{
					case '\0':
						i.Info.HandleIndex %= SE_HANDLE_MAX;
						GameSound.SoundPlay(i.Info.HandleList[i.Info.HandleIndex++]);
						break;

					case 'S':
						for (int index = 0; index < SE_HANDLE_MAX; index++)
						{
							GameSound.SoundStop(i.Info.HandleList[index]);
						}
						break;

					case 'L':
						GameSound.SoundPlay(i.Info.HandleList[0], false);
						break;

					default:
						throw new GameError();
				}
				return true;
			}
			return false;
		}

		public static void SEPlay(int seId)
		{
			SEInfo i = SERes.I.GetHandle(seId);
			int count = 0;

			foreach (SEInfoEx info in PlayList.ToArray())
				if (info.Info == i && 2 <= ++count)
					return;

			PlayList.Enqueue(new SEInfoEx() { Info = i });
			PlayList.Enqueue(null);
		}

		private static void DoAlterCommand(int seId, char alterCommand)
		{
			SEInfo i = SERes.I.GetHandle(seId);

			PlayList.Enqueue(new SEInfoEx()
			{
				Info = i,
				AlterCommand = alterCommand,
			});

			PlayList.Enqueue(null);
		}

		public static void SEStop(int seId)
		{
			DoAlterCommand(seId, 'S');
		}

		public static void SEPlayLoop(int seId)
		{
			DoAlterCommand(seId, 'L');
		}

		public static void UpdateSEVolume()
		{
			SERes.I.CallAllLoadedHandle(UpdateSEVolumeFunc);
		}
	}
}
