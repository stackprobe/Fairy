using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class GameError : Exception
	{
		public GameError(string message = "エラーが発生しました。")
			: base(message)
		{ }
	}
}
