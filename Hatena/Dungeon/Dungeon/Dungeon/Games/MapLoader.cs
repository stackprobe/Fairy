using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;
using System.Text.RegularExpressions;
using System.IO;
using Charlotte.MakeMaps;

namespace Charlotte.Games
{
	public static class MapLoader
	{
		public static Map Load(DungeonMap dungMap)
		{
			return new Map(dungMap);
		}
	}
}
