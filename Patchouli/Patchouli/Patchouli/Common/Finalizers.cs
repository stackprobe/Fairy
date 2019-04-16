using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class Finalizers
	{
		private List<Action> _finalizers = new List<Action>();

		public void AddFunc(Action routine)
		{
			_finalizers.Add(routine);
		}

		public void RemoveFunc(Action routine)
		{
			for (int index = _finalizers.Count - 1; 0 <= index; index--) // LIFO
			{
				if (_finalizers[index] == routine)
				{
					_finalizers.RemoveAt(index);
					break;
				}
			}
		}

		public void Flush()
		{
			while (1 <= _finalizers.Count)
			{
				Common.UnaddElement(_finalizers)();
			}
		}
	}
}
