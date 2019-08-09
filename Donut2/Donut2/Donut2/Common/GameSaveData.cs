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

				lines.Add("" + GameInput.DIR_2.BtnId);
				lines.Add("" + GameInput.DIR_4.BtnId);
				lines.Add("" + GameInput.DIR_6.BtnId);
				lines.Add("" + GameInput.DIR_8.BtnId);
				lines.Add("" + GameInput.A.BtnId);
				lines.Add("" + GameInput.B.BtnId);
				lines.Add("" + GameInput.C.BtnId);
				lines.Add("" + GameInput.D.BtnId);
				lines.Add("" + GameInput.E.BtnId);
				lines.Add("" + GameInput.F.BtnId);
				lines.Add("" + GameInput.L.BtnId);
				lines.Add("" + GameInput.R.BtnId);
				lines.Add("" + GameInput.PAUSE.BtnId);
				lines.Add("" + GameInput.START.BtnId);

				lines.Add("" + GameInput.DIR_2.KeyId);
				lines.Add("" + GameInput.DIR_4.KeyId);
				lines.Add("" + GameInput.DIR_6.KeyId);
				lines.Add("" + GameInput.DIR_8.KeyId);
				lines.Add("" + GameInput.A.KeyId);
				lines.Add("" + GameInput.B.KeyId);
				lines.Add("" + GameInput.C.KeyId);
				lines.Add("" + GameInput.D.KeyId);
				lines.Add("" + GameInput.E.KeyId);
				lines.Add("" + GameInput.F.KeyId);
				lines.Add("" + GameInput.L.KeyId);
				lines.Add("" + GameInput.R.KeyId);
				lines.Add("" + GameInput.PAUSE.KeyId);
				lines.Add("" + GameInput.START.KeyId);

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

				GameInput.DIR_2.BtnId = int.Parse(lines[c++]);
				GameInput.DIR_4.BtnId = int.Parse(lines[c++]);
				GameInput.DIR_6.BtnId = int.Parse(lines[c++]);
				GameInput.DIR_8.BtnId = int.Parse(lines[c++]);
				GameInput.A.BtnId = int.Parse(lines[c++]);
				GameInput.B.BtnId = int.Parse(lines[c++]);
				GameInput.C.BtnId = int.Parse(lines[c++]);
				GameInput.D.BtnId = int.Parse(lines[c++]);
				GameInput.E.BtnId = int.Parse(lines[c++]);
				GameInput.F.BtnId = int.Parse(lines[c++]);
				GameInput.L.BtnId = int.Parse(lines[c++]);
				GameInput.R.BtnId = int.Parse(lines[c++]);
				GameInput.PAUSE.BtnId = int.Parse(lines[c++]);
				GameInput.START.BtnId = int.Parse(lines[c++]);

				GameInput.DIR_2.KeyId = int.Parse(lines[c++]);
				GameInput.DIR_4.KeyId = int.Parse(lines[c++]);
				GameInput.DIR_6.KeyId = int.Parse(lines[c++]);
				GameInput.DIR_8.KeyId = int.Parse(lines[c++]);
				GameInput.A.KeyId = int.Parse(lines[c++]);
				GameInput.B.KeyId = int.Parse(lines[c++]);
				GameInput.C.KeyId = int.Parse(lines[c++]);
				GameInput.D.KeyId = int.Parse(lines[c++]);
				GameInput.E.KeyId = int.Parse(lines[c++]);
				GameInput.F.KeyId = int.Parse(lines[c++]);
				GameInput.L.KeyId = int.Parse(lines[c++]);
				GameInput.R.KeyId = int.Parse(lines[c++]);
				GameInput.PAUSE.KeyId = int.Parse(lines[c++]);
				GameInput.START.KeyId = int.Parse(lines[c++]);

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
