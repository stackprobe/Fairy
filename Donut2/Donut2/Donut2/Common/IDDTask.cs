using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public interface IDDTask : IDisposable
	{
		bool Routine();
	}
}
