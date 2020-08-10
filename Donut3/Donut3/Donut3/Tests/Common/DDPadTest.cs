using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;

namespace Charlotte.Tests.Common
{
	public class DDPadTest
	{
		public void Test01()
		{
			for (; ; )
			{
				DDCurtain.DrawCurtain();

				DDPrint.SetPrint();

				DDPrint.Print("PrimaryPadId: " + DDGround.PrimaryPadId);
				DDPrint.PrintRet();

				for (int btnId = 0; btnId < DDPad.PAD_BUTTON_MAX; btnId++)
				{
					DDPrint.Print(btnId + " ==> " + DDPad.GetInput(DDGround.PrimaryPadId, btnId));
					DDPrint.PrintRet();
				}
				DDEngine.EachFrame();
			}
		}
	}
}
