using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Tests.Common;

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
			GameMain2.Perform(Main3);
		}

		private void Main3()
		{
			//Test01();
			//new GameResourceTest().Test01();
			//new GamePictureTest().Test01();
			//new GameDrawTest().Test01();
			new GameFontRegisterTest().Test01();
		}

		private void Test01()
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
