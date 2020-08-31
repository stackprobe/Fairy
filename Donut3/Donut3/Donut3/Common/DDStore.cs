using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class DDStore<T>
	{
		private Func<T> NewObjectGetter;
		private Stack<T> UsedObjectStack = new Stack<T>();

		public DDStore(Func<T> getter)
		{
			this.NewObjectGetter = getter;
		}

		public void Give(T usedObjcet)
		{
			if (usedObjcet == null)
				throw new DDError();

			this.UsedObjectStack.Push(usedObjcet);
		}

		public T Take()
		{
			if (1 <= this.UsedObjectStack.Count)
				return this.UsedObjectStack.Pop();
			else
				return this.NewObjectGetter();
		}
	}
}
