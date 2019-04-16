using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Security.Permissions;
using Charlotte.Tools;
using Charlotte.Game;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		public static MainWin Self = null;

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

			this.Visible = false;

			ProcMain.WriteLog = message =>
			{
#if DEBUG
				string file = @"C:\tmp\Game.log";
#else
				//string file = Path.Combine(ProcMain.SelfDir, Path.GetFileNameWithoutExtension(ProcMain.SelfFile) + ".log");
				string file = Path.Combine(ProcMain.SelfDir, ProcMain.APP_TITLE + ".log");
#endif

				using (StreamWriter writer = new StreamWriter(file, true, Encoding.UTF8))
				{
					writer.WriteLine("[" + DateTime.Now + "] " + message);
				}
			};

			Self = this;

			new Thread(() =>
			{
				try
				{
					new GameMain().Main();
				}
				catch (Exception ex)
				{
					ProcMain.WriteLog(ex);
				}

				this.BeginInvoke((MethodInvoker)delegate
				{
					this.Close();
				});
			})
			.Start();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// -- 9999
		}
	}
}
