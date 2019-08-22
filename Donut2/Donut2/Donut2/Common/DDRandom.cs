using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class DDRandom
	{
		private uint X;
		private uint Y;
		private uint Z;
		private uint A;

		public DDRandom()
			: this(SecurityTools.CRandom.GetUInt(), SecurityTools.CRandom.GetUInt(), SecurityTools.CRandom.GetUInt(), SecurityTools.CRandom.GetUInt())
		{ }

		public DDRandom(uint x, uint y, uint z, uint a)
		{
			if ((x | y | z | a) == 0u)
				x = 1u;

			this.X = x;
			this.Y = y;
			this.Z = z;
			this.A = a;
		}

		public uint Next()
		{
			uint t;

			t = this.X;
			t ^= this.X << 11;
			t ^= t >> 8;
			t ^= this.A;
			t ^= this.A >> 19;
			this.X = this.Y;
			this.Y = this.Z;
			this.Z = this.A;
			this.A = t;

			return t;
		}

		public double Real2() // ret: 0.0 <=, < 1.0
		{
			return this.Next() / (double)(uint.MaxValue + 1L);
		}
	}
}
