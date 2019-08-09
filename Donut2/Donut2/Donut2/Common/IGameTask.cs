using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public interface IGameTask : IDisposable
	{
		bool Routine();
	}
}
