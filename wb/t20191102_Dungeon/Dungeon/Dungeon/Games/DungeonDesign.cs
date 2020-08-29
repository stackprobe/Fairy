using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public class DungeonDesign
	{
		public const int DUNG_SCREEN_W = 970;
		public const int DUNG_SCREEN_H = 530;

		public static readonly D4Rect FRONT_WALL_0 = new D4Rect(30 * 0, 24 * 0, DUNG_SCREEN_W - (30 + 30) * 0, DUNG_SCREEN_H - (24 + 8) * 0);
		public static readonly D4Rect FRONT_WALL_1 = new D4Rect(30 * 8, 24 * 8, DUNG_SCREEN_W - (30 + 30) * 8, DUNG_SCREEN_H - (24 + 8) * 8);
		public static readonly D4Rect FRONT_WALL_2 = new D4Rect(30 * 12, 24 * 12, DUNG_SCREEN_W - (30 + 30) * 12, DUNG_SCREEN_H - (24 + 8) * 12);
		public static readonly D4Rect FRONT_WALL_3 = new D4Rect(30 * 14, 24 * 14, DUNG_SCREEN_W - (30 + 30) * 14, DUNG_SCREEN_H - (24 + 8) * 14);
		public static readonly D4Rect FRONT_WALL_4 = new D4Rect(30 * 15, 24 * 15, DUNG_SCREEN_W - (30 + 30) * 15, DUNG_SCREEN_H - (24 + 8) * 15);

		public static readonly D4Rect WALK_FRONT_WALL_0 = new D4Rect(30 * -8, 24 * -8, DUNG_SCREEN_W - (30 + 30) * -16, DUNG_SCREEN_H - (24 + 8) * -8);
		public static readonly D4Rect WALK_FRONT_WALL_1 = new D4Rect(30 * 4, 24 * 4, DUNG_SCREEN_W - (30 + 30) * 4, DUNG_SCREEN_H - (24 + 8) * 4);
		public static readonly D4Rect WALK_FRONT_WALL_2 = new D4Rect(30 * 10, 24 * 10, DUNG_SCREEN_W - (30 + 30) * 10, DUNG_SCREEN_H - (24 + 8) * 10);
		public static readonly D4Rect WALK_FRONT_WALL_3 = new D4Rect(30 * 13, 24 * 13, DUNG_SCREEN_W - (30 + 30) * 13, DUNG_SCREEN_H - (24 + 8) * 13);
		public static readonly D4Rect WALK_FRONT_WALL_4 = new D4Rect(30 * 14.5, 24 * 14.5, DUNG_SCREEN_W - (30 + 30) * 14.5, DUNG_SCREEN_H - (24 + 8) * 14.5);
	}
}
