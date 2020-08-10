using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Tests.Common
{
	public class DDPrintTest
	{
		public void Test01()
		{
			for (; ; )
			{
				DDPrint.SetBorder(new I3Color(0, 64, 192));
				DDPrint.SetPrint();
				DDPrint.Print("aaaいろは");
				DDPrint.Print("にほへ123");
				DDPrint.Print("日本語！");
				DDPrint.Print("9999");
				DDPrint.Reset();

				DDEngine.EachFrame();
			}
		}
	}
}
