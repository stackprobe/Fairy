using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.Games;
using Charlotte.MakeMaps;

namespace Charlotte.Mains
{
	public class TitleMenu
	{
		private static readonly I3Color WALL_COLOR = new I3Color(100, 100, 111);

		private DDSimpleMenu SimpleMenu;

		public void Perform()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			//Ground.I.Music.Title.Play();

			string[] items = new string[]
			{
				"ゲームスタート",
				"迷路の設定",
				"設定",
				"終了",
			};

			int selectIndex = 0;

			this.SimpleMenu = new DDSimpleMenu();

			this.SimpleMenu.WallColor = WALL_COLOR;
			//this.SimpleMenu.WallPicture = Ground.I.Picture.TitleWall;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("Dungeon", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						{
							DungeonMap dungMap = new DungeonMapMaker().MakeDungeonMap(
								Ground.I.MakeMap_W,
								Ground.I.MakeMap_H,
								Ground.I.MakeMap_Seed
								);

							using (Game game = new Game())
							{
								game.Map = MapLoader.Load(dungMap);
								game.Perform();
							}
						}
						break;

					case 1:
						this.MakeMapSetting();
						break;

					case 2:
						this.Setting();
						break;

					case 3:
						goto endMenu;

					default:
						throw new DDError();
				}
			}
		endMenu:
			DDEngine.FreezeInput();
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawWall();
				DDEngine.EachFrame();
			}
		}

		private void MakeMapSetting()
		{
			//DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			int selectIndex = 0;

			for (; ; )
			{
				string[] items = new string[] 
				{
					"マップの幅　　　[ " + Ground.I.MakeMap_W + " ]",
					"マップの高さ　　[ " + Ground.I.MakeMap_H + " ]",
					"乱数のシード値　[ " + Ground.I.MakeMap_Seed + " ]",
					"戻る",
				};

				selectIndex = this.SimpleMenu.Perform("設定", items, selectIndex);

				switch (selectIndex)
				{
					case 0:

						// TODO IntVolumeConfig

						this.SimpleMenu.VolumeConfig("マップの幅", Ground.I.MakeMap_W, 10, 200, 1, 10,
							volume => Ground.I.MakeMap_W = DoubleTools.ToInt(volume),
							() => { }
							);
						break;

					case 1:
						this.SimpleMenu.VolumeConfig("マップの高さ", Ground.I.MakeMap_H, 10, 200, 1, 10,
							volume => Ground.I.MakeMap_H = DoubleTools.ToInt(volume),
							() => { }
							);
						break;

					case 2:
						this.SimpleMenu.VolumeConfig("乱数のシード値", Ground.I.MakeMap_Seed, 1, 1000000, 1, 1000,
							volume => Ground.I.MakeMap_Seed = DoubleTools.ToInt(volume),
							() => { }
							);
						break;

					case 3:
						goto endMenu;

					default:
						throw new DDError();
				}
			}
		endMenu:
			DDEngine.FreezeInput();
		}

		private void Setting()
		{
			//DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			string[] items = new string[] 
			{
				"パッドのボタン設定",
				"ウィンドウサイズ変更",
				"ＢＧＭ音量",
				"ＳＥ音量",
				"戻る",
			};

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("設定", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						this.SimpleMenu.PadConfig();
						break;

					case 1:
						this.SimpleMenu.WindowSizeConfig();
						break;

					case 2:
						this.SimpleMenu.VolumeConfig("ＢＧＭ音量", DDGround.MusicVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.MusicVolume = volume;
							DDMusicUtils.UpdateVolume();
						},
						() => { }
						);
						break;

					case 3:
						this.SimpleMenu.VolumeConfig("ＳＥ音量", DDGround.SEVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.SEVolume = volume;
							DDSEUtils.UpdateVolume();
						},
						() =>
						{
							//Ground.I.SE.PauseIn.Play();
						}
						);
						break;

					case 4:
						goto endMenu;

					default:
						throw new DDError();
				}
			}
		endMenu:
			DDEngine.FreezeInput();
		}

		private void DrawWall()
		{
			DDDraw.SetBright(WALL_COLOR);
			DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, 0, 0, DDConsts.Screen_W, DDConsts.Screen_H);
			DDDraw.Reset();
		}

		private void LeaveTitleMenu()
		{
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawWall();

				DDEngine.EachFrame();
			}
			GC.Collect();
		}

		private void ReturnTitleMenu()
		{
			//Ground.I.Music.Title.Play();

			GC.Collect();
		}
	}
}
