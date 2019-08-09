using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	/// <summary>
	/// その他の機能の寄せ集め、そのうち DxLib に関係無いもの。関係有るものは GameSystem へ
	/// </summary>
	public static class GameUtils
	{
		public static byte[] SplitableJoin(string[] lines)
		{
			return BinTools.Join(lines.Select(line => Encoding.UTF8.GetBytes(line)).ToArray());
		}

		public static string[] Split(byte[] data)
		{
			return BinTools.Split(data).Select(bLine => Encoding.UTF8.GetString(bLine)).ToArray();
		}

		public static void Noop(params object[] dummyPrms)
		{
			// noop
		}

		public static T FastDesertElement<T>(List<T> list, Predicate<T> match, T defval = default(T))
		{
			for (int index = 0; index < list.Count; index++)
				if (match(list[index]))
					return ExtraTools.FastDesertElement(list, index);

			return defval;
		}

		public static void CountDown(ref int count)
		{
			if (count < 0)
				count++;
			else if (0 < count)
				count--;
		}

		public static void Approach(ref double value, double target, double rate)
		{
			value -= target;
			value *= rate;
			value += target;
		}
	}
}
