using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public class ResourceClusterFile
	{
		private static readonly byte[] SIGNATURE = Encoding.ASCII.GetBytes("DonutRes");

		private string ClusterFile;
		private int ResCount;
		private long[] ResStartPositions;
		private int[] ResSizes;

		public ResourceClusterFile(string file)
		{
			this.ClusterFile = file;

			long fileSize = new FileInfo(file).Length;

			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				byte[] signature = FileTools.Read(reader, SIGNATURE.Length);

				if (BinTools.Comp(signature, SIGNATURE) != 0)
					throw new DD.Error("Bad signature");

				this.ResCount = BinTools.ToInt(FileTools.Read(reader, 4));

				if (this.ResCount < 0 || IntTools.IMAX < this.ResCount)
					throw new DD.Error("Bad ResCount");

				this.ResStartPositions = new long[this.ResCount];
				this.ResSizes = new int[this.ResCount];

				long count = SIGNATURE.Length + 4 + this.ResCount * 4;

				for (int index = 0; index < this.ResCount; index++)
				{
					int resSize = BinTools.ToInt(FileTools.Read(reader, 4));

					if (resSize < 0 || IntTools.IMAX < resSize)
						throw new DD.Error("Bad resSize " + resSize);

					this.ResStartPositions[index] = count;
					this.ResSizes[index] = resSize;

					if (LongTools.IMAX_64 - count < (long)resSize)
						throw new DD.Error("Bad resSize " + resSize + ", " + count);

					count += (long)resSize;
				}
				if (count + 64L != fileSize)
					throw new DD.Error("Bad fileSize " + fileSize + ", " + count);

				reader.Seek(0L, SeekOrigin.Begin);

				byte[] hash = SecurityTools.GetSHA512(FileTools.Iterate(new LimitedReader(count, reader.Read).Read));
				byte[] hash2 = FileTools.Read(reader, 64);

				if (BinTools.Comp(hash, hash2) != 0)
					throw new DD.Error("Bad hash");
			}
		}

		public int GetResCount()
		{
			return this.ResCount;
		}

		public byte[] LoadResFile(int index)
		{
			if (index < 0 || this.ResCount <= index)
				throw new DD.Error("そんなリソースありません。" + index + ", " + this.ResCount);

			using (FileStream reader = new FileStream(this.ClusterFile, FileMode.Open, FileAccess.Read))
			{
				reader.Seek(this.ResStartPositions[index], SeekOrigin.Begin);
				return ZipTools.Decompress(FileTools.Read(reader, this.ResSizes[index]));
			}
		}
	}
}
