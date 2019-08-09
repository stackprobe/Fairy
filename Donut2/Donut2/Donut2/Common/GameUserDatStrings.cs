using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameUserDatStrings
	{
		private static Dictionary<string, string> Name2Value = DictionaryTools.Create<string>();

		public static void INIT()
		{
			IEnumerable<string> lines = File.ReadAllLines(GameConsts.UserDatStringsFile, StringTools.ENCODING_SJIS).Select(line => line.Trim()).Where(line => line != "" && line[0] != ';');

			foreach (string line in lines)
			{
				int p = line.IndexOf('=');

				if (p == -1)
					throw new GameError();

				string name = line.Substring(0, p).Trim();
				string value = line.Substring(p + 1).Trim();

				Name2Value.Add(name, value);
			}
		}

		public static void FNLZ()
		{
			// noop
		}

		private static string GetValue(string name, string defval)
		{
			if (Name2Value.ContainsKey(name) == false)
				return defval;

			return Name2Value[name];
		}

		// Accessor >

		public static string Version
		{
			get { return GetValue("Version", "0.00"); }
		}

		// app > @ Accessor

		// < app

		// < Accessor
	}
}
