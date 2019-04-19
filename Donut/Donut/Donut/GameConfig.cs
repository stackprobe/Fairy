using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class GameConfig
	{
		public void LoadConfig()
		{
			// TODO
		}

		// "0.01"
		// "0.02"
		// "1.00"
		// etc.
		public string Version = "1.00";

		// -1 == デフォルト
		// 0  == 最初のモニタ
		// 1  == 2番目のモニタ
		// 2  == 3番目のモニタ
		// ...
		public int DisplayIndex = 1;
	}
}
