﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using DxLibDLL;
using Charlotte.Tools;
using Charlotte.Common.MaskGZDataUtils;

namespace Charlotte.Common
{
	public static class DDJammer
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
			//MaskGZData(data); // 復元
			return ret;
		}

		private static void MaskGZData(byte[] data)
		{
			// $_git:secretBegin
//
			////////////////////////////////////////////////
//
			// $_git:secretEnd
		}
	}
}
