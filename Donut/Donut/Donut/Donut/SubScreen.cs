using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class SubScreen : IDisposable
	{
		private static List<SubScreen> SubScreenList = new List<SubScreen>();

		public int W;
		public int H;
		public bool AFlag;
		public int Handle;

		public SubScreen(int w, int h, bool aFlag = false) // Codevil の CreateSubScreen()
		{
			if (
				w < 1 || IntTools.IMAX < w ||
				h < 1 || IntTools.IMAX < h
				)
				throw new GameError();

			this.W = w;
			this.H = h;
			this.AFlag = aFlag;
			this.Handle = -1;

			SubScreenList.Add(this);
		}

		public void Dispose() // Codevil の ReleaseSubScreen()
		{
			throw new NotImplementedException();
		}

		public int GetHandle()
		{
			if (this.Handle == -1)
			{
				this.Handle = DX.MakeScreen(this.W, this.H, this.AFlag ? 1 : 0);

				if (this.Handle == -1)
					throw new GameError();
			}
			return this.Handle;
		}

		public I2Size GetSize() // Codevil の GetSubScreenSize()
		{
			return new I2Size()
			{
				W = this.W,
				H = this.H,
			};
		}

		// ここから static

		public static int CurrDrawScreenHandle = DX.DX_SCREEN_BACK;

		public static void ChangeDrawScreen(int handle)
		{
			if (DX.SetDrawScreen(handle) != 0)
				throw new GameError();

			CurrDrawScreenHandle = handle;
		}

		public static void ChangeDrawScreen(SubScreen screen)
		{
			ChangeDrawScreen(screen.GetHandle());
		}

		public static void RestoreDrawScreen()
		{
			ChangeDrawScreen(GameGround.I.MainScreen != null ? GameGround.I.MainScreen.GetHandle() : DX.DX_SCREEN_BACK);
		}

		public static void UnloadAllSubScreenHandle()
		{
			ChangeDrawScreen(DX.DX_SCREEN_BACK); // これから開放するハンドルであるとマズいので...

			for (int index = 0; index < SubScreenList.Count; index++)
			{
				SubScreen i = SubScreenList[index];

				if (i.Handle != -1)
				{
					if (DX.DeleteGraph(i.Handle) != 0) // ? 失敗
						throw new GameError();

					i.Handle = -1;
				}
			}
		}
	}
}
