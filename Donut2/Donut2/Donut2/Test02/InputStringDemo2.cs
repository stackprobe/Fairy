using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Test02
{
	public class InputStringDemo2
	{
		public void Perform()
		{
			DDUtils.SetMouseDispMode(true);

			string line1 = "";
			string line2 = "";
			string line3 = "";

			for (; ; )
			{
				DDMouse.UpdatePos();

				if (DDMouse.L.GetInput() == 1)
				{
					if (DDUtils.IsOut(new D2Point(DDMouse.X, DDMouse.Y), new D4Rect(50, 50, 50, 16)) == false)
					{
						line1 = EditString("LINE-1 (70 bytes)", line1, 70, '-');
					}
					else if (DDUtils.IsOut(new D2Point(DDMouse.X, DDMouse.Y), new D4Rect(50, 150, 50, 16)) == false)
					{
						line2 = EditString("LINE-2 (ASCII only, 50 bytes)", line2, 50, 'A');
					}
					else if (DDUtils.IsOut(new D2Point(DDMouse.X, DDMouse.Y), new D4Rect(50, 250, 50, 16)) == false)
					{
						line3 = EditString("LINE-3 (DIGIT only, 10 bytes)", line3, 10, '9');
					}
				}

				DDCurtain.DrawCurtain();

				DX.DrawBox(0, 0, DDConsts.Screen_W, 100, DX.GetColor(60, 60, 60), 1);
				DX.DrawBox(0, 100, DDConsts.Screen_W, 200, DX.GetColor(60, 90, 30), 1);
				DX.DrawBox(0, 200, DDConsts.Screen_W, 300, DX.GetColor(60, 30, 90), 1);

				DDPrint.SetPrint(50, 50);
				DDPrint.Print("[EDIT]");

				DDPrint.SetPrint(150, 50);
				DDPrint.Print(line1);

				DDPrint.SetPrint(50, 150);
				DDPrint.Print("[EDIT]");

				DDPrint.SetPrint(150, 150);
				DDPrint.Print(line2);

				DDPrint.SetPrint(50, 250);
				DDPrint.Print("[EDIT]");

				DDPrint.SetPrint(150, 250);
				DDPrint.Print(line3);

				DDEngine.EachFrame();
			}
		}

		private string EditString(string prompt, string initValue, int maxlen, char mode)
		{
			StringBuilder buff = new StringBuilder(maxlen * 3); // FIXME 必要なバッファ長が不明

			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			int inputHdl = DX.MakeKeyInput((uint)maxlen, 1, mode == 'A' ? 1 : 0, mode == '9' ? 1 : 0); // ハンドル生成

			DX.SetActiveKeyInput(inputHdl); // 入力開始

			DX.SetKeyInputString(initValue, inputHdl); // 初期値設定

			for (; ; )
			{
				if (DX.CheckKeyInput(inputHdl) != 0) // 入力終了
					break;

				DDCurtain.DrawCurtain();
				DX.DrawBox(0, 0, DDConsts.Screen_W, 250, DX.GetColor(30, 90, 90), 1);

				DDPrint.SetPrint(50, 50);
				DDPrint.Print("InputString > " + prompt);

				DX.DrawKeyInputModeString(50, 100); // IMEモードとか表示

				DX.GetKeyInputString(buff, inputHdl); // 現状の文字列を取得

				DDPrint.SetPrint(50, 150);
				DDPrint.Print(buff.ToString());

				DX.DrawKeyInputString(50, 200, inputHdl); // 入力中の文字列の描画

				DDEngine.EachFrame();
			}
			DX.DeleteKeyInput(inputHdl); // ハンドル開放

			DDEngine.FreezeInput();

			return buff.ToString();
		}
	}
}
