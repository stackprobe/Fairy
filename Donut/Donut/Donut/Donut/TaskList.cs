using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public class TaskList : IDisposable
	{
		public class TaskInfo
		{
			public Func<object, bool> Routine;
			public object Param;
			public Action<object> ReleaseParam;
		}

		private List<TaskInfo> Infos = new List<TaskInfo>();
		private int LastFrame = -1;

		public void AddTask(TaskInfo ti)
		{
			this.Infos.Add(ti);
		}

		public void AddTopTask(TaskInfo ti)
		{
			this.Infos.Insert(0, ti);
		}

		public void ExecuteAllTask(bool oncePerFrame = true)
		{
			if (oncePerFrame)
			{
				if (GameEngine.ProcFrame <= this.LastFrame)
					return;

				this.LastFrame = GameEngine.ProcFrame;
			}
			bool dead = false;

			for (int index = 0; index < this.Infos.Count; index++)
			{
				TaskInfo ti = this.Infos[index];

				if (ti.Routine(ti.Param) == false)
				{
					ti = this.Infos[index]; // Routine() でこの tl に追加される場合を想定

					if (ti.ReleaseParam != null)
						ti.ReleaseParam(ti.Param);

					ti.Routine = null;
					dead = true;
				}
			}
			if (dead)
				this.Infos.RemoveAll(ti => ti.Routine == null);
		}

		public void Clear()
		{
			while (1 <= this.Infos.Count)
			{
				TaskInfo ti = ExtraTools.UnaddElement(this.Infos);

				if (ti.ReleaseParam != null)
					ti.ReleaseParam(ti.Param);
			}
		}

		public void Dispose()
		{
			this.Clear();
		}

		public static void AddTask_NT(TaskList tl, bool topMode, Func<object, bool> tf, object tp = null, Action<object> tr = null)
		{
			if (tl == null)
				throw new GameError();

			if (tf == null)
				throw new GameError();

			TaskInfo ti = new TaskInfo();

			ti.Routine = tf;
			ti.Param = tp;
			ti.ReleaseParam = tr;

			if (topMode)
				tl.AddTopTask(ti);
			else
				tl.AddTask(ti);
		}

		public static void AddTask<T>(TaskList tl, bool topMode, Func<T, bool> tf, T tp = null, Action<T> tr = null) where T : class
		{
			AddTask_NT(tl, topMode, p => tf((T)p), tp, p => tr((T)p));
		}
	}
}
