using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class Common
	{
		public static T DesertElement<T>(List<T> list, int index)
		{
			T element = list[index];
			list.RemoveAt(index);
			return element;
		}

		public static T UnaddElement<T>(List<T> list)
		{
			return DesertElement(list, list.Count - 1);
		}
	}
}
