using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

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

		public void Test02()
		{
			DDPicture title_wall = DDPictureLoaders.Standard("title_wall.png");

			for (; ; )
			{
				DDDraw.DrawFree(
					title_wall,
					new D2Point(
						Math.Cos(DDEngine.ProcFrame / 11.0) * 100 + 100,
						Math.Sin(DDEngine.ProcFrame / 11.0) * 100 + 100
						),
					new D2Point(
						Math.Cos(DDEngine.ProcFrame / 13.0) * 100 + 700,
						Math.Sin(DDEngine.ProcFrame / 13.0) * 100 + 100
						),
					new D2Point(
						Math.Cos(DDEngine.ProcFrame / 17.0) * 100 + 700,
						Math.Sin(DDEngine.ProcFrame / 17.0) * 100 + 500
						),
					new D2Point(
						Math.Cos(DDEngine.ProcFrame / 19.0) * 100 + 100,
						Math.Sin(DDEngine.ProcFrame / 19.0) * 100 + 500
						)
					);

				DDEngine.EachFrame();
			}
		}
	}
}
