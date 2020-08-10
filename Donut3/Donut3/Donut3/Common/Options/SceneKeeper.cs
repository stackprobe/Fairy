using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Common.Options
{
	public class SceneKeeper
	{
		private int FrameMax;
		private int StartedProcFrame = -1;

		public SceneKeeper(int frameMax)
		{
			if (frameMax < 1 || IntTools.IMAX < frameMax)
				throw new DDError();

			this.FrameMax = frameMax;
		}

		public void Fire()
		{
			this.StartedProcFrame = DDEngine.ProcFrame;
		}

		public void FireDelay(int delay = 1)
		{
			if (delay < 0 || IntTools.IMAX < delay)
				throw new DDError();

			this.StartedProcFrame = DDEngine.ProcFrame + delay;
		}

		public void Clear()
		{
			this.StartedProcFrame = -1;
		}

		public bool IsJustFired()
		{
			return this.StartedProcFrame == DDEngine.ProcFrame;
		}

		public bool IsFlaming()
		{
			return
				this.StartedProcFrame != -1 &&
				this.StartedProcFrame <= DDEngine.ProcFrame &&
				DDEngine.ProcFrame <= this.StartedProcFrame + this.FrameMax;
		}

		public int Count
		{
			get
			{
				if (this.IsFlaming() == false)
					throw new DDError();

				return DDEngine.ProcFrame - this.StartedProcFrame;
			}
		}

		public DDScene GetScene()
		{
			if (this.IsFlaming() == false)
				return new DDScene(-1, 0);

			return new DDScene(DDEngine.ProcFrame - this.StartedProcFrame, this.FrameMax);
		}
	}
}
