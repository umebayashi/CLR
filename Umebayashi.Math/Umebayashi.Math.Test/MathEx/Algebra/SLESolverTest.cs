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
			var a = new MatrixD(new double[] { 1, 4, 4, 2, 2, 5, 3, 1, 1, -2, -3, 1, 1, 4, 1, 3 }, 4, 4);
			var b = new VectorD(new double[] { -1, -7, -12, 2 });

			var c = SLESolver.Solve(a, b, SLEAlgorithm.GE);
			Assert.AreEqual<int>(4, c.Length);
			Assert.AreEqual<double>(-2.0000, Math.Round(c[0], 4));
			Assert.AreEqual<double>(-1.0000, Math.Round(c[1], 4));
			Assert.AreEqual<double>(1.0000, Math.Round(c[2], 4));
			Assert.AreEqual<double>(2.0000, Math.Round(c[3], 4));
		}

		[TestMethod]
		public void TestSolveGEPP()
		{
			//var a = new MatrixD(new double[] { 0, 1, 3, 1, 0, 1, 2, 3, 0 }, 3, 3);
			//var b = new VectorD(new double[] { 2, 2, -3 });

			//var c = SLESolver.Solve(a, b, SLEAlgorithm.GEPP);
			//Assert.AreEqual<int>(3, c.Length);
			//Assert.AreEqual<double>(-1.0000, Math.Round(c[0], 4));
			//Assert.AreEqual<double>(0, Math.Round(c[1], 4));
			//Assert.AreEqual<double>(1.0000, Math.Round(c[2], 4));
			var a = new MatrixD(new double[] { 2, -1, 4, 5, 4, -2, 2, -4, 1, 2, -3, -3, -3, 4, 5, 1 }, 4, 4);
			var b = new VectorD(new double[] { 0, 10, 2, 6 });

			var c = SLESolver.Solve(a, b, SLEAlgorithm.GEPP);
			Assert.AreEqual<int>(4, c.Length);
			Assert.AreEqual<double>(2.0000, Math.Round(c[0], 4));
			Assert.AreEqual<double>(-1.0000, Math.Round(c[1], 4));
			Assert.AreEqual<double>(3.0000, Math.Round(c[2], 4));
			Assert.AreEqual<double>(1.0000, Math.Round(c[3], 4));
		}
	}
}
