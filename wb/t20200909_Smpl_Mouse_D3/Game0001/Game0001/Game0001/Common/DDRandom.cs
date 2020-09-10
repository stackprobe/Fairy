using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	/// <summary>
	/// <para>擬似乱数列</para>
	/// </summary>
	public class DDRandom
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private uint X;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private uint Y;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private uint Z;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private uint A;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public DDRandom()
			: this(SecurityTools.CRandom.GetUInt(), SecurityTools.CRandom.GetUInt(), SecurityTools.CRandom.GetUInt(), SecurityTools.CRandom.GetUInt())
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public DDRandom(uint x, uint y, uint z, uint a)
		{
			if ((x | y | z | a) == 0u)
				x = 1u;

			this.X = x;
			this.Y = y;
			this.Z = z;
			this.A = a;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// [0,2^32)
		/// </summary>
		/// <returns>乱数</returns>
		public uint Next()
		{
			// Xorshift-128

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

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// [0,1]
		/// </summary>
		/// <returns>乱数</returns>
		public double Real()
		{
			return this.Next() / (double)uint.MaxValue;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// [0,1)
		/// </summary>
		/// <returns>乱数</returns>
		public double Real2()
		{
			return this.Next() / (double)(uint.MaxValue + 1L);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// (0,1)
		/// </summary>
		/// <returns>乱数</returns>
		public double Real3()
		{
			return this.Next() / (double)(uint.MaxValue + 1L) + 0.5;
		}
	}
}
