using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Donut;

namespace Charlotte
{
	public class AA
	{
		public enum P
		{
			DUMMY,
			WHITEBOX,
			WHITECIRCLE,
		}

		public static int Pic(P picId)
		{
			return GamePicture.Pic((int)picId);
		}

		public static int Pic_W(P picId)
		{
			return GamePicture.Pic_W((int)picId);
		}

		public static int Pic_H(P picId)
		{
			return GamePicture.Pic_H((int)picId);
		}

		public enum D
		{
			DUMMY,
		}

		public static int Pic(D derId)
		{
			return GameDerivation.Der((int)derId);
		}

		public static int Pic_W(D derId)
		{
			return GameDerivation.Der_W((int)derId);
		}

		public static int Pic_H(D derId)
		{
			return GameDerivation.Der_H((int)derId);
		}
	}
}
