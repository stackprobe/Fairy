using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class DD
	{
		public class EndProc : Exception
		{ }

		public class Error : Exception
		{
			public Error(string message = null)
				: base(message)
			{ }
		}

		// DD >

		// < DD
	}
}
