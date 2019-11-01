using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Charlotte.Tools;

namespace Charlotte.MakeMaps
{
	public class DungeonMapMaker
	{
		private Random Rand;

		/// <summary>
		/// ダンジョンマップを作る。
		/// </summary>
		/// <param name="w">マップの幅</param>
		/// <param name="h">マップの高さ</param>
		public DungeonMap MakeDungeonMap(int w, int h, int seed)
		{
			if (seed == 0)
				seed = (int)(DateTimeToSec.Now.GetSec() % IntTools.IMAX);

			Rand = new Random(seed);

			Map_W = w;
			Map_H = h;
			Map = MakeLikeADungeonMap(24); // pattern == 24

			//OutputToImageFile(); // 初期状態

			for (; ; ) // 全ての通路を繋げる。
			{
				Point[] cps = AllClosedAreaPoints().ToArray();

				if (cps.Length == 1) // ? 全ての通路が繋がった。
					break;

				ClosedArea[] cas = AllClosedAreas(cps).ToArray();
				Array.Sort(cas, (a, b) => a.Size - b.Size); // 狭い順
				ClosedArea ca = cas[0]; // 最も狭い空間を選択
				Point[] breakingWalls = GetBreakingWalls(ca).ToArray(); // 選択された空間の破壊可能な(厚さ１枚の)壁を列挙

				if (breakingWalls.Length == 0) // ? 破壊可能な壁が無い。⇒ この空間は壁で埋める。
				{
					Fill(ca.Pt.X, ca.Pt.Y, 1);

					//OutputToImageFile(); // 途中経過

					continue;
				}

				Point breakingWall = breakingWalls[Rand.Next(breakingWalls.Length)]; // ランダムに選択
				Map[breakingWall.X, breakingWall.Y] = 0; // 壁を破壊

				//OutputToImageFile(); // 途中経過

				//Fill(breakingWall.X, breakingWall.Y, 2); // 途中経過用に塗りつぶし

				//OutputToImageFile(); // 途中経過

				//Fill(breakingWall.X, breakingWall.Y, 0); // 復元

				//OutputToImageFile(); // 途中経過
			}

			// スタート地点とゴール地点を設置
			{
				Point[] pts = AllPoints().Where(p => Map[p.X, p.Y] == 0).ToArray();

				Point startPt = default(Point);
				Point goalPt = pts[Rand.Next(pts.Length)]; // 適当な通路から開始

				for (int c = 0; c < 10; c++) // スタート地点から最も遠い場所、そこから最も遠い場所、そこから最も遠い場所 ... と何回か繰り返す。
				{
					startPt = goalPt;
					pts = Farthest(startPt);
					goalPt = pts[Rand.Next(pts.Length)];
				}

				Map[startPt.X, startPt.Y] = 2; // スタート地点を設置
				Map[goalPt.X, goalPt.Y] = 2; // ゴール地点を設置
			}

			//OutputToImageFile(); // 結果

			return ToDungeonMap();
		}

		private DungeonMap ToDungeonMap()
		{
			DungeonMap dungMap = new DungeonMap(Map_W, Map_H);

			for (int x = 0; x < Map_W; x++)
			{
				for (int y = 0; y < Map_H; y++)
				{
					dungMap[x, y].Wall = Map[x, y] == 1;
				}
			}

			{
				Point[] pts = AllPoints().Where(p => Map[p.X, p.Y] == 2).ToArray();

				if (pts.Length != 2)
					throw null; // never

				Point startPt = pts[0];
				Point goalPt = pts[1];

				if (Rand.Next(2) == 0)
				{
					Point tmp = startPt;
					startPt = goalPt;
					goalPt = tmp;
				}
				dungMap.StartPoint = new I2Point(startPt.X, startPt.Y);
				dungMap.SetFairStartDirection();
				dungMap[goalPt.X, goalPt.Y].Goal = true;
			}

			return dungMap;
		}

		private int Map_W; // マップの幅
		private int Map_H; // マップの高さ

		// Map[MAP_W, MAP_H]
		//
		// 0 ... 通路
		// 1 ... 壁
		// 2 ... スタート地点など
		//
		private int[,] Map;

		private class ClosedArea
		{
			public Point Pt;
			public int Size;
		}

		private IEnumerable<Point> GetBreakingWalls(ClosedArea ca)
		{
			Fill(ca.Pt.X, ca.Pt.Y, 2);

			//OutputToImageFile(); // 途中経過

			for (int x = 1; x < Map_W - 1; x++)
			{
				for (int y = 1; y < Map_H - 1; y++)
				{
					if (
						Map[x - 1, y] == 0 ||
						Map[x + 1, y] == 0 ||
						Map[x, y + 1] == 0 ||
						Map[x, y - 1] == 0
						)
						if (
							Map[x - 1, y] == 2 ||
							Map[x + 1, y] == 2 ||
							Map[x, y + 1] == 2 ||
							Map[x, y - 1] == 2
							)
							yield return new Point(x, y);
				}
			}
			Fill(ca.Pt.X, ca.Pt.Y, 0); // 復元
		}

