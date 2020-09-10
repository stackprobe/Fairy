using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public class GameTest0002
	{
		private int WARP_L = 600;
		private int WARP_T = 400;
		private int WARP_W = 100;
		private int WARP_H = 100;

		private int WARP_DEST_X = 200;
		private int WARP_DEST_Y = 300;

		private int CROSS_WH = 100;

		public void Perform()
		{
			DDEngine.FreezeInput(10);

			for (; ; )
			{
				DDMouse.UpdatePos();

				if (DDUtils.IsOut(new D2Point(DDMouse.X, DDMouse.Y), new D4Rect(WARP_L, WARP_T, WARP_W, WARP_H)) == false)
				{
					DDMouse.X = WARP_DEST_X;
					DDMouse.Y = WARP_DEST_Y;

					DDMouse.ApplyPos();
				}
				if (DDMouse.L.GetInput() == -1)
					break;

				DDCurtain.DrawCurtain();

				DDPrint.SetPrint(0, 0, 24);
				DDPrint.PrintLine("★位置取得と位置設定");
				DDPrint.PrintLine("X=" + DDMouse.X);
				DDPrint.PrintLine("Y=" + DDMouse.Y);
				DDPrint.PrintLine("左クリックでメニューに戻る。");
				DDPrint.PrintLine("水色の領域にマウスカーソルを移動すると、黄色い十字にワープする。");

				DDDraw.SetBright(new I3Color(0, 200, 200));
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, WARP_L, WARP_T, WARP_W, WARP_H);
				DDDraw.Reset();

				DDDraw.SetBright(new I3Color(200, 200, 0));
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, WARP_DEST_X, WARP_DEST_Y - CROSS_WH / 2, 1, CROSS_WH);
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, WARP_DEST_X - CROSS_WH / 2, WARP_DEST_Y, CROSS_WH, 1);
				DDDraw.Reset();

				DDEngine.EachFrame();
			}
		}
	}
}
