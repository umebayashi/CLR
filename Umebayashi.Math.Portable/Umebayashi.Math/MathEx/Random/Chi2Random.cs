using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Random
{
	/// <summary>
	/// カイ2乗分布乱数クラス
	/// </summary>
	public class Chi2Random
	{
		private static object lockObj = new object();
		private static NormalRandom random = new NormalRandom();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="freedomDegree">自由度</param>
		/// <returns></returns>
		public double NextDouble(int freedomDegree)
		{
			lock (lockObj)
			{
				double s = 0;
				for (int i = 0; i < freedomDegree; i++)
				{
					double r = random.NextDouble();
					s += r * r;
				}

				return s;
			}
		}
	}
}
