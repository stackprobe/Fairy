using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class DDSceneUtils
	{
		public static IEnumerable<DDScene> Create(int frameMax)
		{
			for (int frame = 0; frame <= frameMax; frame++)
			{
				yield return new DDScene()
				{
					Numer = frame,
					Denom = frameMax,
					Rate = (double)frame / frameMax,
				};
			}
		}
	}
}
