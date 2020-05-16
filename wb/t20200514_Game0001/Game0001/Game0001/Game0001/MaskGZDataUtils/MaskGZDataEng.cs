using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ^ sync @ MakeGZDataEng

namespace Charlotte.MaskGZDataUtils
{
	// sync > @ MakeGZDataEng

	public class MaskGZDataEng
	{
		private uint X;

		private uint Rand()
		{
			// Xorshift-32

			this.X ^= this.X << 13;
			this.X ^= this.X >> 17;
			this.X ^= this.X << 5;

			return this.X;
		}

		private void Shuffle(int[] values)
		{
			for (int index = values.Length; 2 <= index; index--)
			{
				int a = index - 1;
				int b = (int)(Rand() % (uint)index);

				int tmp = values[a];
				values[a] = values[b];
				values[b] = tmp;
			}
		}

		public void Transpose(byte[] data, uint seed = 123456789)
		{
			int[] swapIdxLst = Enumerable.Range(1, data.Length / 2).ToArray();

			this.X = seed;
			this.Shuffle(swapIdxLst);
			this.X = 0; // clear

			for (int index = 0; index < swapIdxLst.Length; index++)
			{
				int a = index;
				int b = data.Length - swapIdxLst[index];

				byte tmp = data[a];
				data[a] = data[b];
				data[b] = tmp;
			}
		}
	}

	// < sync
}
