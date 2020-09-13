﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class DDConsts
	{
		public const string ConfigFile = "Config.conf";
		public const string SaveDataFile = "SaveData.dat";
		public const string ResourceFile_01 = "Resource.dat";
		public const string ResourceFile_02 = "res.dat";
		public const string ResourceDir_01 = @"C:\Dat\Resource";
		public const string ResourceDir_02 = @"..\..\..\..\res";
		public const string UserDatStringsFile = "Properties.dat";

#if false // 例
		// discmt >

		public const int Screen_W = 800;
		public const int Screen_H = 600;

		// < discmt
#else
		// app > @ Screen_WH

		public const int Screen_W = 800;
		public const int Screen_H = 600;

		// < app
#endif

		public const int Screen_W_Min = 100;
		public const int Screen_H_Min = 100;
		public const int Screen_W_Max = 4000;
		public const int Screen_H_Max = 3000;

		public const double DefaultVolume = 0.45;
	}
}
