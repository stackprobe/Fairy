using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public class ResourceCluster<Handle_t>
	{
		private enum ResMode_e
		{
			CLUSTER = 1,
			FILELIST,
		};

		private string ClusterFile;
		private string FileListFile;
		private Func<byte[], Handle_t> HandleLoader;
		private Action<Handle_t> HandleUnloader;
		private ResMode_e ResMode;
		private ResourceClusterFile RCF = null;
		private string[] FileList = null;
		private int ResCount;
		private Handle_t[] HandleList;
		private bool[] LoadedList;

		public ResourceCluster(string clusterFile, string fileListFile, Func<byte[], Handle_t> handleLoader, Action<Handle_t> handleUnloader)
		{
			this.ClusterFile = clusterFile;
			this.FileListFile = fileListFile;
			this.HandleLoader = handleLoader;
			this.HandleUnloader = handleUnloader;

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
				this.HandleList[resId] = this.HandleLoader(rawData);
				this.LoadedList[resId] = true;
			}
			return this.HandleList[resId];
		}

		public List<int> DerHandleList = new List<int>(); // for Derivation.cs

		public void UnloadAllHandle()
		{
			GameDerivation.UnloadAllDer(DerHandleList);

			for (int resId = 0; resId < this.ResCount; resId++)
			{
				if (this.LoadedList[resId])
				{
					this.HandleUnloader(this.HandleList[resId]);
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
	}
}
