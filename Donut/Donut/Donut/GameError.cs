using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class GameError : Exception
	{
		public GameError()
		{ }

		public GameError(string message)
			: base(message)
		{ }
	}
}
