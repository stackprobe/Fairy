using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public abstract class ResourceCluster<Handle_t>
	{
		private enum ResMode_e
		{
			CLUSTER = 1,
			FILELIST,
		};

		private string ClusterFile;
		private string FileListFile;
		private ResMode_e ResMode;
		private ResourceClusterFile RCF = null;
		private string[] FileList = null;
		private int ResCount;
		private Handle_t[] HandleList;
		private bool[] LoadedList;

		public ResourceCluster(string clusterFile, string fileListFile)
		{
			this.ClusterFile = clusterFile;
			this.FileListFile = fileListFile;

			if (File.Exists(this.ClusterFile))
			{
				this.ResMode = ResMode_e.CLUSTER;
				this.RCF = new ResourceClusterFile(this.ClusterFile);
				this.ResCount = this.RCF.GetResCount();
			}
			else
			{
				this.ResMode = ResMode_e.FILELIST;
				this.FileList = File.ReadAllLines(this.FileListFile, StringTools.ENCODING_SJIS);
				this.ResCount = this.FileList.Length;
			}
			this.HandleList = new Handle_t[this.ResCount];
			this.LoadedList = new bool[this.ResCount];
		}

		public Handle_t GetHandle(int resId)
		{
			if (this.LoadedList[resId] == false)
			{
				byte[] rawData;

				if (this.ResMode == ResMode_e.CLUSTER)
				{
					rawData = this.RCF.LoadResFile(resId);
				}
				else
				{
					string file = this.FileList[resId];

					if (file[1] != ':')
						file = @"..\..\..\..\" + file;

					rawData = File.ReadAllBytes(file);
				}
				this.HandleList[resId] = this.LoadHandle(rawData);
				this.LoadedList[resId] = true;
			}
			return this.HandleList[resId];
		}

		public void UnloadAllHandle()
		{
			for (int resId = 0; resId < this.ResCount; resId++)
			{
				if (this.LoadedList[resId])
				{
					this.UnloadHandle(this.HandleList[resId]);
					this.LoadedList[resId] = false;
				}
			}
		}

		public void CallAllLoadedHandle(Action<Handle_t> routine)
		{
			for (int resId = 0; resId < this.ResCount; resId++)
			{
				if (this.LoadedList[resId])
				{
					routine(this.HandleList[resId]);
				}
			}
		}

		protected abstract Handle_t LoadHandle(byte[] rawData);
		protected abstract void UnloadHandle(Handle_t handle);
	}
}
