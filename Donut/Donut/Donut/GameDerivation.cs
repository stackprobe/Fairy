using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public class GameDerivation
	{
		public static void UnloadDer(int handle)
		{
			// TODO
		}

		public static void UnloadAllDer(List<int> derHandleList)
		{
			while (1 <= derHandleList.Count)
			{
				UnloadDer(ExtraTools.UnaddElement(derHandleList));
			}
		}
	}
}
