using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.Donut;
using Charlotte.Donut;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{1f608f74-e1d0-4300-9193-384d2f71db78}";
		public const string APP_TITLE = "Donut";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

			//Console.WriteLine("Press ENTER.");
			//Console.ReadLine();
		}

		private void Main2(ArgsReader ar)
		{
			new GameProcMain().Main(() => { }, new GameMain().Main);
		}
	}
}
