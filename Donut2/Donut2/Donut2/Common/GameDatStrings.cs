using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameDatStrings
	{
		private static Dictionary<string, string> Name2Value = DictionaryTools.Create<string>();

		public static void INIT()
		{
			string[] lines = FileTools.TextToLines(StringTools.ENCODING_SJIS.GetString(GameResource.Load("DatStrings.txt")));

			foreach (string line in lines)
			{
				int p = line.IndexOf('=');

				if (p == -1)
					throw new GameError();

				string name = line.Substring(0, p);
				string value = line.Substring(p + 1);

				Name2Value.Add(name, value);
			}
		}

		public static void FNLZ()
		{
			// noop
		}

		private static string GetValue(string name)
		{
			if (Name2Value.ContainsKey(name) == false)
				throw new GameError();

			return Name2Value[name];
		}

		// Accessor >

		public static string Title
		{
			get { return GetValue("Title"); }
		}

		public static string Author
		{
			get { return GetValue("Author"); }
		}

		public static string Copyright
		{
			get { return GetValue("Copyright"); }
		}

		// app > @ Accessor

		// < app

		// < Accessor
	}
}
