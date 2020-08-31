using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Test01
{
	public class GameMain
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
			DDCurtain.DrawCurtain();
			DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, 100, 100, DDConsts.Screen_W - 200, DDConsts.Screen_H - 200);
		}

		public void Perform()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			Ground.I.Music.Filed_01.Play();

			long frmProcMilAvgNumer = 0L;
			long frmProcMilAvgDenom = 0L;

			int effectPerFrm = 10;
			int effectCount = 1;

			for (; ; )
			{
				if (DDInput.PAUSE.IsPound())
				{
					break;
				}
				if (DDInput.A.IsPound())
				{
					effectPerFrm--;
				}
				if (DDInput.B.IsPound())
				{
					effectPerFrm++;
				}
				if (DDInput.C.IsPound())
				{
					effectCount++;
				}
				if (DDInput.D.IsPound())
				{
					effectCount--;
				}
				effectPerFrm = IntTools.ToRange(effectPerFrm, 1, 10);
				effectCount = IntTools.ToRange(effectCount, 1, 100);

				if (DDEngine.ProcFrame % effectPerFrm == 0)
				{
					for (int c = 0; c < effectCount; c++)
					{
						new DDCommonEffect(DDGround.GeneralResource.Dummy)
						{
							X = 400.0,
							Y = 300.0,
							Z = 0.5,
							XAdd2 = Math.Cos(DDEngine.ProcFrame / 100.0 + c) * 0.1,
							YAdd2 = Math.Sin(DDEngine.ProcFrame / 100.0 + c) * 0.1,
						}
						.Fire();
					}
				}
				this.DrawWall();



				// フォントのテスト
				DDFontUtils.DrawString_XCenter(
					400, 20,
					"げんかいみんちょう",
					DDFontUtils.GetFont("源界明朝", 70, 6, true, 2)
					);
				DDFontUtils.DrawString_XCenter(
					400, 520,
					"りいてがき",
					DDFontUtils.GetFont("りいてがき筆", 50, 6, true, 2)
					);



				frmProcMilAvgNumer += DDEngine.FrameProcessingMillis;
				frmProcMilAvgDenom++;

				double frmProcMilAvg = (double)frmProcMilAvgNumer / frmProcMilAvgDenom;

				if (DDEngine.ProcFrame % 100 == 0)
				{
					frmProcMilAvgNumer /= 2;
					frmProcMilAvgDenom /= 2;
				}

				DDDraw.SetAlpha(0.5);
				DDDraw.SetBright(new I3Color(0, 0, 0));
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, 0, 0, DDConsts.Screen_W, 16);
				DDDraw.Reset();

				DDPrint.SetPrint();
				DDPrint.SetColor(new I3Color(255, 128, 0));
				DDPrint.Print(string.Format(
					"FST={0},HCT={1},FPM={2},FPW={3},FPMA={4:F3}(EPF={5},EC={6},ELC={7})"
					, DDEngine.FrameStartTime
					, DDEngine.HzChaserTime
					, DDEngine.FrameProcessingMillis
					, DDEngine.FrameProcessingMillis_Worst
					, frmProcMilAvg
					, effectPerFrm
					, effectCount
					, DDGround.EL.Count
					));
				DDPrint.Reset();



				DDEngine.EachFrame();
			}
			DDEngine.FreezeInput();
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawWall();
				DDEngine.EachFrame();
			}
			DDGround.EL.Clear();
		}
	}
}
