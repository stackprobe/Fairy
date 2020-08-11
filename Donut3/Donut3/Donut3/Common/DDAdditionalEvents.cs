using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class DDAdditionalEvents
	{
		public static Action Ground_INIT = () =>
		{
			//DDGround.RO_MouseDispMode = true;
		};

		public static Action PostGameStart = () =>
		{
			//DDFontRegister.Add(@"Font\Genkai-Mincho-font\genkai-mincho.ttf");
		};

		public static Action PostGameStart_G2 = () =>
		{
			//this.Visible = false;
		};

		public static Action<List<string>> Save = lines =>
		{
			lines.Add("Donut3-SaveData"); // Dummy
		};

		public static Action<string[]> Load = lines =>
		{
			int c = 0;

			DDUtils.Noop(lines[c++]); // Dummy
		};

		public static Action PostDrawPicMain = () => { };
		public static Action PostEachFrame = () => { };
	}
}
