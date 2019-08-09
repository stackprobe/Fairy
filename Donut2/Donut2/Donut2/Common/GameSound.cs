using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class GameSound : IDisposable
	{
		private Func<byte[]> Func_GetRawData;
		private int[] Handles;

		public GameSound(string file, int count)
			: this(() => GameResource.Load(file), count)
		{ }

		public GameSound(Func<byte[]> getRawData, int count)
		{
			this.Func_GetRawData = getRawData;
			this.Handles = IntTools.Sequence(-1, count, 0).ToArray();

			GameSoundUtils.Add(this);
		}

		public bool IsHandleLoaded(int index)
		{
			return this.Handles[index] != -1;
		}

		public int GetHandle(int index)
		{
			if (this.Handles[0] == -1)
			{
				byte[] rawData = this.Func_GetRawData();
				int handle = -1;

				GameSystem.PinOn(rawData, p => handle = DX.LoadSoundMemByMemImage(p, rawData.Length));

				if (handle == -1) // ? 失敗
					throw new GameError();

				this.Handles[0] = handle;
			}
			if (this.Handles[index] == -1)
			{
				int handle = DX.DuplicateSoundMem(this.Handles[0]);

				if (handle == -1) // ? 失敗
					throw new GameError();

				this.Handles[index] = handle;
			}
			return this.Handles[index];
		}

		public void Dispose()
		{
			if (GameSoundUtils.Remove(this) == false) // ? Already disposed
				return;

			this.Unload();

			this.Func_GetRawData = null;
			this.Handles = null;
		}

		public void Unload()
		{
			for (int index = 0; index < this.Handles.Length; index++)
			{
				if (this.Handles[index] != -1)
				{
					if (DX.DeleteSoundMem(this.Handles[index]) != 0) // ? 失敗
						throw new GameError();

					this.Handles[index] = -1;
				}
			}
		}
	}
}
