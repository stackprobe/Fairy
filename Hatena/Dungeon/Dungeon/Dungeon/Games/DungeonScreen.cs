using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public static class DungeonScreen
	{
		public static DDSubScreen DungScreen = new DDSubScreen(DungeonDesign.DUNG_SCREEN_W, DungeonDesign.DUNG_SCREEN_H);

		private static IDungeonLayout Layout;

		public static void DrawFront(IDungeonLayout layout, bool walk = false)
		{
			Layout = layout;
			DrawFront_Main(walk);
			Layout = null;
		}

		private static void DrawFront_Main(bool walk)
		{
			using (DungScreen.Section())
			{
				DDDraw.DrawRect(Layout.GetBackgroundPicture(), 0, 0, DungeonDesign.DUNG_SCREEN_W, DungeonDesign.DUNG_SCREEN_H);

				if (walk)
				{
					DrawLayer(DungeonDesign.WALK_FRONT_WALL_4, DungeonDesign.WALK_FRONT_WALL_3, 3);
					DrawLayer(DungeonDesign.WALK_FRONT_WALL_3, DungeonDesign.WALK_FRONT_WALL_2, 2);
					DrawLayer(DungeonDesign.WALK_FRONT_WALL_2, DungeonDesign.WALK_FRONT_WALL_1, 1);
					DrawLayer(DungeonDesign.WALK_FRONT_WALL_1, DungeonDesign.WALK_FRONT_WALL_0, 0);
				}
				else
				{
					DrawLayer(DungeonDesign.FRONT_WALL_4, DungeonDesign.FRONT_WALL_3, 3);
					DrawLayer(DungeonDesign.FRONT_WALL_3, DungeonDesign.FRONT_WALL_2, 2);
					DrawLayer(DungeonDesign.FRONT_WALL_2, DungeonDesign.FRONT_WALL_1, 1);
					DrawLayer(DungeonDesign.FRONT_WALL_1, DungeonDesign.FRONT_WALL_0, 0);
				}
			}
		}

		private static void DrawLayer(D4Rect frontBaseRect, D4Rect behindBaseRect, int y)
		{
			DrawDungWall(Layout.GetWall(0, y, 8), frontBaseRect.Poly, y + 0.5);

			int x;

			for (x = 1; ; x++)
			{
				D4Rect frontRect = frontBaseRect;

				frontRect.L = frontBaseRect.L + x * frontBaseRect.W;

				if (DungeonDesign.DUNG_SCREEN_W <= frontRect.L)
					break;

				DrawDungWall(Layout.GetWall(x, y, 8), frontRect.Poly, y + 0.5);

				frontRect.L = frontBaseRect.L - x * frontBaseRect.W;

				DrawDungWall(Layout.GetWall(-x, y, 8), frontRect.Poly, y + 0.5);
			}
			for (x -= 2; 0 <= x; x--)
			{
				D4Rect frontRect = frontBaseRect;
				D4Rect behindRect = behindBaseRect;

				frontRect.L = frontBaseRect.L + x * frontBaseRect.W;
				behindRect.L = behindBaseRect.L + x * behindBaseRect.W;

				DrawDungWall(Layout.GetWall(x, y, 6), new P4Poly(frontRect.RT, behindRect.RT, behindRect.RB, frontRect.RB), y);

				frontRect.L = frontBaseRect.L - x * frontBaseRect.W;
				behindRect.L = behindBaseRect.L - x * behindBaseRect.W;

				DrawDungWall(Layout.GetWall(-x, y, 4), new P4Poly(behindRect.LT, frontRect.LT, frontRect.LB, behindRect.LB), y);
			}
		}

		private static void DrawDungWall(MapWall.Kind_e kind, P4Poly poly, double y)
		{
			DDPicture picture;

			switch (kind)
			{
				case MapWall.Kind_e.NONE:
					return;

				case MapWall.Kind_e.WALL:
					picture = Layout.GetWallPicture();
					break;

				case MapWall.Kind_e.GATE:
					picture = Layout.GetGatePicture();
					break;

				default:
					throw null; // never
			}
			double bright = 1.0 - y / 10.0;

			DDDraw.SetBright(bright, bright, bright);
			DDDraw.DrawFree(picture, poly);
			DDDraw.Reset();
		}
	}
}
