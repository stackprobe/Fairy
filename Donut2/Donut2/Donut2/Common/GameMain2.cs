﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameMain2
	{
		public static void Perform(Action routine)
		{
			try
			{
				GameMain.GameStart();
				routine();
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
