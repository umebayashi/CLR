using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Random
{
	/// <summary>
	/// F分布乱数生成クラス
	/// </summary>
	public class FRandom
	{
		private static object lockObj = new object();
		private static Chi2Random chi2Random = new Chi2Random();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="freedomDegree1"></param>
		/// <param name="freedomDegree2"></param>
		/// <returns></returns>
		public double NextDouble(int freedomDegree1, int freedomDegree2)
		{
			lock (lockObj)
			{
				var r1 = chi2Random.NextDouble(freedomDegree1);
				var r2 = chi2Random.NextDouble(freedomDegree2);

				return (r1 * freedomDegree2) / (r2 * freedomDegree1);
			}
		}
	}
}
