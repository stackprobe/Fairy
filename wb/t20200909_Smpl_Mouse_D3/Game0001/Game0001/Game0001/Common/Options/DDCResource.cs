using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Common.Options
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class DDCResource
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static Dictionary<string, DDPicture> PictureCache = DictionaryTools.CreateIgnoreCase<DDPicture>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DDPicture GetPicture(string file)
		{
			if (PictureCache.ContainsKey(file) == false)
				PictureCache.Add(file, DDPictureLoaders.Standard(file));

			return PictureCache[file];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static Dictionary<string, DDMusic> MusicCache = DictionaryTools.CreateIgnoreCase<DDMusic>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DDMusic GetMusic(string file)
		{
			if (MusicCache.ContainsKey(file) == false)
				MusicCache.Add(file, new DDMusic(file));

			return MusicCache[file];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static Dictionary<string, DDSE> SECache = DictionaryTools.CreateIgnoreCase<DDSE>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DDSE GetSE(string file)
		{
			if (SECache.ContainsKey(file) == false)
				SECache.Add(file, new DDSE(file));

			return SECache[file];
		}
	}
}
