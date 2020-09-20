using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	/// <summary>
	/// <para>全プロジェクトで共通のリソース</para>
	/// <para>当該リソースは Resource/General フォルダに収録すること。</para>
	/// </summary>
	public class DDGeneralResource
	{
		// game_app.ico --> DDMain.GetAppIcon()

		public DDPicture Dummy = DDPictureLoaders.Standard(@"General\Dummy.png");
		public DDPicture WhiteBox = DDPictureLoaders.Standard(@"General\WhiteBox.png");
		public DDPicture WhiteCircle = DDPictureLoaders.Standard(@"General\WhiteCircle.png");

		public DDMusic 無音 = new DDMusic(@"General\muon.wav");

		// 新しいリソースをここへ追加...
	}
}
