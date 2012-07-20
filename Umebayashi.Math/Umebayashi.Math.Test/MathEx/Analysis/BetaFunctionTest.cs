using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Analysis
{
	[TestClass]
	public class BetaFunctionTest
	{
		[TestMethod]
		public void TestBeta()
		{
			foreach (var x in new RangeD(1.0, 2.0, 0.1))
			{
				foreach (var y in new RangeD(1.0, 2.0, 0.1))
				{
					Console.WriteLine("Beta(x = {0}, y = {1}) = {2}", x, y, BetaFunction.Beta(x, y));
				}
			}
		}

		[TestMethod]
		public void TestP_F()
		{
			var p = BetaFunction.P_F(0.2761709627258, 10, 12);
			Console.WriteLine(p);
		}
	}
}
