using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;

namespace Charlotte.Tests.Common
{
	public class GameKeyTest
	{
		public void Test01()
		{
			for (; ; )
			{
				GameCurtain.DrawCurtain();

				GamePrint.SetPrint(); GamePrint.Print("RETURN ==> " + GameKey.GetInput(DX.KEY_INPUT_RETURN));
				GamePrint.PrintRet(); GamePrint.Print("SPACE  ==> " + GameKey.GetInput(DX.KEY_INPUT_SPACE));
				GamePrint.PrintRet(); GamePrint.Print("Z      ==> " + GameKey.GetInput(DX.KEY_INPUT_Z));
				GamePrint.PrintRet(); GamePrint.Print("X      ==> " + GameKey.GetInput(DX.KEY_INPUT_X));
				GamePrint.PrintRet(); GamePrint.Print("DIR_4  ==> " + GameKey.GetInput(DX.KEY_INPUT_LEFT));
				GamePrint.PrintRet(); GamePrint.Print("DIR_6  ==> " + GameKey.GetInput(DX.KEY_INPUT_RIGHT));
				GamePrint.PrintRet(); GamePrint.Print("DIR_8  ==> " + GameKey.GetInput(DX.KEY_INPUT_UP));
				GamePrint.PrintRet(); GamePrint.Print("DIR_2  ==> " + GameKey.GetInput(DX.KEY_INPUT_DOWN));

				GameEngine.EachFrame();
			}
		}
	}
}
