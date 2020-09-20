using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	/// <summary>
	/// <para>全プロジェクトで共通のリソース</para>
	/// <para>当該リソースは C:/Dat/Resource/Fairy/Donut3/General フォルダに収録すること。</para>
	/// </summary>
	public class DDGeneralResource
	{
		// game_app.ico --> DDMain.GetAppIcon()

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public DDPicture Dummy = DDPictureLoaders.Standard(@"Fairy\Donut3\General\Dummy.png");
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public DDPicture WhiteBox = DDPictureLoaders.Standard(@"Fairy\Donut3\General\WhiteBox.png");
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public DDPicture WhiteCircle = DDPictureLoaders.Standard(@"Fairy\Donut3\General\WhiteCircle.png");

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public DDMusic 無音 = new DDMusic(@"Fairy\Donut3\General\muon.wav");

		// 新しいリソースをここへ追加...
	}
}
