﻿using System;
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

		public const int SCREEN_W_MIN = 100;
		public const int SCREEN_H_MIN = 100;
		public const int SCREEN_W_MAX = 4000;
		public const int SCREEN_H_MAX = 3000;
	}
}
