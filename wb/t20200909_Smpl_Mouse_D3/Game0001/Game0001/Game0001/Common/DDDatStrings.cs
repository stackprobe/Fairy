using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
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

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static Dictionary<string, string> Name2Value = DictionaryTools.Create<string>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
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

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static string GetValue(string name)
		{
			if (Name2Value.ContainsKey(name) == false)
				throw new DDError(name);

			return Name2Value[name];
		}

		// Accessor >

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string Title
		{
			get { return GetValue("Title"); }
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string Author
		{
			get { return GetValue("Author"); }
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string Copyright
		{
			get { return GetValue("Copyright"); }
		}

		// < Accessor
	}
}
