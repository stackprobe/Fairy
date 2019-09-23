using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class DDTable<T>
	{
		private T[] Inner;

		public int W { get; private set; }
		public int H { get; private set; }

		public DDTable(int w, int h)
		{
			if (
				w < 1 || IntTools.IMAX < w ||
				h < 1 || IntTools.IMAX / w < h
				)
				throw new DDError();

			this.Inner = new T[w * h];
			this.W = w;
			this.H = h;
		}

		public T this[int x, int y]
		{
			get
			{
				return this.Inner[x + y * this.W];
			}

			set
			{
				this.Inner[x + y * this.W] = value;
			}
		}

		public T GetCell(int x, int y, T defval = default(T))
		{
			if (
				x < 0 || this.W <= x ||
				y < 0 || this.H <= y
				)
				return defval;

			return this[x, y];
		}

		public IEnumerable<T> Iterate()
		{
#if true
			return this.Inner; // 要素が変更されても問題無いっぽい。
#else
			for (int index = 0; index < this.Inner.Length; index++)
			{
				yield return this.Inner[index];
			}
#endif
		}
	}
}
