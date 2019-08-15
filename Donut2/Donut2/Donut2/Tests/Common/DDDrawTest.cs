using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Tests.Common
{
	public class DDDrawTest
	{
		public void Test01()
		{
			DDPicture title_wall = DDPictureLoaders.Standard("title_wall.png");

			for (; ; )
			{
				//DDDraw.DrawSimple(title_wall, 0, 0);
				DDDraw.DrawRect(title_wall, 0, 0, DDConsts.Screen_W, DDConsts.Screen_H);

				DDEngine.EachFrame();
			}
		}
	}
}
