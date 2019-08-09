using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GamePictureLoaders2
	{
		public static GamePicture Wrapper(int handle, int w, int h)
		{
			GamePicture.PictureInfo info = new GamePicture.PictureInfo()
			{
				Handle = handle,
				W = w,
				H = h,
			};

			return new GamePicture(() => info, v => { }, v => { });
		}
	}
}
