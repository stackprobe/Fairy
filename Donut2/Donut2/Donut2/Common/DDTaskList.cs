using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class DDTaskList
	{
		private List<IDDTask> Tasks = new List<IDDTask>();

		public void Add(IDDTask task)
		{
			this.Tasks.Add(task);
		}

		public void ExecuteAllTask()
		{
			for (int index = 0; index < this.Tasks.Count; index++)
			{
				IDDTask task = this.Tasks[index];

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

		public int Count
		{
			get
			{
				return this.Tasks.Count;
			}
		}
	}
}
