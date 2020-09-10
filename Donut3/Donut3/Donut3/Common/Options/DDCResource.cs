using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Common.Options
{
	public static class DDCResource
	{
		private static Dictionary<string, DDPicture> PictureCache = DictionaryTools.CreateIgnoreCase<DDPicture>();

		public static DDPicture GetPicture(string file)
		{
			if (PictureCache.ContainsKey(file) == false)
				PictureCache.Add(file, DDPictureLoaders.Standard(file));

			return PictureCache[file];
		}

		private static Dictionary<string, DDMusic> MusicCache = DictionaryTools.CreateIgnoreCase<DDMusic>();

		public static DDMusic GetMusic(string file)
		{
			if (MusicCache.ContainsKey(file) == false)
				MusicCache.Add(file, new DDMusic(file));

			return MusicCache[file];
		}

		private static Dictionary<string, DDSE> SECache = DictionaryTools.CreateIgnoreCase<DDSE>();

		public static DDSE GetSE(string file)
		{
			if (SECache.ContainsKey(file) == false)
				SECache.Add(file, new DDSE(file));

			return SECache[file];
		}
	}
}
