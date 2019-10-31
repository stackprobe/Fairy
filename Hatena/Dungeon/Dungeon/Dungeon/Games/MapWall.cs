using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games
{
	public class MapWall
	{
		public Kind_e Kind = Kind_e.NONE;
		public string Script = null; // null == スクリプト無し

		// <---- prm

		public enum Kind_e
		{
			NONE = 1,
			WALL,
			GATE,
		}
	}
}
