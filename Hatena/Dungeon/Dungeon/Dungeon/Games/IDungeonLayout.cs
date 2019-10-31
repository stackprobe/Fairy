using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games
{
	public interface IDungeonLayout
	{
		DDPicture GetWallPicture();
		DDPicture GetGatePicture();
		DDPicture GetBackgroundPicture();

		/// <summary>
		/// 指定された壁の状態を得る。
		/// </summary>
		/// <param name="x">左右位置(-1=左隣,0=ここ,1=右隣)</param>
		/// <param name="y">前後位置(0=ここ,1=1歩前,2=2歩前)</param>
		/// <param name="direction">方向(2468)</param>
		/// <returns>壁の状態</returns>
		MapWall.Kind_e GetWall(int x, int y, int direction);
	}
}
