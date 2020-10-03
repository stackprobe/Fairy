using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class DDConfig
	{
		// 設定項目 >

		/// <summary>
		/// <para>-1 == デフォルト</para>
		/// <para>0  == 最初のモニタ</para>
		/// <para>1  == 2番目のモニタ</para>
		/// <para>2  == 3番目のモニタ</para>
		/// <para>...</para>
		/// </summary>
		public static int DisplayIndex = -1;

		public static string LogFile = @"C:\tmp\Game.log";
		public static int LogCountMax = IntTools.IMAX;
		public static bool LOG_ENABLED = true;
		public static string ApplicationLogSaveDirectory = @"C:\tmp";

		// < 設定項目

		public static void Load()
		{
			if (File.Exists(DDConsts.ConfigFile) == false)
				return;

			string[] lines = File.ReadAllLines(DDConsts.ConfigFile, StringTools.ENCODING_SJIS).Select(line => line.Trim()).Where(line => line != "" && line[0] != ';').ToArray();
			int c = 0;

			if (lines.Length != int.Parse(lines[c++]))
				throw new DDError();

			// 設定項目 >

			DisplayIndex = int.Parse(lines[c++]);
			LogFile = lines[c++];
			LogCountMax = int.Parse(lines[c++]);
			LOG_ENABLED = int.Parse(lines[c++]) != 0;
			ApplicationLogSaveDirectory = lines[c++];

			// < 設定項目
		}
	}
}
