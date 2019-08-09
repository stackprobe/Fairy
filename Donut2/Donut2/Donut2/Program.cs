using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{7b951036-ee8d-45f9-a717-6c20844547e3}";
		public const string APP_TITLE = "Donut2";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				//Console.WriteLine("Press ENTER.");
				//Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			GameMain.GameStart();
			try
			{
				Main3();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
			finally
			{
				GameMain.GameEnd();
			}
		}

		private void Main3()
		{
			for (; ; )
			{
				if (GameInput.A.IsPress())
				{
					break;
				}

				GameEngine.EachFrame();
			}
		}
	}
}
