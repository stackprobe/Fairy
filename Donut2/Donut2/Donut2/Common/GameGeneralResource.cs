using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class GameGeneralResource
	{
		public GamePicture Dummy = GamePictureLoaders.Standard(@"General\Dummy.png");
		public GamePicture WhiteBox = GamePictureLoaders.Standard(@"General\WhiteBox.png");

		// 新しいリソースをここへ追加...
	}
}
