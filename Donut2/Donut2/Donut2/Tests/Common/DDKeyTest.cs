using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;

namespace Charlotte.Tests.Common
{
	public class DDKeyTest
	{
		public void Test01()
		{
			for (; ; )
			{
				DDCurtain.DrawCurtain();

				DDPrint.SetPrint(); DDPrint.Print("RETURN ==> " + DDKey.GetInput(DX.KEY_INPUT_RETURN));
				DDPrint.PrintRet(); DDPrint.Print("SPACE  ==> " + DDKey.GetInput(DX.KEY_INPUT_SPACE));
				DDPrint.PrintRet(); DDPrint.Print("Z      ==> " + DDKey.GetInput(DX.KEY_INPUT_Z));
				DDPrint.PrintRet(); DDPrint.Print("X      ==> " + DDKey.GetInput(DX.KEY_INPUT_X));
				DDPrint.PrintRet(); DDPrint.Print("DIR_4  ==> " + DDKey.GetInput(DX.KEY_INPUT_LEFT));
				DDPrint.PrintRet(); DDPrint.Print("DIR_6  ==> " + DDKey.GetInput(DX.KEY_INPUT_RIGHT));
				DDPrint.PrintRet(); DDPrint.Print("DIR_8  ==> " + DDKey.GetInput(DX.KEY_INPUT_UP));
				DDPrint.PrintRet(); DDPrint.Print("DIR_2  ==> " + DDKey.GetInput(DX.KEY_INPUT_DOWN));

				DDEngine.EachFrame();
			}
		}
	}
}
