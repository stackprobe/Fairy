using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Tests.Common
{
	public class DDResourceTest
	{
		public void Test01()
		{
			ProcMain.WriteLog(Encoding.ASCII.GetString(DDResource.Load(@"Fairy\Donut3\Resource テスト用\Dummy01.txt")));
			ProcMain.WriteLog(Encoding.ASCII.GetString(DDResource.Load(@"Fairy\Donut3\Resource テスト用\Dummy02.txt")));
			ProcMain.WriteLog(Encoding.ASCII.GetString(DDResource.Load(@"Fairy\Donut3\Resource テスト用\Dummy03.txt")));
		}

		public void Test02()
		{
			foreach (string file in DDResource.GetFiles())
			{
				ProcMain.WriteLog("resource file ==> " + file);
			}
		}
	}
}
