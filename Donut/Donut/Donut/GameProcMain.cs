using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public class GameProcMain
	{
		public void Main(Action gameInit, Action gameMain)
		{
#if false
			ProcMain.WriteLog = message =>
			{
#if DEBUG
				string file = @"C:\tmp\Game.log";
#else
				//string file = Path.Combine(ProcMain.SelfDir, Path.GetFileNameWithoutExtension(ProcMain.SelfFile) + ".log");
				string file = Path.Combine(ProcMain.SelfDir, ProcMain.APP_TITLE + ".log");
#endif

				using (StreamWriter writer = new StreamWriter(file, true, Encoding.UTF8))
				{

				}
			};
#endif

			HandleDam.Transaction(hDam =>
			{
				GameGround.I = new GameGround();
				GameGround.I.Finalizers = hDam;

				gameInit();

				Main2(gameMain);
			});
		}

		private void Main2(Action gameMain)
		{
			//
		}
	}
}
