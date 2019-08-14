using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Test01
{
	public class TitleMenu
	{
		private GameSimpleMenu SmplMenu;

		public void Perform()
		{
			GameCurtain.SetCurtain();
			GameEngine.FreezeInput();

			Ground.I.Music.Title.Play();

			string[] items = new string[]
			{
				"ゲームスタート",
				"コンテニュー？",
				"設定",
				"終了",
			};

			int selectIndex = 0;

			this.SmplMenu = new GameSimpleMenu();

			//this.SmplMenu.WallColor = new I3Color(0, 0, 64); // test
			this.SmplMenu.WallPicture = Ground.I.Picture.TitleWall;

			for (; ; )
			{
				selectIndex = this.SmplMenu.Perform("Donut2", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						{
							this.LeaveTitleMenu();

							{
								AppGameMain appGameMain = new AppGameMain();

								appGameMain.INIT();
								appGameMain.Perform();
								appGameMain.FNLZ();
							}

							this.ReturnTitleMenu();
						}
						break;

					case 1:
						// TODO
						break;

					case 2:
						this.Setting();
						break;

					case 3:
						goto endMenu;

					default:
						throw new GameError();
				}
			}
		endMenu:
			GameEngine.FreezeInput();
			GameMusicUtils.Fade();
			GameCurtain.SetCurtain(30, -1.0);

			foreach (GameScene scene in GameSceneUtils.Create(40))
			{
				this.DrawWall();
				GameEngine.EachFrame();
			}
		}

		private void Setting()
		{
			GameCurtain.SetCurtain();
			GameEngine.FreezeInput();

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
				selectIndex = this.SmplMenu.Perform("設定", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						this.SmplMenu.PadConfig();
						break;

					case 1:
						this.SmplMenu.WindowSizeConfig();
						break;

					case 2:
						this.SmplMenu.VolumeConfig("ＢＧＭ音量", GameGround.MusicVolume, 0, 100, 1, 10, volume =>
						{
							GameGround.MusicVolume = volume;
							GameMusicUtils.UpdateVolume();
						},
						() => { }
						);
						break;

					case 3:
						this.SmplMenu.VolumeConfig("ＳＥ音量", GameGround.SEVolume, 0, 100, 1, 10, volume =>
						{
							GameGround.SEVolume = volume;
							GameSEUtils.UpdateVolume();
						},
						() =>
						{
							if (SecurityTools.CRandom.GetReal2() < 0.5)
								Ground.I.SE.PauseIn.Play();
							else
								Ground.I.SE.PauseOut.Play();
						}
						);
						break;

					case 4:
						goto endMenu;

					default:
						throw new GameError();
				}
			}
		endMenu:
			GameEngine.FreezeInput();
		}

		private void DrawWall()
		{
			GameDraw.DrawRect(Ground.I.Picture.TitleWall, 0, 0, GameConsts.Screen_W, GameConsts.Screen_H);
		}

		private void LeaveTitleMenu()
		{
			GameMusicUtils.Fade();
			GameCurtain.SetCurtain(30, -1.0);
		}

		private void ReturnTitleMenu()
		{
			Ground.I.Music.Title.Play();
		}
	}
}
