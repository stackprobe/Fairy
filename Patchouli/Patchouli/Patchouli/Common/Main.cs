using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	public class Main
	{
		public void Main1()
		{
			using (new MSection("{c808da96-120f-4de5-846a-8c1c120b4a69}")) // shared_uuid@g -- 全ゲーム同時プレイ禁止のため、@g(global)指定
			{
				Main2();
			}
		}

		private void Main2()
		{
#if DEBUG
			DX.SetApplicationLogSaveDirectory(@"C:\tmp");
#endif
			DX.SetOutApplicationLogValidFlag(Define.LOG_ENABLED ? 1 : 0); // DxLib のログを出力 1: する 0: しない

			DX.SetAlwaysRunFlag(1); // ? 非アクティブ時に 1: 動く 0: 止まる

#if DEBUG
			DX.SetMainWindowText("DEBUG MODE");
#else
			DX.SetMainWindowText(ProcMain.APP_TITLE + " 0.00"); // TODO
#endif

			//DX.SetGraphMode(Define.SCREEN_W, Define.SCREEN_H, 32);
			DX.SetGraphMode(Ground.RealScreen_W, Ground.RealScreen_H, 32);
			DX.ChangeWindowMode(1); // 1: ウィンドウ 0: フルスクリーン

			//DX.SetFullSceneAntiAliasingMode(4, 2); // 適当な値が分からん。フルスクリーン廃止したので不要

			DX.SetWindowIconHandle(MainWin.Self.Icon.Handle); // ウィンドウ左上のアイコン

			if (Config.DisplayIndex != -1)
				DX.SetUseDirectDrawDeviceIndex(Config.DisplayIndex);

			if (DX.DxLib_Init() != 0)
				throw new Exception();

			// TODO
			// TODO
			// TODO
		}
	}
}
