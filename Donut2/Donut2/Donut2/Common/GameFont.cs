using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class GameFont
	{
		private WorkingDir WD = new WorkingDir();

		public GameFont(string file)
			: this(() => GameResource.Load(file))
		{ }

		public GameFont(Func<byte[]> getFileData)
		{
			//
		}

		public void Load()
		{
		}

		public void Unload()
		{
		}
	}
}
