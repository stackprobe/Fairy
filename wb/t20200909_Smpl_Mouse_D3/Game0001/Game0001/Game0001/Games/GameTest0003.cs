using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public class GameTest0003
	{
		private const double SPEED_MIN = 0.1;
		private const double SPEED_DEF = 1.0;
		private const double SPEED_MAX = 10.0;
		private const double SPEED_CHANGE_STEP = 0.1;

		private int CROSS_WH = 100;

		public void Perform()
		{
			DDUtils.SetMouseDispMode(false);
			DDEngine.FreezeInput(10);

			double x = DDConsts.Screen_W / 2;
			double y = DDConsts.Screen_H / 2;
			double speed = SPEED_DEF;

			for (; ; )
			{
				DDMouse.UpdateMove();

				x += DDMouse.MoveX * speed;
				y += DDMouse.MoveY * speed;

				DDUtils.ToRange(ref x, 0, DDConsts.Screen_W - 1);
				DDUtils.ToRange(ref y, 0, DDConsts.Screen_H - 1);

				int ix = DoubleTools.ToInt(x);
				int iy = DoubleTools.ToInt(y);

				if (DDKey.IsPound(DX.KEY_INPUT_Z))
					speed += SPEED_CHANGE_STEP;

				if (DDKey.IsPound(DX.KEY_INPUT_X))
					speed -= SPEED_CHANGE_STEP;

				DDUtils.ToRange(ref speed, SPEED_MIN, SPEED_MAX);

				if (DDMouse.L.GetInput() == -1)
					break;

				DDCurtain.DrawCurtain();

				DDPrint.SetPrint(0, 0, 24);
				DDPrint.PrintLine("★マウスカーソルを奪う");
				DDPrint.PrintLine("X=" + x.ToString("F3"));
				DDPrint.PrintLine("Y=" + y.ToString("F3"));
				DDPrint.PrintLine("Speed=" + speed);
				DDPrint.PrintLine("左クリックでメニューに戻る。");
				DDPrint.PrintLine("Zキー:速度上げ");
				DDPrint.PrintLine("Xキー:速度下げ");

				DDDraw.SetBright(new I3Color(255, 128, 0));
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, ix, iy - CROSS_WH / 2, 1, CROSS_WH);
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, ix - CROSS_WH / 2, iy, CROSS_WH, 1);
				DDDraw.Reset();

				DDEngine.EachFrame();
			}
			DDUtils.SetMouseDispMode(true);
		}
	}
}
