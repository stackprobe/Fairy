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
			string dir = WD.MakePath();
			string file = Path.Combine(dir, localFile);

			FileTools.CreateDir(dir);
			File.WriteAllBytes(file, fileData);

			if (GameWin32.AddFontResourceEx(file, GameWin32.FR_PRIVATE, IntPtr.Zero) == 0) // ? 失敗
				throw new GameError();

			FontFiles.Add(file);
		}

		private static List<string> FontFiles = new List<string>();

		private static void Unload(string file)
		{
			if (GameWin32.RemoveFontResourceEx(file, GameWin32.FR_PRIVATE, IntPtr.Zero) == 0) // ? 失敗
				throw new GameError();
		}

		private static void UnloadAll()
		{
			foreach (string file in FontFiles)
				Unload(file);
		}
	}
}
