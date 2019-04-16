using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class Define
	{
#if DEBUG
		public const bool LOG_ENABLED = true;
#else
		public const bool LOG_ENABLED = false;
#endif

		// app > @ define SCREEN_WH

		public const int SCREEN_W = 800;
		public const int SCREEN_H = 600;

		// < app
	}
}
