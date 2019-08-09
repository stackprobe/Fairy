using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameMain2
	{
		public static void Main(Action gMain)
		{
			try
			{
				GameMain.GameStart();
				gMain();
				GameMain.GameEnd();
			}
			catch
			{
				GameMain.GameErrorEnd();
				throw;
			}
		}
	}
}
