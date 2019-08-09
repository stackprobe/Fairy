using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;
using System.Drawing;

namespace Charlotte.Common
{
	public class GameSubScreen : IDisposable
	{
		private int W;
		private int H;
		private bool AFlag;
		private int Handle = -1; // -1 == 未設定

		public GameSubScreen(int w, int h, bool aFlag = false)
		{
			this.W = w;
			this.H = h;
			this.AFlag = aFlag;
			this.Handle = -1;

			GameSubScreenUtils.Add(this);
		}

		public void Dispose()
		{
			if (GameSubScreenUtils.Remove(this) == false) // ? Already disposed
				return;

			if (this.Handle != -1)
				if (DX.DeleteGraph(this.Handle) != 0)
					throw new Exception("Failed to DX.DeleteGraph()");
		}

		public void Unload()
		{
			if (this.Handle != -1)
			{
				if (DX.DeleteGraph(this.Handle) != 0)
					throw new Exception("Failed to DX.DeleteGraph()");

				this.Handle = -1;
			}
		}

		public int GetHandle()
		{
			if (this.Handle == -1)
			{
				this.Handle = DX.MakeScreen(this.W, this.H, this.AFlag ? 1 : 0);

				if (this.Handle == -1)
					throw new Exception("Failed to DX.MaksScreen()");
			}
			return this.Handle;
		}

		public void ChangeDrawScreen()
		{
			GameSubScreenUtils.ChangeDrawScreen(this);
		}

		public Size GetSize()
		{
			return new Size(this.W, this.H);
		}
	}
}
