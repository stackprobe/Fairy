using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class OneObject<T>
	{
		private Func<T> _getter;

		public OneObject(Func<T> getter)
		{
			_getter = getter;
		}

		private T[] _i = null;

		public T I
		{
			get
			{
				if (_i == null)
					_i = new T[] { _getter() };

				return _i[0];
			}
		}
	}
}
