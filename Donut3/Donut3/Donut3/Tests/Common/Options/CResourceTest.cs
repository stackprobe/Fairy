using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Common.Options;

namespace Charlotte.Tests.Common.Options
{
	public class CResourceTest
	{
		public void Test01()
		{
			for (; ; )
			{
				DDCurtain.DrawCurtain();

				DDDraw.DrawSimple(DDCResource.GetPicture(@"Fairy\Donut3\CResource\Picture\Picture0001.png"), 0, 0);
				DDDraw.DrawSimple(DDCResource.GetPicture(@"Fairy\Donut3\CResource\Picture\Picture0002.png"), 200, 0);
				DDDraw.DrawSimple(DDCResource.GetPicture(@"Fairy\Donut3\CResource\Picture\Picture0003.png"), 400, 0);

				DDEngine.EachFrame();
			}
		}
	}
}
