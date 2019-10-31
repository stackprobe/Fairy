using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public Map Map;

		// <---- prm

		public static Game I = null;

		private DDPicture WallPicture;
		private DDPicture GatePicture;
		private DDPicture BackgroundPicture;

		private Player Player = new Player();

		private int Frame;

		public Game()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Perform()
		{
			this.Player.X = this.Map.DungeonMap.StartPoint.X;
			this.Player.Y = this.Map.DungeonMap.StartPoint.Y;
			this.Player.Direction = this.Map.DungeonMap.StartDirection;

			this.WallPicture = Ground.I.Picture.Wall;
			this.GatePicture = Ground.I.Picture.Gate;
			this.BackgroundPicture = Ground.I.Picture.Background;

			this.Frame = 0;

			for (; ; this.Frame++)
			{
				if (1 <= DDInput.DIR_8.GetInput())
				{
					foreach (DDScene scene in DDSceneUtils.Create(5))
					{
						DungeonScreen.DrawFront(this.Layout, true);
						this.DrawDungeon();

						DDEngine.EachFrame();
					}
					if (this.Map[this.Player.X, this.Player.Y].GetWall(this.Player.Direction).Kind == MapWall.Kind_e.WALL)
					{
						// 壁衝突 todo ???
					}
					else
					{
						switch (this.Player.Direction)
						{
							case 4: this.Player.X--; break;
							case 6: this.Player.X++; break;
							case 8: this.Player.Y--; break;
							case 2: this.Player.Y++; break;

							default:
								throw null; // never
						}
					}
					foreach (DDScene scene in DDSceneUtils.Create(5))
					{
						DungeonScreen.DrawFront(this.Layout, false);
						this.DrawDungeon();

						DDEngine.EachFrame();
					}
				}
				if (1 <= DDInput.DIR_4.GetInput())
				{
					foreach (DDScene scene in DDSceneUtils.Create(5))
					{
						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon(scene.Rate);

						DDEngine.EachFrame();
					}
					this.Player.Direction = Utilities.RotL(this.Player.Direction);

					foreach (DDScene scene in DDSceneUtils.Create(5))
					{
						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon(-scene.RemainingRate);

						DDEngine.EachFrame();
					}
				}
				if (1 <= DDInput.DIR_6.GetInput())
				{
					foreach (DDScene scene in DDSceneUtils.Create(5))
					{
						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon(-scene.Rate);

						DDEngine.EachFrame();
					}
					this.Player.Direction = Utilities.RotR(this.Player.Direction);

					foreach (DDScene scene in DDSceneUtils.Create(5))
					{
						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon(scene.RemainingRate);

						DDEngine.EachFrame();
					}
				}
				if (1 <= DDInput.DIR_2.GetInput())
				{
					foreach (DDScene scene in DDSceneUtils.Create(5))
					{
						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon(-scene.Rate);

						DDEngine.EachFrame();
					}
					this.Player.Direction = Utilities.RotR(this.Player.Direction);

					foreach (DDScene scene in DDSceneUtils.Create(10))
					{
						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon(scene.RemainingRate * 2.0 - 1.0);

						DDEngine.EachFrame();
					}
					this.Player.Direction = Utilities.RotR(this.Player.Direction);

					foreach (DDScene scene in DDSceneUtils.Create(5))
					{
						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon(scene.RemainingRate);

						DDEngine.EachFrame();
					}
				}

				// Draw ...

				DungeonScreen.DrawFront(this.Layout);
				this.DrawDungeon();

				DDEngine.EachFrame();
			}
		}

		private void DrawDungeon(double xSlideRate = 0.0)
		{
			DDCurtain.DrawCurtain();
			DDDraw.DrawCenter(DungeonScreen.DungScreen.ToPicture(), DDConsts.Screen_W / 2 + xSlideRate * 90.0, DDConsts.Screen_H / 2 - 150);
			DDDraw.DrawSimple(Ground.I.Picture.GameFrame, 0, 0);
		}

		private IDungeonLayout Layout = new LayoutInfo();

		private class LayoutInfo : IDungeonLayout
		{
			public DDPicture GetWallPicture()
			{
				return Game.I.WallPicture;
			}

			public DDPicture GetGatePicture()
			{
				return Game.I.GatePicture;
			}

			public DDPicture GetBackgroundPicture()
			{
				return Game.I.BackgroundPicture;
			}

			public MapWall.Kind_e GetWall(int x, int y, int direction)
			{
				switch (Game.I.Player.Direction)
				{
					case 4: return Game.I.Map[Game.I.Player.X - y, Game.I.Player.Y - x].GetWall(Utilities.RotL(direction)).Kind;
					case 6: return Game.I.Map[Game.I.Player.X + y, Game.I.Player.Y + x].GetWall(Utilities.RotR(direction)).Kind;
					case 8: return Game.I.Map[Game.I.Player.X + x, Game.I.Player.Y - y].GetWall(direction).Kind;
					case 2: return Game.I.Map[Game.I.Player.X - x, Game.I.Player.Y + y].GetWall(10 - direction).Kind;

					default:
						throw null; // never
				}
			}
		}
	}
}
