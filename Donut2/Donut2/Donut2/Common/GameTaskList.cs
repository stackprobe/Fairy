using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class GameTaskList
	{
		private List<IGameTask> Tasks = new List<IGameTask>();

		public void Add(IGameTask task)
		{
			this.Tasks.Add(task);
		}

		public void Fire()
		{
			for (int index = 0; index < this.Tasks.Count; index++)
			{
				IGameTask task = this.Tasks[index];

				if (task.Routine() == false)
				{
					task.Destroy();
					ExtraTools.FastDesertElement(this.Tasks, index--);
				}
			}
		}

		public void Clear()
		{
			for (int index = 0; index < this.Tasks.Count; index++)
			{
				this.Tasks[index].Destroy();
			}
			this.Tasks.Clear();
		}
	}
}
