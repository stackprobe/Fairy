using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameDerivationUtils
	{
		public static List<GamePicture> Derivations = new List<GamePicture>();

		public static void Add(GamePicture derivation)
		{
			Derivations.Add(derivation);
		}

		public static void UnloadAll()
		{
			foreach (GamePicture derivation in Derivations)
				derivation.Unload();
		}
	}
}
