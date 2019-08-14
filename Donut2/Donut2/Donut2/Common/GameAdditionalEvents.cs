using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameAdditionalEvents
	{
		public static Action Ground_INIT = () =>
		{
			//GameGround.RO_MouseDispMode = true;
		};
		public static Action Ground_FNLZ = () => { };
		public static Action PostGameStart = () =>
		{
			//GameFontRegister.Add(@"Font\Genkai-Mincho-font\genkai-mincho.ttf");
		};
		public static Action PostGameStart_G2 = () =>
		{
			//this.Visible = false;
		};
		public static Action<List<string>> Save = lines =>
		{
			lines.Add(DateTime.Now.ToString()); // Dummy
		};
		public static Action<string[]> Load = lines =>
		{
			int c = 0;

			GameUtils.Noop(lines[c++]); // Dummy
		};
	}
}
