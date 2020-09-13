using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.Test01;
using Charlotte.Test02;
using Charlotte.Tests.Common;
using Charlotte.Tests.Common.Options;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{54b6bdb8-53bb-4403-be55-1461dcf873de}";
		public const string APP_TITLE = "Donut3";

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

		public void Main2(ArgsReader ar)
		{
			DDMain2.Perform(Main3);
		}

		private void Main3()
		{
			//Test01();
			//new DDResourceTest().Test01();
			//new DDResourceTest().Test02();
			//new DDPictureTest().Test01();
			//new DDDrawTest().Test01();
			//new DDDrawTest().Test02();
			//new DDFontRegisterTest().Test01();
			//new DDKeyTest().Test01();
			//new DDPrintTest().Test01();
			//new DDPadTest().Test01();
			//new DDMouseTest().Test01();
			//TitleMenu_Test01();
			//TitleMenu_Test02();
			//new InputStringDemo().Perform();
			//new InputStringDemo2().Perform();
			new DDCResourceTest().Test01();
		}

		private void TitleMenu_Test01()
		{
			DDCurtain.SetCurtain(0, -1.0); // 一旦暗転する。

			new TitleMenu().Perform();
		}

		private void TitleMenu_Test02()
		{
			DDCurtain.SetCurtain(0, -1.0); // 一旦暗転する。

			DDUtils.SetMouseDispMode(true);
			new TitleMenu().Perform();
			DDUtils.SetMouseDispMode(false);
		}

		private void Test01()
		{
			for (; ; )
			{
				if (DDInput.A.GetInput() == 1)
				{
					break;
				}

				DDCurtain.DrawCurtain();

				DDPrint.SetPrint();
				DDPrint.Print("FrameProcessingMillis: " + DDEngine.FrameProcessingMillis);

				DDDraw.DrawBegin(DDGround.GeneralResource.WhiteBox, DDConsts.Screen_W / 2.0, DDConsts.Screen_H / 2.0);
				DDDraw.DrawZoom(10.0);
				DDDraw.DrawRotate(DDEngine.ProcFrame / 20.0);
				DDDraw.DrawEnd();

				DDEngine.EachFrame();
			}
		}
	}
}
