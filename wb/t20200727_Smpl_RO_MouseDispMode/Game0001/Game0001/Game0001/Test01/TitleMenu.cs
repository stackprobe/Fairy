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
		private DDSimpleMenu SmplMenu;

		public void Perform()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			//Ground.I.Music.Title.Play();

			string[] items = new string[]
			{
				"ゲームスタート",
				"コンテニュー？",
				"設定",
				"終了",
			};

			int selectIndex = 0;

			// 常にマウス無効にするため、一時的にマウスを非表示にする。Hack
			{
				bool mouseEnabled = DDUtils.GetMouseDispMode();
				DDUtils.SetMouseDispMode(false);

				this.SmplMenu = new DDSimpleMenu();

				DDUtils.SetMouseDispMode(mouseEnabled); // 元に戻す。
			}

			this.SmplMenu.WallColor = new I3Color(0, 0, 64);
			//this.SmplMenu.WallPicture = Ground.I.Picture.TitleWall;

			for (; ; )
			{
				selectIndex = this.SmplMenu.Perform("Donut2", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						{
							this.LeaveTitleMenu();

							{
								GameMain gameMain = new GameMain();

								gameMain.INIT();
								gameMain.Perform();
								gameMain.FNLZ();
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
				"ゲーム画面上でのマウスカーソルの表示／非表示の切り替え",
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
						this.SmplMenu.VolumeConfig("ＢＧＭ音量", DDGround.MusicVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.MusicVolume = volume;
							DDMusicUtils.UpdateVolume();
						},
						() => { }
						);
						break;

					case 3:
						this.SmplMenu.VolumeConfig("ＳＥ音量", DDGround.SEVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.SEVolume = volume;
							DDSEUtils.UpdateVolume();
						},
						() =>
						{
							/*
							if (SecurityTools.CRandom.GetReal2() < 0.5)
								Ground.I.SE.PauseIn.Play();
							else
								Ground.I.SE.PauseOut.Play(); */
						}
						);
						break;

					case 4:
						DDGround.RO_MouseDispMode = DDGround.RO_MouseDispMode == false;
						DDUtils.SetMouseDispMode(DDGround.RO_MouseDispMode);
						break;

					case 5:
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
