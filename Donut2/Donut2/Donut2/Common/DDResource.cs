using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class DDResource
	{
		public static bool ReleaseMode;

		public class ResInfo
		{
			public long Offset;
			public int Size;
		}

		public static Dictionary<string, ResInfo> File2ResInfo = DictionaryTools.CreateIgnoreCase<ResInfo>();

		public static void INIT()
		{
			ReleaseMode = File.Exists(DDConsts.ResourceFile);

			if (ReleaseMode)
			{
				List<ResInfo> resInfos = new List<ResInfo>();

				using (FileStream reader = new FileStream(DDConsts.ResourceFile, FileMode.Open, FileAccess.Read))
				{
					while (reader.Position < reader.Length)
					{
						int size = BinTools.ToInt(FileTools.Read(reader, 4));

						if (size < 0)
							throw new DDError();

						resInfos.Add(new ResInfo()
						{
							Offset = reader.Position,
							Size = size,
						});

						reader.Seek((long)size, SeekOrigin.Current);
					}
				}
				string[] files = FileTools.TextToLines(StringTools.ENCODING_SJIS.GetString(LoadFile(resInfos[0])));

				if (files.Length != resInfos.Count)
					throw new DDError(files.Length + ", " + resInfos.Count);

				for (int index = 0; index < files.Length; index++)
					File2ResInfo.Add(files[index], resInfos[index]);
			}
		}

		private static byte[] LoadFile(long offset, int size)
		{
			using (FileStream reader = new FileStream(DDConsts.ResourceFile, FileMode.Open, FileAccess.Read))
			{
				reader.Seek(offset, SeekOrigin.Begin);

				return DDJammer.Decode(FileTools.Read(reader, size));
			}
		}

		private static byte[] LoadFile(ResInfo resInfo)
		{
			return LoadFile(resInfo.Offset, resInfo.Size);
		}

		public static byte[] Load(string file)
		{
			if (ReleaseMode)
			{
				return LoadFile(File2ResInfo[file]);
			}
			else
			{
				return File.ReadAllBytes(Path.Combine(DDConsts.ResourceDir, file));
			}
		}

		public static void Save(string file, byte[] fileData)
		{
			if (ReleaseMode)
			{
				throw new DDError();
			}
			else
			{
				File.WriteAllBytes(Path.Combine(DDConsts.ResourceDir, file), fileData);
			}
		}
	}
}
