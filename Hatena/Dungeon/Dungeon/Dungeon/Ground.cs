﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Ground
	{
		public static Ground I;

		public ResourceMusic Music = new ResourceMusic();
		public ResourcePicture Picture = new ResourcePicture();
		public ResourceSE SE = new ResourceSE();

		public int MakeMap_W = 30;
		public int MakeMap_H = 30;
		public int MakeMap_Seed = 1;
	}
}
