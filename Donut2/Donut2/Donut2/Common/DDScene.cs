using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class DDScene
	{
		public int Numer;
		public int Denom;
		public double Rate;

		public int Remaining
		{
			get
			{
				return this.Denom - this.Numer;
			}
		}
	}
}
