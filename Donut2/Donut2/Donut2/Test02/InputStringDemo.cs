using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;

namespace Charlotte.Test02
{
	public class InputStringDemo
	{
		public void Perform()
		{
			StringBuilder buff = new StringBuilder(100); // マージンどんくらい要るの？
			//StringBuilder buff = new StringBuilder(30);

			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			int inputHdl = DX.MakeKeyInput(30, 1, 0, 0); // ハンドル生成

			DX.SetActiveKeyInput(inputHdl); // 入力開始

			for (; ; )
			{
				if (DX.CheckKeyInput(inputHdl) != 0) // 入力終了
					break;

				DDCurtain.DrawCurtain();

				DX.DrawKeyInputModeString(100, 100); // IMEモードとか表示

				DX.GetKeyInputString(buff, inputHdl); // 現状の文字列を取得

				DDPrint.SetPrint(100, 200);
				DDPrint.Print(buff.ToString());

				DX.DrawKeyInputString(100, 300, inputHdl); // 入力中の文字列の描画

				DDEngine.EachFrame();
			}
			DX.DeleteKeyInput(inputHdl); // ハンドル開放

			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				DDCurtain.DrawCurtain();

				DDPrint.SetPrint(100, 200);
				DDPrint.Print(buff.ToString());

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}
	}
}
