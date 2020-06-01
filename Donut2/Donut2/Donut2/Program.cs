using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Tests.Common;
using Charlotte.Test01;
using Charlotte.Test02;

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

		public void Main2(ArgsReader ar)
		{
			Main3();
		}

		private void Main3()
		{
			DDAdditionalEvents.Ground_INIT = () =>
			{
				//GameGround.RO_MouseDispMode = true;
			};

			DDAdditionalEvents.PostGameStart = () =>
			{
				// Font >

				DDFontRegister.Add(@"Font\Genkai-Mincho-font\genkai-mincho.ttf");
				DDFontRegister.Add(@"Font\riitf\RiiT_F.otf");

				// < Font

				Ground.I = new Ground();
			};

			DDAdditionalEvents.Save = lines =>
			{
				lines.Add(DateTime.Now.ToString()); // Dummy
				lines.Add(DateTime.Now.ToString()); // Dummy
				lines.Add(DateTime.Now.ToString()); // Dummy
			};

			DDAdditionalEvents.Load = lines =>
			{
				int c = 0;

				DDUtils.Noop(lines[c++]); // Dummy
				DDUtils.Noop(lines[c++]); // Dummy
				DDUtils.Noop(lines[c++]); // Dummy
			};

			DDMain2.Perform(Main4);
		}

		private void Main4()
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
			TitleMenu_Test02();
			//new InputStringDemo().Perform();
			//new InputStringDemo2().Perform();
		}

		private void TitleMenu_Test01()
		{
			new TitleMenu().Perform();
		}

		private void TitleMenu_Test02()
		{
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
