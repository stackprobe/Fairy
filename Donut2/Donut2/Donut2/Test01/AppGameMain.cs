using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Test01
{
	public class AppGameMain
	{
		public void INIT()
		{
			// noop
		}

		public void FNLZ()
		{
			// noop
		}

		private void DrawWall()
		{
			GameCurtain.DrawCurtain();
			GameDraw.DrawRect(GameGround.GeneralResource.WhiteBox, 100, 100, GameConsts.Screen_W - 200, GameConsts.Screen_H - 200);
		}

		public void Perform()
		{
			GameCurtain.SetCurtain();
			GameEngine.FreezeInput();

			Ground.I.Music.Filed_01.Play();

			long frmProcMilAvgNumer = 0L;
			long frmProcMilAvgDenom = 0L;

			int effectPerFrm = 10;
			int effectCount = 1;

			for (; ; )
			{
				if (GameInput.PAUSE.IsPound())
				{
					break;
				}
				if (GameInput.A.IsPound())
				{
					effectPerFrm--;
				}
				if (GameInput.B.IsPound())
				{
					effectPerFrm++;
				}
				if (GameInput.C.IsPound())
				{
					effectCount++;
				}
				if (GameInput.D.IsPound())
				{
					effectCount--;
				}
				effectPerFrm = IntTools.Range(effectPerFrm, 1, 10);
				effectCount = IntTools.Range(effectCount, 1, 100);

				if (GameEngine.ProcFrame % effectPerFrm == 0)
				{
					for (int c = 0; c < effectCount; c++)
					{
						new GameCommonEffect(GameGround.GeneralResource.Dummy)
						{
							X = 400.0,
							Y = 300.0,
							Z = 0.5,
							XAdd2 = Math.Cos(GameEngine.ProcFrame / 100.0 + c) * 0.1,
							YAdd2 = Math.Sin(GameEngine.ProcFrame / 100.0 + c) * 0.1,
						}
						.Fire();
					}
				}
				this.DrawWall();



				// フォントのテスト
				GameFontUtils.DrawString(
					10, 20,
					"げんかいみんちょう",
					GameFontUtils.GetFont("源界明朝", 70, 6, true, 2)
					);
				GameFontUtils.DrawString(
					10, 550,
					"りいてがき",
					GameFontUtils.GetFont("りいてがき筆", 50, 60, true, 2)
					);



				frmProcMilAvgNumer += GameEngine.FrameProcessingMillis;
				frmProcMilAvgDenom++;

				double frmProcMilAvg = (double)frmProcMilAvgNumer / frmProcMilAvgDenom;

				if (GameEngine.ProcFrame % 100 == 0)
				{
					frmProcMilAvgNumer /= 2;
					frmProcMilAvgDenom /= 2;
				}

				GamePrint.SetPrint();
				GamePrint.SetColor(new I3Color(255, 128, 0));
				GamePrint.Print(string.Format(
					"FST={0},LT={1},FPM={2},FPMA={3:F3}(EPF={4},EC={5})"
					, GameEngine.FrameStartTime
					, GameEngine.LangolierTime
					, GameEngine.FrameProcessingMillis
					, frmProcMilAvg
					, effectPerFrm
					, effectCount 
					));
				GamePrint.Reset();



				GameEngine.EachFrame();
			}
			GameEngine.FreezeInput();
			GameMusicUtils.Fade();
			GameCurtain.SetCurtain(30, -1.0);

			foreach (GameScene scene in GameSceneUtils.Create(40))
			{
				this.DrawWall();
				GameEngine.EachFrame();
			}
			GameGround.EL.Clear();
		}
	}
}
