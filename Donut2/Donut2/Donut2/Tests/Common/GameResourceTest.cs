using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Tests.Common
{
	public class GameResourceTest
	{
		public void Test01()
		{
			ProcMain.WriteLog(Encoding.ASCII.GetString(GameResource.Load(@"Resource テスト用\Dummy01.txt")));
			ProcMain.WriteLog(Encoding.ASCII.GetString(GameResource.Load(@"Resource テスト用\Dummy02.txt")));
			ProcMain.WriteLog(Encoding.ASCII.GetString(GameResource.Load(@"Resource テスト用\Dummy03.txt")));
		}
	}
}
