using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class DDMain2
	{
		public static void Perform(Action routine)
		{
			ExceptionDam.Section(eDam =>
			{
				eDam.Invoke(() =>
				{
					DDMain.GameStart();

					try
					{
						routine();
					}
					catch (DDCoffeeBreak)
					{ }

					DDMain.GameEnd();
				});

				DDMain.GameEnd2(eDam);
			});
		}
	}
}
