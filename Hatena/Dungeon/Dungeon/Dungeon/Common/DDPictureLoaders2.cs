using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	/// <summary>
	/// Unload()する必要なし。
	/// 必要あり -> DDPictureLoader
	/// </summary>
	public static class DDPictureLoaders2
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DDPicture Wrapper(int handle, int w, int h)
		{
			DDPicture.PictureInfo info = new DDPicture.PictureInfo()
			{
				Handle = handle,
				W = w,
				H = h,
			};

			return new DDPicture(() => info, v => { }, v => { });
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DDPicture Wrapper(int handle, I2Size size)
		{
			return Wrapper(handle, size.W, size.H);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DDPicture Wrapper(DDSubScreen subScreen)
		{
			return Wrapper(subScreen.GetHandle(), subScreen.GetSize());
		}
	}
}
