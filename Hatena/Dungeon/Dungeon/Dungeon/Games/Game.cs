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
			this.WallPicture = Ground.I.Picture.Wall;
			this.GatePicture = Ground.I.Picture.Gate;
			this.BackgroundPicture = Ground.I.Picture.Background;

		restart:
			this.Player.X = this.Map.DungeonMap.StartPoint.X;
			this.Player.Y = this.Map.DungeonMap.StartPoint.Y;
			this.Player.Direction = this.Map.DungeonMap.StartDirection;

			DDEngine.FreezeInput();
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -0.8);

			for (; ; ) // マップのプレビュー
			{
				if (DDInput.A.GetInput() == 1)
					break;

				if (DDInput.B.GetInput() == 1)
					break;

				if (DDInput.PAUSE.GetInput() == 1)
					break;

				DungeonScreen.DrawFront(this.Layout);
				this.DrawDungeon();

				DDCurtain.EachFrame();

				// マップの表示
				{
					int cell_wh = 400 / Math.Max(this.Map.W, this.Map.H);

					if (cell_wh < 1)
						throw null; // 2bs?

					for (int x = -1; x <= this.Map.W; x++)
					{
						for (int y = -1; y <= this.Map.H; y++)
						{
							int l = (DDConsts.Screen_W - cell_wh * this.Map.W) / 2;
							int t = (DDConsts.Screen_H - cell_wh * this.Map.H) / 2;

							I3Color color = new I3Color(64, 64, 64);

							if (this.Map.DungeonMap[x, y].Wall)
								color = new I3Color(200, 200, 50);

							if (this.Map.DungeonMap[x, y].Goal)
								color = new I3Color(255, 0, 0);

							if (x == this.Map.DungeonMap.StartPoint.X && y == this.Map.DungeonMap.StartPoint.Y)
								color = new I3Color(0, 128, 255);

							DDDraw.SetBright(color);
							DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, l + x * cell_wh, t + y * cell_wh, cell_wh, cell_wh);
							DDDraw.Reset();
						}
					}
				}

				DDPrint.SetPrint();
				DDPrint.Print("※ [青]＝スタート地点 , [赤]＝ゴール地点 , 上方向が北　( パラメータ＝" + this.Map.DungeonMap.ParameterString + " )");

				DDPrint.SetPrint(DDConsts.Screen_W / 2 - 90, DDConsts.Screen_H - 16);
				DDPrint.Print("PRESS Z KEY TO START");

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
			Ground.I.Music.Maze.Play();
			DDCurtain.SetCurtain(30, 0.0);

			this.Frame = 0;

			for (; ; this.Frame++)
			{
				if (DDInput.PAUSE.GetInput() == 1) // ポーズ・メニュー画面
				{
					Ground.I.SE.PauseEnter.Play();

					DDEngine.FreezeInput();
					DDCurtain.SetCurtain(30, -0.8);

					const int ITEM_NUM = 3;
					int selectIndex = 0;

					for (; ; )
					{
						if (DDInput.PAUSE.GetInput() == 1)
							break;

						if (DDInput.DIR_2.IsPound())
							selectIndex++;

						if (DDInput.DIR_8.IsPound())
							selectIndex--;

						selectIndex += ITEM_NUM;
						selectIndex %= ITEM_NUM;

						if (DDInput.A.GetInput() == 1)
						{
							switch (selectIndex)
							{
								case 0:
									Ground.I.SE.Restart.Play();
									goto restart;

								case 1:
									Ground.I.SE.ReturnToTitle.Play();
									goto endGame;

								case 2:
									goto endPauseMenu;

								default:
									throw null; // never
							}
						}
						if (DDInput.B.GetInput() == 1)
						{
							if (selectIndex == ITEM_NUM - 1)
								goto endPauseMenu;

							selectIndex = ITEM_NUM - 1;
						}
						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon();

						DDCurtain.EachFrame();

						DDDraw.SetBright(new I3Color(0, 0, 100));
						DDDraw.SetAlpha(0.3);
						DDDraw.DrawBegin(DDGround.GeneralResource.WhiteBox, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
						DDDraw.DrawSetSize(300, 200);
						DDDraw.DrawEnd();
						DDDraw.Reset();

						DDPrint.SetPrint(380, 215, 50);

						{
							int c = 0;

							DDPrint.PrintLine("[" + (selectIndex == c++ ? ">" : " ") + "] この迷路をやり直す");
							DDPrint.PrintLine("[" + (selectIndex == c++ ? ">" : " ") + "] タイトルに戻る");
							DDPrint.PrintLine("[" + (selectIndex == c++ ? ">" : " ") + "] 迷路に戻る");
						}

						DDEngine.EachFrame();
					}
				endPauseMenu:
					DDCurtain.SetCurtain(30, 0.0);

					Ground.I.SE.PauseLeave.Play();
				}

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
						// 壁衝突

						Ground.I.SE.Poka.Play();
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

				if (this.Map[this.Player.X, this.Player.Y].Script == MapCellScript.GOAL)
				{
					Ground.I.Music.Completed.Play();
					DDCurtain.SetCurtain(60, -0.8);

					foreach (DDScene scene in DDSceneUtils.Create(30))
					{
						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon();

						DDEngine.EachFrame();
					}

					DDEngine.FreezeInput();

					for (; ; )
					{
						if (DDInput.A.GetInput() == 1)
							break;

						if (DDInput.B.GetInput() == 1)
							break;

						if (DDInput.PAUSE.GetInput() == 1)
							break;

						DungeonScreen.DrawFront(this.Layout);
						this.DrawDungeon();

						DDCurtain.EachFrame();

						DDDraw.DrawCenter(Ground.I.Picture.CongratulationsPanel, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);

						DDPrint.SetPrint(DDConsts.Screen_W / 2 - 100, DDConsts.Screen_H - 16);
						DDPrint.Print("PRESS Z KEY TO CONTINUE");

						DDEngine.EachFrame();
					}
					break;
				}

				// Draw ...

				DungeonScreen.DrawFront(this.Layout);
				this.DrawDungeon();

				DDEngine.EachFrame();
			}

		endGame:
			DDCurtain.SetCurtain(30, -1.0);
			DDMusicUtils.Fade();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				DungeonScreen.DrawFront(this.Layout);
				this.DrawDungeon();

				DDEngine.EachFrame();
			}
		}

		private double DistanceFromStart = 0.0;
		private double DistanceFromStart_Delay = 0.0;

		private void DrawDungeon(double xSlideRate = 0.0)
		{
			DDCurtain.DrawCurtain();
			DDDraw.DrawCenter(DungeonScreen.DungScreen.ToPicture(), DDConsts.Screen_W / 2 + xSlideRate * 90.0, DDConsts.Screen_H / 2 - 150);
			DDDraw.DrawSimple(Ground.I.Picture.GameFrame, 0, 0);

			this.DistanceFromStart = DDUtils.GetDistance(
				this.Map.DungeonMap.StartPoint.X - this.Player.X,
				this.Map.DungeonMap.StartPoint.Y - this.Player.Y
				);

			DDUtils.Approach(ref this.DistanceFromStart_Delay, this.DistanceFromStart, 0.8);

			DDPrint.SetPrint(100, 410, 24);
			DDPrint.SetBorder(new I3Color(100, 50, 150));
			DDPrint.PrintLine("方角：" + "南西東北"[this.Player.Direction / 2 - 1]);
			DDPrint.PrintLine("スタート地点からの直線距離：" + this.DistanceFromStart_Delay.ToString("F3"));
			DDPrint.Reset();
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
