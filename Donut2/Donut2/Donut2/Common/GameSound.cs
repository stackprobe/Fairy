using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class GameSound
	{
		private Func<byte[]> Func_GetFileData;
		private int HandleCount;
		private int[] Handles = null; // null == Unloaded

		public Action PostLoaded = () => { };

		public GameSound(string file, int handleCount)
			: this(() => GameResource.Load(file), handleCount)
		{ }

		public GameSound(Func<byte[]> getFileData, int handleCount)
		{
			this.Func_GetFileData = getFileData;
			this.HandleCount = handleCount;

			GameSoundUtils.Add(this);
		}

		public void Unload()
		{
			if (this.Handles != null)
			{
				foreach (int handle in this.Handles)
					if (DX.DeleteSoundMem(handle) != 0) // ? 失敗
						throw new GameError();

				this.Handles = null;
			}
		}

		public bool IsLoaded()
		{
			return this.Handles != null;
		}

		public int GetHandle(int index)
		{
			if (this.Handles == null)
			{
				this.Handles = new int[this.HandleCount];

				{
					byte[] fileData = this.Func_GetFileData();
					int handle = -1;

					GameSystem.PinOn(fileData, p => handle = DX.LoadSoundMemByMemImage(p, fileData.Length));

					if (handle == -1) // ? 失敗
						throw new GameError();

					this.Handles[0] = handle;
				}

				for (int ndx = 1; ndx < this.HandleCount; ndx++)
				{
					int handle = DX.DuplicateSoundMem(this.Handles[0]);

					if (handle == -1) // ? 失敗
						throw new GameError();

					this.Handles[ndx] = handle;
				}

				this.PostLoaded();
			}
			return this.Handles[index];
		}
	}
}
