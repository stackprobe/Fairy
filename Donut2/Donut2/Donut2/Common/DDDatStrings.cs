﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class DDDatStrings
	{
		private static Dictionary<string, string> Name2Value = DictionaryTools.Create<string>();

		public static void INIT()
		{
			string[] lines = FileTools.TextToLines(StringTools.ENCODING_SJIS.GetString(DDResource.Load("DatStrings.txt")));

			foreach (string line in lines)
			{
				int p = line.IndexOf('=');

				if (p == -1)
					throw new DDError();

				string name = line.Substring(0, p);
				string value = line.Substring(p + 1);

				Name2Value.Add(name, value);
			}
		}

		private static string GetValue(string name)
		{
			if (Name2Value.ContainsKey(name) == false)
				throw new DDError(name);

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

		// < Accessor
	}
}
