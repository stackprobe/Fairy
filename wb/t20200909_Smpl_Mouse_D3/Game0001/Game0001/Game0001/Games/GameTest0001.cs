using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games
{
	public class GameTest0001
	{
		public void Perform()
		{
			DDEngine.FreezeInput(10);

			for (; ; )
			{
				DDMouse.UpdatePos();

				if (DDMouse.L.GetInput() == -1)
					break;

				DDCurtain.DrawCurtain();

				DDPrint.SetPrint(0, 0, 24);
				DDPrint.PrintLine("★位置取得のみ");
				DDPrint.PrintLine("X=" + DDMouse.X);
				DDPrint.PrintLine("Y=" + DDMouse.Y);
				DDPrint.PrintLine("左クリックでメニューに戻る。");

				DDEngine.EachFrame();
			}
		}
	}
}
