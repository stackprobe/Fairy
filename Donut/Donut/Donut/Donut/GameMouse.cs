using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Donut
{
	public class GameMouse
	{
		public static GameMouse I = null;

		public int MouseRot = 0;

		public enum MOUBTN
		{
			L,
			M,
			R,

			MAX, // num of MOUBTN.*
		};

		private int[] MouseStatus = new int[(int)MOUBTN.MAX];

		public void MouseEachFrame()
		{
			uint status;

			if (GameEngine.WindowIsActive)
			{
				MouseRot = DX.GetMouseHWheelRotVol();
				status = (uint)DX.GetMouseInput();
			}
			else // ? 非アクティブ -> 無入力
			{
				MouseRot = 0;
				status = 0u;
			}
			MouseRot = IntTools.ToRange(MouseRot, -IntTools.IMAX, IntTools.IMAX);

			GameDefine.UpdateInput(ref MouseStatus[(int)MOUBTN.L], (status & DX.MOUSE_INPUT_LEFT) != 0u);
			GameDefine.UpdateInput(ref MouseStatus[(int)MOUBTN.M], (status & DX.MOUSE_INPUT_MIDDLE) != 0u);
			GameDefine.UpdateInput(ref MouseStatus[(int)MOUBTN.R], (status & DX.MOUSE_INPUT_RIGHT) != 0u);
		}

		public int GetMouInput(MOUBTN mouBtnId)
		{
			return GameEngine.FreezeInputFrame != 0 ? 0 : MouseStatus[(int)mouBtnId];
		}

		public bool GetMouPound(MOUBTN mouBtnId)
		{
			return GameToolkit2.IsPound(GetMouInput(mouBtnId));
		}

		public int MouseX = GameGround.I.ScreenCenterX;
		public int MouseY = GameGround.I.ScreenCenterY;

		public void UpdateMousePos()
		{
			if (DX.GetMousePoint(out MouseX, out MouseY) != 0) // ? 失敗
				throw new GameError();

			MouseX *= GameGround.I.ScreenSize.W;
			MouseX /= GameGround.I.RealScreenSize.W;
			MouseY *= GameGround.I.ScreenSize.H;
			MouseY /= GameGround.I.RealScreenSize.H;
		}

		public void ApplyMousePos()
		{
			int mx = MouseX;
			int my = MouseY;

			mx *= GameGround.I.RealScreenSize.W;
			mx /= GameGround.I.ScreenSize.W;
			my *= GameGround.I.RealScreenSize.H;
			my /= GameGround.I.ScreenSize.H;

			if (DX.SetMousePoint(mx, my) != 0) // ? 失敗
				throw new GameError();
		}

		public int MouseMoveX;
		public int MouseMoveY;

		private int UMM_LastFrame = -IntTools.IMAX;
		private int UMM_CenterX = GameGround.I.ScreenCenterX;
		private int UMM_CenterY = GameGround.I.ScreenCenterY;

		public void UpdateMouseMove()
		{
			if (GameEngine.ProcFrame <= UMM_LastFrame) // ? 2回以上更新した。
				throw new GameError();

			UpdateMousePos();

			MouseMoveX = MouseX - UMM_CenterX;
			MouseMoveY = MouseY - UMM_CenterY;

			MouseX = UMM_CenterX;
			MouseY = UMM_CenterY;

			ApplyMousePos();

			if (UMM_LastFrame + 1 < GameEngine.ProcFrame) // ? 1フレーム以上更新しなかった。
			{
				MouseMoveX = 0;
				MouseMoveY = 0;
			}
			UMM_LastFrame = GameEngine.ProcFrame;
		}
	}
}
