using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	/// <summary>
	/// Codevil の GameTools2
	/// </summary>
	public class GameToolkit2
	{
		private const int POUND_FIRST_DELAY = 17;
		private const int POUND_DELAY = 4;

		public static bool IsPound(int counter)
		{
			return counter == 1 || (POUND_FIRST_DELAY < counter && (counter - POUND_FIRST_DELAY) % POUND_DELAY == 1);
		}
	}
}
