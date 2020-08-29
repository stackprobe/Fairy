using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.MakeMaps
{
	public class DungeonMap
	{
		private DungeonMapCell[,] Table;

		public DungeonMapCell DefaultCell = new DungeonMapCell() { Wall = true };

		public int W { get; private set; }
		public int H { get; private set; }

		public I2Point StartPoint = new I2Point(0, 0);
		public int StartDirection = 2;

		public string ParameterString = "Dummy";

		public DungeonMap(int w, int h)
		{
			if (w < 1 || IntTools.IMAX < w) throw null;
			if (h < 1 || IntTools.IMAX < h) throw null;

			this.Table = new DungeonMapCell[w, h];
			this.W = w;
			this.H = h;

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					this.Table[x, y] = new DungeonMapCell();
				}
			}
		}

		public DungeonMapCell this[int x, int y]
		{
			get
			{
				if (
					x < 0 || this.W <= x ||
					y < 0 || this.H <= y
					)
					return this.DefaultCell;

				return this.Table[x, y];
			}
		}

		public void SetFairStartDirection()
		{
			int x = this.StartPoint.X;
			int y = this.StartPoint.Y;

			if (this[x, y + 1].Wall == false)
			{
				this.StartDirection = 2;
			}
			else if (this[x - 1, y].Wall == false)
			{
				this.StartDirection = 4;
			}
			else if (this[x + 1, y].Wall == false)
			{
				this.StartDirection = 6;
			}
			else if (this[x, y - 1].Wall == false)
			{
				this.StartDirection = 8;
			}
		}
	}
}
