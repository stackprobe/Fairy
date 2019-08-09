using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Tests.Common
{
	public class GameFontRegisterTest
	{
		public void Test01()
		{
			GameFontRegister.Add(@"Font\Genkai-Mincho-font\genkai-mincho.ttf");
			GameFontRegister.Add(@"Font\riitf\RiiT_F.otf");
		}
	}
}
