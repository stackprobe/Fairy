using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class DDObjectStock<T> where T : class
	{
		private Func<T> CreateObject;
		private Queue<T> Stock = new Queue<T>();

		public DDObjectStock(Func<T> createObject)
		{
			this.CreateObject = createObject;
		}

		public void Give(T waste)
		{
			this.Stock.Enqueue(waste);
		}

		public T Take()
		{
			T ret;

			if (this.Stock.Count == 0)
				ret = this.CreateObject();
			else
				ret = this.Stock.Dequeue();

			return ret;
		}
	}
}
