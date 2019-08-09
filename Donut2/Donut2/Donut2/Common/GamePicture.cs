using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class GamePicture
	{
		public class PictureInfo
		{
			public int Handle;
			public int W;
			public int H;
		}

		private PictureInfo Info = null; // null == Unloaded
		private Func<PictureInfo> Loader;
		private Action<PictureInfo> Unloader;

		public GamePicture(Func<PictureInfo> loader, Action<PictureInfo> unloader, Action<GamePicture> adder)
		{
			this.Loader = loader;
			this.Unloader = unloader;

			adder(this);
		}

		public void Unload()
		{
			// この画像を参照している GameDerivation を先に Unload しなければならない。

			if (this.Info != null)
			{
				this.Unloader(this.Info);
				this.Info = null;
			}
		}

		private PictureInfo GetInfo()
		{
			if (this.Info == null)
				this.Info = this.Loader();

			return this.Info;
		}

		public int GetHandle()
		{
			return this.GetInfo().Handle;
		}

		public int Get_W()
		{
			return this.GetInfo().W;
		}

		public int Get_H()
		{
			return this.GetInfo().H;
		}
	}
}
