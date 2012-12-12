using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx
{
	public static class CollectionExtension
	{
		/// <summary>
		/// 2次元ジャグ配列の行と列を入れ替える
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static T[][] Transpose<T>(this T[][] source)
		{
			var columnLengths = source.Select(x => x.Length);
			if (columnLengths.Distinct().Count() > 1)
			{
				throw new ArgumentNullException("全ての行の列数が一致している必要があります");
			}

			var columns = columnLengths.First();
			var list = new List<T[]>();
			for (int c = 0; c < columns; c++)
			{
				list.Add(source.Select(x => x[c]).ToArray());
			}

			return list.ToArray();
		}
	}
}
