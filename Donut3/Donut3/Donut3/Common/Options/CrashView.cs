using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Common.Options
{
	public class CrashView : IDisposable
	{
		private static readonly I3Color DefaultColor = new I3Color(0, 255, 255);
		private const double POINT_WH = 4.0;

		private DDSubScreen MyScreen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);

		public CrashView()
		{
			using (this.MyScreen.Section())
			{
				DDCurtain.DrawCurtain();
			}
		}

		public void Draw(Crash crash)
		{
			Draw(new Crash[] { crash });
		}

		public void Draw(Crash crash, I3Color color)
		{
			Draw(new Crash[] { crash }, color);
		}

		public void Draw(IEnumerable<Crash> crashes)
		{
			Draw(crashes, DefaultColor);
		}

		public void Draw(IEnumerable<Crash> crashes, I3Color color)
		{
			DDDraw.SetBright(color);

			using (this.MyScreen.Section())
			{
				Queue<IEnumerable<Crash>> q = new Queue<IEnumerable<Crash>>();

				q.Enqueue(crashes);

				while (1 <= q.Count)
				{
					foreach (Crash crash in q.Dequeue())
					{
						switch (crash.Kind)
						{
							case CrashUtils.Kind_e.NONE:
								break;

							case CrashUtils.Kind_e.POINT:
								DDDraw.DrawBegin(DDGround.GeneralResource.WhiteBox, crash.Pt.X - DDGround.ICamera.X, crash.Pt.Y - DDGround.ICamera.Y);
								DDDraw.DrawSetSize(POINT_WH, POINT_WH);
								DDDraw.DrawEnd();
								break;

							case CrashUtils.Kind_e.CIRCLE:
								DDDraw.DrawBegin(DDGround.GeneralResource.WhiteCircle, crash.Pt.X - DDGround.ICamera.X, crash.Pt.Y - DDGround.ICamera.Y);
								DDDraw.DrawSetSize(crash.R * 2.0, crash.R * 2.0);
								DDDraw.DrawEnd();
								break;

							case CrashUtils.Kind_e.RECT:
								DDDraw.DrawRect(
									DDGround.GeneralResource.WhiteBox,
									crash.Rect.L - DDGround.ICamera.X,
									crash.Rect.T - DDGround.ICamera.Y,
									crash.Rect.W,
									crash.Rect.H
									);
								break;

							case CrashUtils.Kind_e.MULTI:
								q.Enqueue(crash.Cs);
								break;

							default:
								throw null; // never
						}
					}
				}
			}
			DDDraw.Reset();
		}

		public DDPicture GetPicture()
		{
			return this.MyScreen.ToPicture();
		}

		public void DrawToScreen(double a = 0.3)
		{
			DDDraw.SetAlpha(a);
			DDDraw.DrawSimple(this.GetPicture(), 0, 0);
			DDDraw.Reset();
		}

		public void Dispose()
		{
			if (this.MyScreen != null)
			{
				this.MyScreen.Dispose();
				this.MyScreen = null;
			}
		}
	}
}
