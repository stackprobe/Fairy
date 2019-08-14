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
			if (GameConsts.Screen_W % 8 != 0) throw new GameError();
			if (GameConsts.Screen_H % 8 != 0) throw new GameError();

			const int W8 = GameConsts.Screen_W / 8;
			const int H8 = GameConsts.Screen_H / 8;

			string[] items = new string[]
			{
				string.Format("{0} x {1} (デフォルト)", GameConsts.Screen_W, GameConsts.Screen_H),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 *  1, GameConsts.Screen_H + H8 *  1),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 *  2, GameConsts.Screen_H + H8 *  2),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 *  3, GameConsts.Screen_H + H8 *  3),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 *  4, GameConsts.Screen_H + H8 *  4),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 *  5, GameConsts.Screen_H + H8 *  5),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 *  6, GameConsts.Screen_H + H8 *  6),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 *  7, GameConsts.Screen_H + H8 *  7),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 *  8, GameConsts.Screen_H + H8 *  8),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 *  9, GameConsts.Screen_H + H8 *  9),
				string.Format("{0} x {1}", GameConsts.Screen_W + W8 * 10, GameConsts.Screen_H + H8 * 10),
				"フルスクリーン 画面に合わせる",
				"フルスクリーン 縦横比維持",
				"フルスクリーン 黒背景 (推奨)",
				"戻る",
			};

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = Perform("ウィンドウサイズ設定", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
						GameMain.SetScreenSize(GameConsts.Screen_W + W8 * selectIndex, GameConsts.Screen_H + H8 * selectIndex);
						break;

					case 11:
						GameMain.SetScreenSize(GameGround.MonitorRect.W, GameGround.MonitorRect.H);
						break;

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
