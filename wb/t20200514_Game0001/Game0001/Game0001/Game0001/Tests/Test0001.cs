using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			foreach (DDScene scene in DDSceneUtils.Create(600))
			{
				if (DDInput.A.GetInput() == 1)
				{
					break;
				}

				{
					double xRate = DDConsts.Screen_W * 1.0 / Ground.I.Picture.Pic0001.Get_W();
					double yRate = DDConsts.Screen_H * 1.0 / Ground.I.Picture.Pic0001.Get_H();

					DDDraw.DrawBegin(Ground.I.Picture.Pic0001, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
					DDDraw.DrawZoom(Math.Max(xRate, yRate));
					DDDraw.DrawEnd();

					DDCurtain.DrawCurtain(-0.5);

					DDDraw.DrawBegin(Ground.I.Picture.Pic0001, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
					DDDraw.DrawZoom(Math.Min(xRate, yRate));
					DDDraw.DrawEnd();
				}

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}
	}
}
