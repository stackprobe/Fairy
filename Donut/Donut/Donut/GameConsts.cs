using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class GameConsts
	{
#if DEBUG
		public static bool DEBUG_MODE = true;
#else
		public static bool DEBUG_MODE = false;
#endif
	}
}
