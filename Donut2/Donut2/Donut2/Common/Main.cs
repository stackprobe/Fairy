using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DxLibDLL;

namespace Charlotte.Common
{
	public partial class DD
	{
		private class Main
		{
		}

		public static void WinMain()
		{
			DX.SetApplicationLogSaveDirectory(@"C:\tmp");

			//new Icon(@"C:\Dat\Icon\game_app.ico").Handle;

			new GameMain().Main();

			// TODO
		}
	}
}
