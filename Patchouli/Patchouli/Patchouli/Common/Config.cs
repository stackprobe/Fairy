using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class Config
	{
		// 設定項目 >

		// -1 == デフォルト
		// 0  == 最初のモニタ
		// 1  == 2番目のモニタ
		// 2  == 3番目のモニタ
		// ...
		public static int DisplayIndex = 1;

		// < 設定項目

		public void Import()
		{
			// TODO
		}
	}
}
