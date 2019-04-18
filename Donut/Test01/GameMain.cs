using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Donut;

namespace Charlotte
{
	public class GameMain : IGameMain
	{
		public void Init()
		{
			// noop
		}

		public void Main()
		{
			// TODO ???
		}

		public IntPtr GetIcon()
		{
			return new Icon(@"C:\Dat\Icon\game_app.ico").Handle;
		}
	}
}
