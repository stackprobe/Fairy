using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Common
{
	public static class GameSaveData
	{
		public static void Save()
		{
			List<byte[]> blocks = new List<byte[]>();

			// for Donut2
			{
				List<string> lines = new List<string>();

				lines.Add("" + GameGround.RealScreen_W);
				lines.Add("" + GameGround.RealScreen_H);

				lines.Add("" + GameGround.RealScreenDraw_L);
				lines.Add("" + GameGround.RealScreenDraw_T);
				lines.Add("" + GameGround.RealScreenDraw_W);
				lines.Add("" + GameGround.RealScreenDraw_H);

				lines.Add("" + DoubleTools.ToLong(GameGround.MusicVolume * IntTools.IMAX));
				lines.Add("" + DoubleTools.ToLong(GameGround.SEVolume * IntTools.IMAX));

				lines.Add("" + GameInput.DIR_2.PadBtnId);
				lines.Add("" + GameInput.DIR_4.PadBtnId);
				lines.Add("" + GameInput.DIR_6.PadBtnId);
				lines.Add("" + GameInput.DIR_8.PadBtnId);
				lines.Add("" + GameInput.A.PadBtnId);
				lines.Add("" + GameInput.B.PadBtnId);
				lines.Add("" + GameInput.C.PadBtnId);
				lines.Add("" + GameInput.D.PadBtnId);
				lines.Add("" + GameInput.E.PadBtnId);
				lines.Add("" + GameInput.F.PadBtnId);
				lines.Add("" + GameInput.L.PadBtnId);
				lines.Add("" + GameInput.R.PadBtnId);
				lines.Add("" + GameInput.PAUSE.PadBtnId);
				lines.Add("" + GameInput.START.PadBtnId);

				lines.Add("" + GameInput.DIR_2.KbdKeyId);
				lines.Add("" + GameInput.DIR_4.KbdKeyId);
				lines.Add("" + GameInput.DIR_6.KbdKeyId);
				lines.Add("" + GameInput.DIR_8.KbdKeyId);
				lines.Add("" + GameInput.A.KbdKeyId);
				lines.Add("" + GameInput.B.KbdKeyId);
				lines.Add("" + GameInput.C.KbdKeyId);
				lines.Add("" + GameInput.D.KbdKeyId);
				lines.Add("" + GameInput.E.KbdKeyId);
				lines.Add("" + GameInput.F.KbdKeyId);
				lines.Add("" + GameInput.L.KbdKeyId);
				lines.Add("" + GameInput.R.KbdKeyId);
				lines.Add("" + GameInput.PAUSE.KbdKeyId);
				lines.Add("" + GameInput.START.KbdKeyId);

				// 新しい項目をここへ追加...

				blocks.Add(GameUtils.SplitableJoin(lines.ToArray()));
			}

			// for app
			{
				List<string> lines = new List<string>();

				// app > @ Save

				lines.Add(DateTimeUnit.Now().ToString()); // Dummy

				// < app

				blocks.Add(GameUtils.SplitableJoin(lines.ToArray()));
			}

			File.WriteAllBytes(GameConsts.SaveDataFile, GameJammer.Encode(BinTools.SplittableJoin(blocks.ToArray())));
		}

		public static void Load()
		{
			if (File.Exists(GameConsts.SaveDataFile) == false)
				return;

			byte[][] blocks = BinTools.Split(GameJammer.Decode(File.ReadAllBytes(GameConsts.SaveDataFile)));
			int bc = 0;

			// 項目が増えた場合を想定して try ～ catch しておく。

			try // for Donut2
			{
				string[] lines = GameUtils.Split(blocks[bc++]);
				int c = 0;

				// TODO int.Parse -> IntTools.ToInt

				GameGround.RealScreen_W = int.Parse(lines[c++]);
				GameGround.RealScreen_H = int.Parse(lines[c++]);

				GameGround.RealScreenDraw_L = int.Parse(lines[c++]);
				GameGround.RealScreenDraw_T = int.Parse(lines[c++]);
				GameGround.RealScreenDraw_W = int.Parse(lines[c++]);
				GameGround.RealScreenDraw_H = int.Parse(lines[c++]);

				GameGround.MusicVolume = long.Parse(lines[c++]) / (double)IntTools.IMAX;
				GameGround.SEVolume = long.Parse(lines[c++]) / (double)IntTools.IMAX;

				GameInput.DIR_2.PadBtnId = int.Parse(lines[c++]);
				GameInput.DIR_4.PadBtnId = int.Parse(lines[c++]);
				GameInput.DIR_6.PadBtnId = int.Parse(lines[c++]);
				GameInput.DIR_8.PadBtnId = int.Parse(lines[c++]);
				GameInput.A.PadBtnId = int.Parse(lines[c++]);
				GameInput.B.PadBtnId = int.Parse(lines[c++]);
				GameInput.C.PadBtnId = int.Parse(lines[c++]);
				GameInput.D.PadBtnId = int.Parse(lines[c++]);
				GameInput.E.PadBtnId = int.Parse(lines[c++]);
				GameInput.F.PadBtnId = int.Parse(lines[c++]);
				GameInput.L.PadBtnId = int.Parse(lines[c++]);
				GameInput.R.PadBtnId = int.Parse(lines[c++]);
				GameInput.PAUSE.PadBtnId = int.Parse(lines[c++]);
				GameInput.START.PadBtnId = int.Parse(lines[c++]);

				GameInput.DIR_2.KbdKeyId = int.Parse(lines[c++]);
				GameInput.DIR_4.KbdKeyId = int.Parse(lines[c++]);
				GameInput.DIR_6.KbdKeyId = int.Parse(lines[c++]);
				GameInput.DIR_8.KbdKeyId = int.Parse(lines[c++]);
				GameInput.A.KbdKeyId = int.Parse(lines[c++]);
				GameInput.B.KbdKeyId = int.Parse(lines[c++]);
				GameInput.C.KbdKeyId = int.Parse(lines[c++]);
				GameInput.D.KbdKeyId = int.Parse(lines[c++]);
				GameInput.E.KbdKeyId = int.Parse(lines[c++]);
				GameInput.F.KbdKeyId = int.Parse(lines[c++]);
				GameInput.L.KbdKeyId = int.Parse(lines[c++]);
				GameInput.R.KbdKeyId = int.Parse(lines[c++]);
				GameInput.PAUSE.KbdKeyId = int.Parse(lines[c++]);
				GameInput.START.KbdKeyId = int.Parse(lines[c++]);

				// 新しい項目をここへ追加...
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}

			try // for app
			{
				string[] lines = GameUtils.Split(blocks[bc++]);
				int c = 0;

				// app > @ Load

				GameUtils.Noop(lines[c++]); // Dummy

				// < app
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}
	}
}
