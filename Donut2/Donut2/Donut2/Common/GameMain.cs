using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Common
{
	public static class GameMain
	{
		private static bool LowWrote = false;

		public static void GameStart()
		{
			ProcMain.WriteLog = message =>
			{
				const string logFile = @"C:\tmp\Game.log";

				using (StreamWriter writer = new StreamWriter(logFile, LowWrote, Encoding.UTF8))
				{
					writer.WriteLine("[" + DateTime.Now + "] " + message);
				}
				LowWrote = true;
			};

			// *.INIT
			{
				GameGround.INIT();
				GameResource.INIT();
			}
		}

		public static void GameEnd()
		{
			// *.FNLZ
			{
				GameResource.FNLZ();
				GameGround.FNLZ();
			}
		}
	}
}
