﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class DDError : Exception
	{
		public DDError(string message = "エラーが発生しました。")
			: base(message)
		{ }
	}
}
