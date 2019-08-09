using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameConfig
	{
		// 設定項目 >

		/// <summary>
		/// -1 == デフォルト
		/// 0  == 最初のモニタ
		/// 1  == 2番目のモニタ
		/// 2  == 3番目のモニタ
		/// ...
		/// </summary>
		public static int DisplayIndex = 1;

		public static string LogFile = @"C:\tmp\Game.log";
		public static bool LOG_ENABLED = true;

		// < 設定項目

		public static void Load()
		{
			if (File.Exists(GameConsts.ConfigFile) == false)
				return;

			string[] lines = File.ReadAllLines(GameConsts.ConfigFile, StringTools.ENCODING_SJIS).Select(line => line.Trim()).Where(line => line != "" && line[0] != ';').ToArray();
			int c = 0;

			if (lines.Length != int.Parse(lines[c++]))
				throw new GameError();

			// 設定項目 >

			DisplayIndex = int.Parse(lines[c++]);
			LogFile = lines[c++];
			LOG_ENABLED = int.Parse(lines[c++]) != 0;

			// < 設定項目
		}
	}
}
