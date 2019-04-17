using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public class GameGround
	{
		public static GameGround I = null;

		public GameGround()
		{ }

		public HandleDam Finalizers;
		public HandleDam EndProcFinalizers = new HandleDam();

		public I2Size ScreenSize = new I2Size(800, 600);
	}
}
