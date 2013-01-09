using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Random
{
	/// <summary>
	/// 正規分布乱数クラス
	/// </summary>
	public class NormalRandom
	{
		private static object lockObj = new object();
		private static MTRandom random = new MTRandom();

		private bool sw = true;
		private double t = 0;
		private double u = 0;

		/// <summary>
		/// 平均0,分散1の正規乱数を返す
		/// </summary>
		/// <returns></returns>
		public double NextDouble()
		{
			lock (lockObj)
			{
				if (sw)
				{
					sw = false;

					t = Math.Sqrt(-2 * Math.Log(1 - random.NextDouble()));
					u = 2 * Math.PI * random.NextDouble();

					return t * Math.Cos(u);
				}
				else
				{
					sw = true;

					return t * Math.Sin(u);
				}
			}
		}
	}
}
