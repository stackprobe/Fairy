using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public class OneObject<T>
	{
		private Func<T> Getter;

		public OneObject(Func<T> getter)
		{
			this.Getter = getter;
		}

		private bool Loaded = false;
		private T Value;

		public T I
		{
			get
			{
				if (this.Loaded == false)
				{
					this.Value = this.Getter();
					this.Loaded = true;
				}
				return this.Value;
			}
		}
	}
}
