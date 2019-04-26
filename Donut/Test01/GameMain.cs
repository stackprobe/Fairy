using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;
using Charlotte.Donut;
using DxLibDLL;

namespace Charlotte
{
	public class GameMain : IGameMain
	{
		public void Init()
		{
			//GameGround.I.ScreenSize = new I2Size(600, 400);
			//GameGround.I.ScreenSize = new I2Size(500, 500);
			//GameGround.I.ScreenSize = new I2Size(400, 600);
		}

		public void Main()
		{
			for (; ; )
			{
				GameEngine.EachFrame();
			}
		}

		public IntPtr GetIcon()
		{
			return new Icon(@"C:\Dat\Icon\game_app.ico").Handle;
		}
	}
}
