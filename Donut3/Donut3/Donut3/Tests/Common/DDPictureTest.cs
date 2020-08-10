using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Tests.Common
{
	public class DDPictureTest
	{
		public void Test01()
		{
			DDPicture title_wall = DDPictureLoaders.Standard("title_wall.png");

			title_wall.GetHandle();
		}
	}
}
