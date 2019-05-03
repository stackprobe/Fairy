using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Charlotte.Donut
{
	public class GameHelper
	{
		public class EndProc : Exception
		{ }

		public class Error : Exception
		{
			public Error(string message = null)
				: base(message)
			{ }
		}

		public static void PinOn<T>(T data, Action<IntPtr> routine)
		{
			GCHandle pinnedData = GCHandle.Alloc(data, GCHandleType.Pinned);
			try
			{
				routine(pinnedData.AddrOfPinnedObject());
			}
			finally
			{
				pinnedData.Free();
			}
		}
	}
}
