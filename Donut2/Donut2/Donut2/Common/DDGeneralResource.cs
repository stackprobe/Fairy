﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class DDGeneralResource
	{
		public DDPicture Dummy = DDPictureLoaders.Standard(@"General\Dummy.png");
		public DDPicture WhiteBox = DDPictureLoaders.Standard(@"General\WhiteBox.png");
		public DDPicture WhiteCircle = DDPictureLoaders.Standard(@"General\WhiteCircle.png");

		// 新しいリソースをここへ追加...
	}
}
