using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Games
{
	public class TitleMenu
	{
		private DDSimpleMenu SimpleMenu;

		public void Perform()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			//Ground.I.Music.Title.Play();

			string[] items = new string[]
			{
				"テスト01",
				"テスト02",
				"テスト03",
				"設定",
				"終了",
			};

			int selectIndex = 0;

			this.SimpleMenu = new DDSimpleMenu();

			this.SimpleMenu.WallColor = new I3Color(0, 0, 64);
			//this.SimpleMenu.WallPicture = Ground.I.Picture.TitleWall;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("Donut3", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						this.LeaveTitleMenu();
						new GameTest0001().Perform();
						this.ReturnTitleMenu();
						break;

					case 1:
						this.LeaveTitleMenu();
						new GameTest0002().Perform();
						this.ReturnTitleMenu();
						break;

					case 2:
						this.LeaveTitleMenu();
						new GameTest0003().Perform();
						this.ReturnTitleMenu();
						break;

					case 3:
						this.Setting();
						break;

					case 4:
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

		private void Setting()
		{
			DDCurtain.SetCurtain();
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
							//Ground.I.SE.XXXXX.Play();
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
			DDDraw.SetBright(new I3Color(0, 0, 64));
			DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, 0, 0, DDConsts.Screen_W, DDConsts.Screen_H);
			DDDraw.Reset();
			//DDDraw.DrawRect(Ground.I.Picture.TitleWall, 0, 0, DDConsts.Screen_W, DDConsts.Screen_H);
		}

		private void LeaveTitleMenu()
		{
#if false
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawWall();
				DDEngine.EachFrame();
			}
#endif

			GC.Collect();
		}

		private void ReturnTitleMenu()
		{
			//Ground.I.Music.Title.Play();

			GC.Collect();
		}
	}
}
