using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Tests.Common
{
	public class GameDrawTest
	{
		public void Test01()
		{
			GamePicture title_wall = GamePictureLoaders.Standard("title_wall.png");

			for (; ; )
			{
				//GameDraw.DrawSimple(title_wall, 0, 0);
				GameDraw.DrawRect(title_wall, 0, 0, GameConsts.Screen_W, GameConsts.Screen_H);

				GameEngine.EachFrame();
			}
		}
	}
}
