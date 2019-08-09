using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class GameUtils
	{
		public static byte[] SplitableJoin(string[] lines)
		{
			return BinTools.Join(lines.Select(line => Encoding.UTF8.GetBytes(line)).ToArray());
		}

		public static string[] Split(byte[] data)
		{
			return BinTools.Split(data).Select(bLine => Encoding.UTF8.GetString(bLine)).ToArray();
		}

		public static void Noop(params object[] dummyPrms)
		{
			// noop
		}
	}
}
