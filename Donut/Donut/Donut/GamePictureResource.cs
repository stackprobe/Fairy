using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GamePictureResource : ResourceCluster<GamePicture.PicInfo>
	{
		public GamePictureResource(string clusterFile, string fileListFile)
			: base(clusterFile, fileListFile)
		{ }

		protected override GamePicture.PicInfo LoadHandle(byte[] rawData)
		{
			return GamePicture.GraphicHandle2PicInfo(GamePicture.SoftImage2GraphicHandle(GamePicture.FileData2SoftImage(rawData)));
		}

		protected override void UnloadHandle(GamePicture.PicInfo handle)
		{
			handle.Dispose();
		}
	}
}
