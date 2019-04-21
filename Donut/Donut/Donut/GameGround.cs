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

		public void LoadFromDatFile()
		{
			//
		}

		public void SaveToDatFile()
		{
			//
		}

		public GameConfig Config = new GameConfig();

		public HandleDam Handles; // Codevil の GetFinalizers() に相当する。
		public HandleDam GameHandles = new HandleDam(); // Codevil の GetEndProcFinalizers() に相当する。

		public I2Size ScreenSize = new I2Size(800, 600); // Codevil の Gnd.RealScreen_WH
		public I4Rect ScreenDrawRect = null; // Codeliv の Gnd.RealScreenDraw_LTWH, null == 不使用
		public I4Rect MonitorRect;

		public bool RO_MouseDispMode = false;
	}
}
