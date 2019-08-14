using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;

namespace Charlotte.Tests.Common
{
	public class GamePadTest
	{
		public void Test01()
		{
			for (; ; )
			{
				GameCurtain.DrawCurtain();

				GamePrint.SetPrint();

				GamePrint.Print("PrimaryPadId: " + GameGround.PrimaryPadId);
				GamePrint.PrintRet();

				for (int btnId = 0; btnId < GamePad.PAD_BUTTON_MAX; btnId++)
				{
					GamePrint.Print(btnId + " ==> " + GamePad.GetInput(GameGround.PrimaryPadId, btnId));
					GamePrint.PrintRet();
				}
				GameEngine.EachFrame();
			}
		}
	}
}
