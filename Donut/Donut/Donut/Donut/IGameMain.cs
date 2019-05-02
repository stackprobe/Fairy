using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Donut
{
	public interface IGameMain
	{
		void Init();
		void Main();
		IntPtr GetIcon();
	}
}
