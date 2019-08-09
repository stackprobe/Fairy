using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameJammer
	{
		public static byte[] Encode(byte[] data)
		{
			data = ZipTools.Compress(data);
			MaskGZData(data);
			return data;
		}

		public static byte[] Decode(byte[] data)
		{
			MaskGZData(data);
			byte[] ret = ZipTools.Decompress(data);
			MaskGZData(data); // 復元
			return ret;
		}

		private static void MaskGZData(byte[] data)
		{
			if (data.Length != 0)
			{
				data[0] ^= (0x1f ^ 0x44);
				data[1] ^= (0x8b ^ 0x44);
			}
		}
	}
}
