using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class DDResource
	{
		// ResourceFile_01 ... ロードのみ
		// ResourceFile_02 ... ロードのみ(セーブ不可)

		// ResourceDir_01 ... ロードのみ
		// ResourceDir_02 ... ロード・セーブ

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static bool ReleaseMode;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class ResInfo
		{
			public string ResFile;
			public long Offset;
			public int Size;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static Dictionary<string, ResInfo> File2ResInfo = DictionaryTools.CreateIgnoreCase<ResInfo>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void INIT()
		{
			ReleaseMode = File.Exists(DDConsts.ResourceFile_01);

			if (ReleaseMode)
			{
				foreach (string resFile in new string[] { DDConsts.ResourceFile_01, DDConsts.ResourceFile_02 })
				{
					List<ResInfo> resInfos = new List<ResInfo>();

					using (FileStream reader = new FileStream(resFile, FileMode.Open, FileAccess.Read))
					{
						while (reader.Position < reader.Length)
						{
							int size = BinTools.ToInt(FileTools.Read(reader, 4));

							if (size < 0)
								throw new DDError();

							resInfos.Add(new ResInfo()
							{
								ResFile = resFile,
								Offset = reader.Position,
								Size = size,
							});

							reader.Seek((long)size, SeekOrigin.Current);
						}
					}
					string[] files = FileTools.TextToLines(StringTools.ENCODING_SJIS.GetString(LoadFile(resInfos[0])));

					if (files.Length != resInfos.Count)
						throw new DDError(files.Length + ", " + resInfos.Count);

					for (int index = 1; index < files.Length; index++)
					{
						string file = files[index];

						if (File2ResInfo.ContainsKey(file))
							throw new DDError(file);

						File2ResInfo.Add(file, resInfos[index]);
					}
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static byte[] LoadFile(string resFile, long offset, int size)
		{
			using (FileStream reader = new FileStream(resFile, FileMode.Open, FileAccess.Read))
			{
				reader.Seek(offset, SeekOrigin.Begin);

				return DDJammer.Decode(FileTools.Read(reader, size));
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static byte[] LoadFile(ResInfo resInfo)
		{
			return LoadFile(resInfo.ResFile, resInfo.Offset, resInfo.Size);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] Load(string file)
		{
			if (ReleaseMode)
			{
				return LoadFile(File2ResInfo[file]);
			}
			else
			{
				string datFile = Path.Combine(DDConsts.ResourceDir_01, file);

				if (File.Exists(datFile) == false)
				{
					datFile = Path.Combine(DDConsts.ResourceDir_02, file);

					if (File.Exists(datFile) == false)
						throw new Exception(datFile);
				}
				return File.ReadAllBytes(datFile);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Save(string file, byte[] fileData)
		{
			if (ReleaseMode)
			{
				throw new DDError();
			}
			else
			{
				File.WriteAllBytes(Path.Combine(DDConsts.ResourceDir_02, file), fileData);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// <para>ファイルリストを取得する。</para>
		/// <para>ソート済み</para>
		/// <para>'_' で始まるファイルの除去済み</para>
		/// </summary>
		/// <returns>ファイルリスト</returns>
		public static IEnumerable<string> GetFiles()
		{
			IEnumerable<string> files;

			if (ReleaseMode)
			{
				files = File2ResInfo.Keys;
			}
			else
			{
				files = EnumerableTools.Join(new IEnumerable<string>[]
				{
					Directory.GetFiles(DDConsts.ResourceDir_01, "*", SearchOption.AllDirectories).Select(file => FileTools.ChangeRoot(file, DDConsts.ResourceDir_01)),
					Directory.GetFiles(DDConsts.ResourceDir_02, "*", SearchOption.AllDirectories).Select(file => FileTools.ChangeRoot(file, DDConsts.ResourceDir_02)),
				});

				// '_' で始まるファイルの除去
				// makeDDResourceFile は '_' で始まるファイルを含めない。
				files = files.Where(file => Path.GetFileName(file)[0] != '_');
			}

			// ソート
			// makeDDResourceFile はファイルリストを sortJLinesICase している。
			// ここでソートする必要は無いが、戻り値に統一性を持たせるため(毎回ファイルの並びが違うということのないように)ソートしておく。
			files = EnumerableTools.Sort(files, StringTools.CompIgnoreCase);

			return files;
		}
	}
}
