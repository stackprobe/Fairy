using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class GameError : Exception
	{
		public GameError(string message = "Sorry, An error has occurred")
			: base(message)
		{ }
	}
}
