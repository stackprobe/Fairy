using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameMain2
	{
		public static void Perform(Action routine)
		{
			ExceptionDam.Section(eDam =>
			{
				eDam.Invoke(() =>
				{
					GameMain.GameStart();

					try
					{
						routine();
					}
					catch (GameCoffeeBreak)
					{ }

					GameMain.GameEnd();
				});

				GameMain.GameEnd2(eDam);
			});
		}
	}
}
