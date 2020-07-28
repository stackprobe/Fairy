using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Tests;
using Charlotte.Test01;

namespace Charlotte
{
	public class Program2
	{
		public void Main2()
		{
			try
			{
				Main3();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void Main3()
		{
			DDAdditionalEvents.Ground_INIT = () =>
			{
				//DDGround.RO_MouseDispMode = true;
			};

			DDAdditionalEvents.PostGameStart = () =>
			{
				// Font >

				//DDFontRegister.Add(@"Font\Genkai-Mincho-font\genkai-mincho.ttf");

				// < Font

				Ground.I = new Ground();
			};

			DDAdditionalEvents.Save = lines =>
			{
				lines.Add((DDGround.RO_MouseDispMode ? 1 : 0).ToString());

				//lines.Add(DateTime.Now.ToString()); // Dummy
				//lines.Add(DateTime.Now.ToString()); // Dummy
				//lines.Add(DateTime.Now.ToString()); // Dummy

				// 新しい項目をここへ追加...
			};

			DDAdditionalEvents.Load = lines =>
			{
				int c = 0;

				DDGround.RO_MouseDispMode = int.Parse(lines[c++]) != 0;

				//DDUtils.Noop(lines[c++]); // Dummy
				//DDUtils.Noop(lines[c++]); // Dummy
				//DDUtils.Noop(lines[c++]); // Dummy

				// 新しい項目をここへ追加...

				// ----

				DDUtils.SetMouseDispMode(DDGround.RO_MouseDispMode); // ここで再設定するしかない。Hack
			};

			DDMain2.Perform(Main4);
		}

		private void Main4()
		{
			if (ProcMain.ArgsReader.ArgIs("/D"))
			{
				Main4_Debug();
			}
			else
			{
				Main4_Release();
			}
		}

		private void Main4_Debug()
		{
			//new Test0001().Test01();
			new TitleMenu().Perform();
		}

		private void Main4_Release()
		{
			throw null;
		}
	}
}