		private IEnumerable<ClosedArea> AllClosedAreas(Point[] cps)
		{
			foreach (Point pt in cps)
			{
				if (Map[pt.X, pt.Y] == 0) // ? 通路
				{
					Fill(pt.X, pt.Y, 2);
					int size = AllPoints().Where(p => Map[p.X, p.Y] == 2).Count();
					Fill(pt.X, pt.Y, 0); // 復元
					yield return new ClosedArea() { Pt = pt, Size = size, };
				}
			}
			AllPoints().Where(p => Map[p.X, p.Y] == 2).ToList().ForEach(p => Map[p.X, p.Y] = 0); // 復元
		}

		private IEnumerable<Point> AllClosedAreaPoints()
		{
			foreach (Point pt in AllPoints())
			{
				if (Map[pt.X, pt.Y] == 0) // ? 通路
				{
					Fill(pt.X, pt.Y, 2);
					yield return pt;
				}
			}
			AllPoints().Where(p => Map[p.X, p.Y] == 2).ToList().ForEach(p => Map[p.X, p.Y] = 0); // 復元
		}

		private IEnumerable<Point> AllPoints()
		{
			for (int x = 0; x < Map_W; x++)
			{
				for (int y = 0; y < Map_H; y++)
				{
					yield return new Point(x, y);
				}
			}
		}

		private void Fill(int x, int y, int fillValue)
		{
			int targValue = Map[x, y];

			if (targValue == fillValue)
				throw null; // never

			Queue<Point> seekers = new Queue<Point>();

			seekers.Enqueue(new Point(x, y));

			while (1 <= seekers.Count)
			{
				Point pt = seekers.Dequeue();

				if (
					pt.X < 0 || Map_W <= pt.X ||
					pt.Y < 0 || Map_H <= pt.Y
					)
					continue;

				if (Map[pt.X, pt.Y] != targValue)
					continue;

				Map[pt.X, pt.Y] = fillValue;

				seekers.Enqueue(new Point(pt.X + 1, pt.Y));
				seekers.Enqueue(new Point(pt.X - 1, pt.Y));
				seekers.Enqueue(new Point(pt.X, pt.Y + 1));
				seekers.Enqueue(new Point(pt.X, pt.Y - 1));
			}
		}

		private Point[] Farthest(Point startPt)
		{
#if false
			// 途中経過として
			{
				Map[startPt.X, startPt.Y] = 2;
				OutputToImageFile();
				Map[startPt.X, startPt.Y] = 0; // 復元
			}
#endif

			List<Point> farthestPts = null;
			List<Point> pts = new List<Point>();

			pts.Add(startPt);

			for (; ; )
			{
				List<Point> acceptPts = new List<Point>();
				List<Point> nextPts = new List<Point>();

				foreach (Point pt in pts)
				{
					if (
						pt.X < 0 || Map_W <= pt.X ||
						pt.Y < 0 || Map_H <= pt.Y
						)
						continue;

					if (Map[pt.X, pt.Y] != 0)
						continue;

					Map[pt.X, pt.Y] = 2;

					acceptPts.Add(pt);

					nextPts.Add(new Point(pt.X + 1, pt.Y));
					nextPts.Add(new Point(pt.X - 1, pt.Y));
					nextPts.Add(new Point(pt.X, pt.Y + 1));
					nextPts.Add(new Point(pt.X, pt.Y - 1));
				}
				if (nextPts.Count == 0)
					break;

				farthestPts = acceptPts;
				pts = nextPts;
			}
			AllPoints().Where(p => Map[p.X, p.Y] == 2).ToList().ForEach(p => Map[p.X, p.Y] = 0); // 復元

#if false
			// 途中経過として
			{
				farthestPts.ForEach(p => Map[p.X, p.Y] = 2);
				OutputToImageFile();
				farthestPts.ForEach(p => Map[p.X, p.Y] = 0); // 復元
			}
#endif

			return farthestPts.ToArray();
		}

		private int ImageIndex = 0;

		private void OutputToImageFile()
		{
			using (Bitmap bmp = new Bitmap(Map_W, Map_H))
			{
				for (int x = 0; x < Map_W; x++)
				{
					for (int y = 0; y < Map_H; y++)
					{
						Color color;

						switch (Map[x, y])
						{
							case 0: color = Color.Gray; break;
							case 1: color = Color.Yellow; break;
							case 2: color = Color.Red; break;

							default:
								throw null; // never
						}
						bmp.SetPixel(x, y, color);
					}
				}
				bmp.Save(string.Format(@"C:\temp\{0}.bmp", (ImageIndex++).ToString("D4")), ImageFormat.Bmp); // 適当な場所にビットマップで出力する。
			}
		}

		private int[,] MakeLikeADungeonMap(int pattern) // pattern: 0 ～ 1023
		{
			int[,] map = new int[Map_W, Map_H];

			for (int x = 0; x < Map_W; x++)
			{
				for (int y = 0; y < Map_H; y++)
				{
					map[x, y] = Rand.Next(2);
				}
			}
			for (int c = 0; c < Map_W * Map_H * 30; c++)
			{
				int x = Rand.Next(Map_W);
				int y = Rand.Next(Map_H);
				int count = 0;

				for (int xc = -1; xc <= 1; xc++)
				{
					for (int yc = -1; yc <= 1; yc++)
					{
						count += map[(x + Map_W + xc) % Map_W, (y + Map_H + yc) % Map_H];
					}
				}
				map[x, y] = (pattern >> count) & 1;
			}
			return map;
		}
	}
}
