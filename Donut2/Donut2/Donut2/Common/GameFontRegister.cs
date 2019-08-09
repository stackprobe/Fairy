using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameFontRegister
	{
		private static WorkingDir WD;

		public static void INIT()
		{
			WD = new WorkingDir();
		}

		public static void FNLZ()
		{
			UnloadAll();

			WD.Dispose();
			WD = null;
		}

		public static void Add(string file)
		{
			Add(GameResource.Load(file), Path.GetFileName(file));
		}

		public static void Add(byte[] fileData, string localFile)
		{
			// TODO
		}

		private static void UnloadAll()
		{
			// TODO
		}
	}
}
