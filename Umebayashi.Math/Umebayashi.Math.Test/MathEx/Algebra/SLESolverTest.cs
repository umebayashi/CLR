using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Algebra
{
	[TestClass]
	public class SLESolverTest
	{
		[TestMethod]
		public void TestSolveGE()
		{
			var a = new MatrixD(new double[] { 3, 5, 4, 1, 1, 2, 2, 3, 1 }, 3, 3);
			var b = new VectorD(new double[] { 13, 20, 13 });

			var c = SLESolver.Solve(a, b, SLEAlgorithm.GE);
			Assert.AreEqual<int>(3, c.Length);
			Assert.AreEqual<double>(2.0000, Math.Round(c[0], 4));
			Assert.AreEqual<double>(1.0000, Math.Round(c[1], 4));
			Assert.AreEqual<double>(3.0000, Math.Round(c[2], 4));
		}
	}
}
