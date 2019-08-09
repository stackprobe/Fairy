using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GamePictureUtils
	{
		public static List<GamePicture> Pictures = new List<GamePicture>();

		public static void Add(GamePicture picture)
		{
			Pictures.Add(picture);
		}

		public static void UnloadAll()
		{
			GameDerivationUtils.UnloadAll(); // 先に！

			foreach (GamePicture picture in Pictures)
				picture.Unload();
		}
	}
}
