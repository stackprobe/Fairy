using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			ProcMain.GUIMain(() => new MainWin(), APP_IDENT, APP_TITLE);
		}

		public const string APP_IDENT = "{becd9f3d-2e89-458c-8d07-6eaa0cdc82e4}";
		public const string APP_TITLE = "Game0001";
	}
}
