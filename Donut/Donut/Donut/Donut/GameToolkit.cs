using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Donut
{
	/// <summary>
	/// Codevil の GameTools
	/// </summary>
	public static class GameToolkit
	{
		public class Scene
		{
			public int Numer;
			public int Denom;
			public double Rate;
		}

		public static IEnumerable<Scene> SceneLoop(int frameMax)
		{
			for (int count = 0; count < frameMax; count++)
			{
				yield return new Scene()
				{
					Numer = count,
					Denom = frameMax,
					Rate = count * 1.0 / frameMax,
				};
			}
		}

		// ---- Curtain ----

		private static Queue<double> CurtainQueue = new Queue<double>();

		public static double CurrCurtainWhiteLevel;
		public static int LastCurtainFrame = -1;

		public static void CurtainEachFrame(bool oncePerFrame = true)
		{
			if (oncePerFrame)
			{
				if (GameEngine.ProcFrame <= LastCurtainFrame)
					return;

				LastCurtainFrame = GameEngine.ProcFrame;
			}
		}

		public static void SetCurtain(int frameMax = 30, double destWhiteLevel = 0.0)
		{
			SetCurtain(frameMax, destWhiteLevel, CurrCurtainWhiteLevel);
		}

		public static void SetCurtain(int frameMax, double destWhiteLevel, double startWhiteLevel)
		{
			frameMax = IntTools.ToRange(frameMax, 0, 3600); // 0 frame - 1 min
			destWhiteLevel = DoubleTools.ToRange(destWhiteLevel, -1.0, 1.0);
			startWhiteLevel = DoubleTools.ToRange(startWhiteLevel, -1.0, 1.0);

			CurtainQueue.Clear();

			if (frameMax == 0)
			{
				CurtainQueue.Enqueue(destWhiteLevel);
			}
			for (int frmcnt = 0; frmcnt <= frameMax; frmcnt++)
			{
				double wl;

				if (frmcnt == 0)
					wl = startWhiteLevel;
				else if (frmcnt == frameMax)
					wl = destWhiteLevel;
				else
					wl = startWhiteLevel + (destWhiteLevel - startWhiteLevel) * ((double)frmcnt / frameMax);

				CurtainQueue.Enqueue(wl);
			}
		}

		// ----
	}
}
