using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Tests.Common
{
	public class GamePictureTest
	{
		public void Test01()
		{
			GamePicture title_wall = GamePictureLoaders.Standard("title_wall.png");

			title_wall.GetHandle();
		}
	}
}
