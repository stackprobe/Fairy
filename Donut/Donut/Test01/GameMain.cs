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
				GameDrawPicture.DPE.SetBright(DX.GetColor(0, 0, 0));
				GameDrawPicture.DrawRect((int)AA.P.WHITEBOX, 0.0, 0.0, GameGround.I.ScreenSize.W, GameGround.I.ScreenSize.H);
				GameDrawPicture.DPE.Reset();

				GameDrawPicture.DrawBegin((int)AA.P.DUMMY, GameGround.I.ScreenCenterX, GameGround.I.ScreenCenterY);
				GameDrawPicture.DrawRotate(GameEngine.ProcFrame / 0.001);
				GameDrawPicture.DrawEnd();

				GameEngine.EachFrame(); // あれ、60fpsになってなくね？？？
			}
		}

		public IntPtr GetIcon()
		{
			return new Icon(@"C:\Dat\Icon\game_app.ico").Handle;
		}
	}
}
