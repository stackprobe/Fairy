using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte
{
	public class ResourcePicture
	{
		public DDPicture Background = DDPictureLoaders.Standard(@"Wall\Background.png");
		public DDPicture Wall = DDPictureLoaders.Standard(@"Wall\Wall.png");
		public DDPicture Gate = DDPictureLoaders.Standard(@"Wall\Goal.png");

		public DDPicture GameFrame = DDPictureLoaders.Standard(@"GameFrame.png");
	}
}
