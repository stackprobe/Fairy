using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	/// <summary>
	/// Unload()する必要なし。
	/// 必要あり -> DDPictureLoader
	/// </summary>
	public static class DDPictureLoaders2
	{
		public static DDPicture Wrapper(Func<int> getHandle, int w, int h)
		{
			DDPicture.PictureInfo info = new DDPicture.PictureInfo()
			{
				Handle = -1,
				W = w,
				H = h,
			};

			return new DDPicture(() =>
			{
				info.Handle = getHandle();
				return info;
			},
			v => { },
			v => { }
			);
		}

		public static DDPicture Wrapper(Func<int> getHandle, I2Size size)
		{
			return Wrapper(getHandle, size.W, size.H);
		}

		public static DDPicture Wrapper(DDSubScreen subScreen)
		{
			return Wrapper(() => subScreen.GetHandle(), subScreen.GetSize());
		}
	}
}
