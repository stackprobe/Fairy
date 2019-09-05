using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class DDTaskList
	{
		private DDList<IDDTask> Tasks = new DDList<IDDTask>();

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

					this.Tasks[index] = null;
					//this.Tasks.FastRemoveAt(index--); // old
				}
			}
			//this.Tasks.FastRemoveAll(task => task == null);
			this.Tasks.RemoveAll(task => task == null);
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
