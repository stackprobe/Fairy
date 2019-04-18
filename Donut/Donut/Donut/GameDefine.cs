using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class GameDefine
	{
#if DEBUG
		public const bool DEBUG_MODE = true;
#else
		public const bool DEBUG_MODE = false;
#endif

		public static readonly I2Size SCREEN_SIZE = new I2Size(800, 600);
	}
}
