using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.MakeMaps;

namespace Charlotte.Games
{
	public class Map
	{
		public DungeonMap DungeonMap { get; private set; }

		public int W { get { return this.DungeonMap.W; } }
		public int H { get { return this.DungeonMap.H; } }

		private MapCell[,] Cells;

		public Map(DungeonMap dungMap)
		{
			this.DungeonMap = dungMap;

			this.Cells = new MapCell[this.W, this.H];

			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					this.Cells[x, y] = new MapCell();
					this.Cells[x, y].Wall_2.Kind = this.GetWallKind(this.DungeonMap[x, y + 1]);
					this.Cells[x, y].Wall_4.Kind = this.GetWallKind(this.DungeonMap[x - 1, y]);
					this.Cells[x, y].Wall_6.Kind = this.GetWallKind(this.DungeonMap[x + 1, y]);
					this.Cells[x, y].Wall_8.Kind = this.GetWallKind(this.DungeonMap[x, y - 1]);
				}
			}
			this.DefaultCell_2_Wall.Wall_2.Kind = MapWall.Kind_e.WALL;
			this.DefaultCell_4_Wall.Wall_4.Kind = MapWall.Kind_e.WALL;
			this.DefaultCell_6_Wall.Wall_6.Kind = MapWall.Kind_e.WALL;
			this.DefaultCell_8_Wall.Wall_8.Kind = MapWall.Kind_e.WALL;
		}

		private MapWall.Kind_e GetWallKind(DungeonMapCell dmCell)
		{
			if (dmCell.Wall)
				return MapWall.Kind_e.WALL;

			if (dmCell.Goal)
				return MapWall.Kind_e.GATE;

			return MapWall.Kind_e.NONE;
		}

		private MapCell DefaultCell = new MapCell();
		private MapCell DefaultCell_2_Wall = new MapCell();
		private MapCell DefaultCell_4_Wall = new MapCell();
		private MapCell DefaultCell_6_Wall = new MapCell();
		private MapCell DefaultCell_8_Wall = new MapCell();

		public MapCell this[int x, int y]
		{
			get
			{
				if (
					x < 0 || this.W <= x ||
					y < 0 || this.H <= y
					)
				{
					if (0 <= y && y < this.H)
					{
						if (x == -1)
							return this.DefaultCell_6_Wall;

						if (x == this.W)
							return this.DefaultCell_4_Wall;
					}
					if (0 <= x && x < this.W)
					{
						if (y == -1)
							return this.DefaultCell_2_Wall;

						if (y == this.H)
							return this.DefaultCell_8_Wall;
					}
					return this.DefaultCell;
				}
				return this.Cells[x, y];
			}
		}
	}
}
