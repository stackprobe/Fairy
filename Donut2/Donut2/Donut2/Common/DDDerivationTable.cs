using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class DDDerivationTable
	{
		private AutoTable<DDPicture> DerTable;

		public DDDerivationTable(DDPicture picture, int x, int y, int w, int h, int xNum, int yNum, int xStep = -1, int yStep = -1)
		{
			if (xStep == -1) xStep = w;
			if (yStep == -1) yStep = h;

			this.DerTable = new AutoTable<DDPicture>(xNum, yNum);

			for (int xc = 0; xc < xNum; xc++)
			{
				for (int yc = 0; yc < yNum; yc++)
				{
					this.DerTable[xc, yc] = DDDerivations.GetPicture(picture, x + xc * xStep, y + yc * yStep, w, h);
				}
			}
		}

		public DDPicture this[int x, int y]
		{
			get
			{
				return this.DerTable[x, y];
			}
		}
	}
}
