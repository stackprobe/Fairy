using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	public class GameSimpleMenu
	{
		public I3Color Color = null;
		public I3Color BorderColor = null;
		public I3Color WallColor = null;
		public GamePicture WallPicture = null;
		public double WallCurtain = 0.0; // -1.0 ～ 1.0
		public int X = 16;
		public int Y = 16;
		public int YStep = 32;

		// <---- prm

		public int Perform(string title, string[] items, int selectIndex)
		{
			GameCurtain.SetCurtain();
			GameEngine.FreezeInput();

			for (; ; )
			{
				if (GameInput.A.IsPound())
				{
					break;
				}
				if (GameInput.B.IsPound())
				{
					if (selectIndex == items.Length - 1)
						break;

					selectIndex = items.Length - 1;
				}
				if (GameInput.DIR_8.IsPound())
				{
					selectIndex--;
				}
				if (GameInput.DIR_2.IsPound())
				{
					selectIndex++;
				}

				selectIndex += items.Length;
				selectIndex %= items.Length;

				GameCurtain.DrawCurtain();

				if (this.WallColor != null)
					DX.DrawBox(0, 0, GameConsts.Screen_W, GameConsts.Screen_H, GameDxUtils.GetColor(this.WallColor), 1);

				if (this.WallPicture != null)
				{
					GameDraw.DrawRect(this.WallPicture, 0, 0, GameConsts.Screen_W, GameConsts.Screen_H);
					//GameDraw.DrawCenter(this.WallPicture, GameConsts.Screen_W / 2.0, GameConsts.Screen_H / 2.0); // old
					GameCurtain.DrawCurtain(this.WallCurtain);
				}
				if (this.Color != null)
					GamePrint.SetColor(this.Color);

				if (this.BorderColor != null)
					GamePrint.SetBorder(this.BorderColor);

				GamePrint.SetPrint(this.X, this.Y, this.YStep);
				//GamePrint.SetPrint(16, 16, 32); // old
				GamePrint.Print(title);
				GamePrint.PrintRet();

				for (int c = 0; c < items.Length; c++)
				{
					GamePrint.Print(string.Format("[{0}] {1}", selectIndex == c ? ">" : " ", items[c]));
					GamePrint.PrintRet();
				}
				GamePrint.Reset();

				GameEngine.EachFrame();
			}
			GameEngine.FreezeInput();

			return selectIndex;
		}

		private class ButtonInfo
		{
			public GameInput.Button Button;
			public string Name;

			public ButtonInfo(GameInput.Button button, string name)
			{
				this.Button = button;
				this.Name = name;
			}
		}

		public void PadConfig()
		{
			ButtonInfo[] btnInfos = new ButtonInfo[]
			{
				// app > @ btnInfos

				new ButtonInfo(GameInput.DIR_2, "下"),
				new ButtonInfo(GameInput.DIR_4, "左"),
				new ButtonInfo(GameInput.DIR_6, "右"),
				new ButtonInfo(GameInput.DIR_8, "上"),
				new ButtonInfo(GameInput.A, "ショットボタン"),
				new ButtonInfo(GameInput.B, "低速ボタン"),
				new ButtonInfo(GameInput.C, "ボムボタン"),
				//new ButtonInfo(GameInput.D, ""),
				//new ButtonInfo(GameInput.E, ""),
				//new ButtonInfo(GameInput.F, ""),
				//new ButtonInfo(GameInput.L, ""),
				//new ButtonInfo(GameInput.R, ""),
				new ButtonInfo(GameInput.PAUSE, "ポーズボタン"),
				//new ButtonInfo(GameInput.START, ""),

				// < app
			};

			foreach (ButtonInfo btnInfo in btnInfos)
				btnInfo.Button.Backup();

			try
			{
				foreach (ButtonInfo btnInfo in btnInfos)
					btnInfo.Button.BtnId = -1;

				GameCurtain.SetCurtain();
				GameEngine.FreezeInput();

				int currBtnIndex = 0;

				while (currBtnIndex < btnInfos.Length)
				{
					if (GameKey.GetInput(DX.KEY_INPUT_SPACE) == 1)
					{
						return;
					}
					if (GameKey.GetInput(DX.KEY_INPUT_Z) == 1)
					{
						currBtnIndex++;
						goto endInput;
					}

					{
						int pressBtnId = -1;

						for (int padId = 0; padId < GamePad.GetPadCount(); padId++)
							for (int btnId = 0; btnId < GamePad.PAD_BUTTON_MAX; btnId++)
								if (GamePad.GetInput(padId, btnId) == 1)
									pressBtnId = btnId;

						for (int c = 0; c < currBtnIndex; c++)
							if (btnInfos[c].Button.BtnId == pressBtnId)
								pressBtnId = -1;

						if (pressBtnId != -1)
						{
							btnInfos[currBtnIndex].Button.BtnId = pressBtnId;
							currBtnIndex++;
						}
					}
				endInput:

					GameCurtain.DrawCurtain();

					if (this.WallPicture != null)
					{
						GameDraw.DrawRect(this.WallPicture, 0, 0, GameConsts.Screen_W, GameConsts.Screen_H);
						//GameDraw.DrawCenter(this.WallPicture, GameConsts.Screen_W / 2.0, GameConsts.Screen_H / 2.0); // old
						GameCurtain.DrawCurtain(this.WallCurtain);
					}
					if (this.Color != null)
						GamePrint.SetColor(this.Color);

					if (this.BorderColor != null)
						GamePrint.SetBorder(this.BorderColor);

					GamePrint.SetPrint(this.X, this.Y, this.YStep);
					//GamePrint.SetPrint(16, 16, 32); // old
					GamePrint.Print("ゲームパッドのボタン設定");
					GamePrint.PrintRet();

					for (int c = 0; c < btnInfos.Length; c++)
					{
						GamePrint.Print(string.Format("[{0}] {1}", currBtnIndex == c ? ">" : " ", btnInfos[c].Name));

						if (c < currBtnIndex)
						{
							int btnId = btnInfos[c].Button.BtnId;

							GamePrint.Print("　->　");

							if (btnId == -1)
								GamePrint.Print("割り当てナシ");
							else
								GamePrint.Print("" + btnId);
						}
						GamePrint.PrintRet();
					}
					GamePrint.Print("★　カーソルの機能に割り当てるボタンを押して下さい。");
					GamePrint.PrintRet();
					GamePrint.Print("★　スペースを押すとキャンセルします。");
					GamePrint.PrintRet();
					GamePrint.Print("★　[Z]を押すとボタンの割り当てをスキップします。");
					GamePrint.PrintRet();

					GameEngine.EachFrame();
				}
				btnInfos = null;
			}
			finally
			{
				if (btnInfos != null)
					foreach (ButtonInfo info in btnInfos)
						info.Button.Restore();

				GameEngine.FreezeInput();
			}
		}

		public void WindowSizeConfig()
		{
			string[] items = new string[]
			{
				// app > @ WindowSize_items_0_10

				"800 x 600 (デフォルト)",
				"900 x 675",
				"1000 x 750",
				"1100 x 825",
				"1200 x 900",
				"1300 x 975",
				"1400 x 1050",
				"1500 x 1125",
				"1600 x 1200",
				"1700 x 1275",
				"1800 x 1350",

				// < app

				"フルスクリーン",
				"フルスクリーン (縦横比維持)",
				"フルスクリーン (黒背景)",
				"戻る",
			};

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = Perform("ウィンドウサイズ設定", items, selectIndex);

				switch (selectIndex)
				{
					// app > @ WindowSize_switch_case_0_10

					case 0: GameMain.SetScreenSize(800, 600); break;
					case 1: GameMain.SetScreenSize(900, 675); break;
					case 2: GameMain.SetScreenSize(1000, 750); break;
					case 3: GameMain.SetScreenSize(1100, 825); break;
					case 4: GameMain.SetScreenSize(1200, 900); break;
					case 5: GameMain.SetScreenSize(1300, 975); break;
					case 6: GameMain.SetScreenSize(1400, 1050); break;
					case 7: GameMain.SetScreenSize(1500, 1125); break;
					case 8: GameMain.SetScreenSize(1600, 1200); break;
					case 9: GameMain.SetScreenSize(1700, 1275); break;
					case 10: GameMain.SetScreenSize(1800, 1350); break;

					// < app

					case 11: GameMain.SetScreenSize(GameGround.MonitorRect.W, GameGround.MonitorRect.H); break;
					case 12:
						{
							int w = GameGround.MonitorRect.W;
							int h = (GameConsts.Screen_H * GameGround.MonitorRect.W) / GameConsts.Screen_W;

							if (GameGround.MonitorRect.H < h)
							{
								h = GameGround.MonitorRect.H;
								w = (GameConsts.Screen_W * GameGround.MonitorRect.H) / GameConsts.Screen_H;

								if (GameGround.MonitorRect.W < w)
									throw new GameError();
							}
							GameMain.SetScreenSize(w, h);
						}
						break;

					case 13:
						{
							int w = GameGround.MonitorRect.W;
							int h = (GameConsts.Screen_H * GameGround.MonitorRect.W) / GameConsts.Screen_W;

							if (GameGround.MonitorRect.H < h)
							{
								h = GameGround.MonitorRect.H;
								w = (GameConsts.Screen_W * GameGround.MonitorRect.H) / GameConsts.Screen_H;

								if (GameGround.MonitorRect.W < w)
									throw new GameError();
							}
							GameMain.SetScreenSize(GameGround.MonitorRect.W, GameGround.MonitorRect.H);

							GameGround.RealScreenDraw_L = (GameGround.MonitorRect.W - w) / 2;
							GameGround.RealScreenDraw_T = (GameGround.MonitorRect.H - h) / 2;
							GameGround.RealScreenDraw_W = w;
							GameGround.RealScreenDraw_H = h;
						}
						break;

					case 14:
						goto endLoop;

					default:
						throw new GameError();
				}
			}
		endLoop:
			;
		}

		private static double VolumeValueToRate(int value, int minval, int valRange)
		{
			return (double)(value - minval) / valRange;
		}

		public double VolumeConfig(string title, double rate, int minval, int maxval, int valStep, int valFastStep, Action<double> valChanged, Action pulse)
		{
			const int PULSE_FRM = 60;

			int valRange = maxval - minval;
			int value = minval + DoubleTools.ToInt(rate * valRange);
			int origval = value;

			GameCurtain.SetCurtain();
			GameEngine.FreezeInput();

			for (; ; )
			{
				bool chgval = false;

				if (GameInput.A.IsPound())
				{
					break;
				}
				if (GameInput.B.IsPound())
				{
					if (value == origval)
						break;

					value = origval;
					chgval = true;
				}
				if (GameInput.DIR_8.IsPound())
				{
					value += valFastStep;
					chgval = true;
				}
				if (GameInput.DIR_6.IsPound())
				{
					value += valStep;
					chgval = true;
				}
				if (GameInput.DIR_4.IsPound())
				{
					value -= valStep;
					chgval = true;
				}
				if (GameInput.DIR_2.IsPound())
				{
					value -= valFastStep;
					chgval = true;
				}
				if (chgval)
				{
					value = IntTools.Range(value, minval, maxval);
					valChanged(VolumeValueToRate(value, minval, valRange));
				}
				if (GameEngine.ProcFrame % PULSE_FRM == 0)
				{
					pulse();
				}

				GameCurtain.DrawCurtain();

				if (this.WallPicture != null)
				{
					GameDraw.DrawRect(this.WallPicture, 0, 0, GameConsts.Screen_W, GameConsts.Screen_H);
					GameCurtain.DrawCurtain(this.WallCurtain);
				}
				if (this.Color != null)
					GamePrint.SetColor(this.Color);

				if (this.BorderColor != null)
					GamePrint.SetBorder(this.BorderColor);

				GamePrint.SetPrint(this.X, this.Y, this.YStep);
				GamePrint.Print(title);
				GamePrint.PrintRet();

				GamePrint.Print(string.Format("[{0}]　最小={1}　最大={2}", value, minval, maxval));
				GamePrint.PrintRet();

				GamePrint.Print("★　左＝下げる");
				GamePrint.PrintRet();
				GamePrint.Print("★　右＝上げる");
				GamePrint.PrintRet();
				GamePrint.Print("★　下＝速く下げる");
				GamePrint.PrintRet();
				GamePrint.Print("★　上＝速く上げる");
				GamePrint.PrintRet();
				GamePrint.Print("★　調整が終わったら決定ボタンを押して下さい。");
				GamePrint.PrintRet();

				GameEngine.EachFrame();
			}
			GameEngine.FreezeInput();

			return VolumeValueToRate(value, minval, valRange);
		}
	}
}
