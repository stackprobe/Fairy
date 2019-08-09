using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public class GameIEnumerableTask : IGameTask
	{
		private IEnumerator<bool> Sequencer;
		private Action EndRoutine;

		public GameIEnumerableTask(IEnumerable<bool> routine, Action endRoutine)
		{
			this.Sequencer = routine.GetEnumerator();
			this.EndRoutine = endRoutine;
		}

		public bool Routine()
		{
			if (this.Sequencer != null && (this.Sequencer.MoveNext() == false || this.Sequencer.Current == false))
			{
				this.Sequencer.Dispose();
				this.Sequencer = null;
			}
			return this.Sequencer != null;
		}

		public void Dispose()
		{
			if (this.EndRoutine != null)
			{
				if (this.Sequencer != null)
				{
					this.Sequencer.Dispose();
					this.Sequencer = null;
				}
				this.EndRoutine();
				this.EndRoutine = null;
			}
		}
	}
}
