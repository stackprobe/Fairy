using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Tests.Common
{
	public class DDMouseTest
	{
		public void Test01()
		{
			int rot = 0;

			for (; ; )
			{
				DDCurtain.DrawCurtain();

				DDMouse.UpdatePos();

				rot += DDMouse.Rot;

				DDPrint.SetPrint();
				DDPrint.Print(string.Join(
					", "
					, DDMouse.L.GetInput()
					, DDMouse.R.GetInput()
					, DDMouse.M.GetInput()
					, DDMouse.L.IsPound() ? 1 : 0
					, DDMouse.R.IsPound() ? 1 : 0
					, DDMouse.M.IsPound() ? 1 : 0
					, DDMouse.X
					, DDMouse.Y
					//, DDMouse.Rot
					, rot
					));

				DDEngine.EachFrame();
			}
		}
	}
}
