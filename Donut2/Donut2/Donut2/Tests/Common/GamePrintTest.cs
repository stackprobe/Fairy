using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Tests.Common
{
	public class GamePrintTest
	{
		public void Test01()
		{
			for (; ; )
			{
				GamePrint.SetBorder(new I3Color(0, 64, 192));
				GamePrint.SetPrint();
				GamePrint.Print("aaaいろは");
				GamePrint.Print("にほへ123");
				GamePrint.Print("日本語！");
				GamePrint.Print("9999");
				GamePrint.Reset();

				GameEngine.EachFrame();
			}
		}
	}
}
