using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameSceneUtils
	{
		public static IEnumerable<GameScene> Create(int frameMax)
		{
			for (int frame = 0; frame <= frameMax; frame++)
			{
				yield return new GameScene()
				{
					Numer = frame,
					Denom = frameMax,
					Rate = (double)frame / frameMax,
				};
			}
		}
	}
}
