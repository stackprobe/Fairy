using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class DDDatStrings
	{
		// 新しいプロジェクトを作成したら DatStrings.txt をプロジェクトのリソースに設置し、DatStringsFile の指し先を変更すること。
		// Donut3 側で新しい項目が追加されたら、手動で DatStrings.txt に追加する必要がある。
		// ややっこしくなるので、プロジェクト独自の項目を追加したりなどしないこと。

#if false // 例
		// discmt >

		private const string DatStringsFile = @"Fairy\Donut3\DatStrings.txt";
		//private const string DatStringsFile = @"Fairy\Donut3\DatStrings_v0001.txt";
		//private const string DatStringsFile = @"Fairy\Donut3\DatStrings_v0002.txt";
		//private const string DatStringsFile = @"Fairy\Donut3\DatStrings_v0003.txt";

		// < discmt
#else
		// app > @ DatStringsFile

		private const string DatStringsFile = @"Fairy\Donut3\DatStrings.txt";

		// < app
#endif

		private static Dictionary<string, string> Name2Value = DictionaryTools.Create<string>();

		public static void INIT()
		{
			string[] lines = FileTools.TextToLines(StringTools.ENCODING_SJIS.GetString(DDResource.Load(DatStringsFile)));

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
