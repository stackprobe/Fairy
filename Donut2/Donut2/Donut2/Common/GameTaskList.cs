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

		public void ExecuteAllTask()
		{
			for (int index = 0; index < this.Tasks.Count; index++)
			{
				IGameTask task = this.Tasks[index];

				if (task.Routine() == false) // ? 終了
				{
					task.Dispose();
					ExtraTools.FastDesertElement(this.Tasks, index--);
				}
			}
		}

		public void Clear()
		{
			for (int index = 0; index < this.Tasks.Count; index++)
			{
				this.Tasks[index].Dispose();
			}
			this.Tasks.Clear();
		}
	}
}
