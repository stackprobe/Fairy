using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	public class TaskList : IDisposable
	{
		public class Info
		{
			public Func<object, bool> Routine;
			public object Param;
			public Action<object> ReleaseParam;
		}

		private List<Info> Infos = new List<Info>();
		private int LastFrame = -1;

		public void AddTask(Info ti)
		{
			this.Infos.Add(ti);
		}

		public void AddTopTask(Info ti)
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
				Info ti = this.Infos[index];

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
				Info ti = ExtraTools.UnaddElement(this.Infos);

				if (ti.ReleaseParam != null)
					ti.ReleaseParam(ti.Param);
			}
		}

		public void Dispose()
		{
			this.Clear();
		}
	}
}
