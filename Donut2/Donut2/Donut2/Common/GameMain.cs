using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameMain
	{
		public static void GameStart()
		{
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
