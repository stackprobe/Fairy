using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Utilities
	{
		public static int RotL(int direction)
		{
			return direction / 2 + ((direction / 2) % 2) * 5;
		}

		public static int RotR(int direction)
		{
			return direction * 2 - (direction / 6) * 10;
		}
	}
}
