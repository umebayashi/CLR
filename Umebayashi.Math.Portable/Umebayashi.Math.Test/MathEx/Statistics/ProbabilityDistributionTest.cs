using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Statistics
{
	[TestClass]
	public class ProbabilityDistributionTest
	{
		[TestMethod]
		public void TestBinomial()
		{
			var n = 10;

			for (double p = 0.1; p <= 1.0; p += 0.1)
			{
				Console.WriteLine("Binomial(n = {0}, p = {1})", n, p);
				for (int x = 0; x <= n; x++)
				{
					var y = ProbabilityDistribution.Binomial(n, p, x);
					OutputResult(x, y);
				}
				Console.WriteLine();
			}
		}

		[TestMethod]
		public void TestHyperGeometric()
		{
			var n_all = 100;
			var n_sample = 10;

			for (double p = 0.1; p <= 1.0; p += 0.1)
			{
				Console.WriteLine("HyperGeometric(n_all = {0}, n_sample = {1}, p = {2})", n_all, n_sample, p);
				for (int x = 0; x <= n_sample; x++)
				{
					var y = ProbabilityDistribution.HyperGeometric(n_all, n_sample, p, x);
					OutputResult(x, y);
				}
				Console.WriteLine();
			}
		}

		[TestMethod]
		public void TestPoisson()
		{
			var lambda = 1;
			Console.WriteLine("Poisson(lambda = {0})", lambda);
			for (int x = 0; x <= 20; x++)
			{
				var y = ProbabilityDistribution.Poisson(lambda, x);
				OutputResult(x, y);
			}
		}

		[TestMethod]
		public void TestNormal()
		{
			var mean = 0;
			var variance = 1;
			Console.WriteLine("Normal(mean = {0}, variance = {1})", mean, variance);

			using (StreamWriter writer = new StreamWriter(@"c:\temp\normal.csv"))
			{
				foreach (var x in new RangeD(-4.0, 4.0, 0.01))
				{
					var y = ProbabilityDistribution.Normal(mean, variance, x);
					OutputResult(writer, x, y);
				}
			}
		}

		[TestMethod]
		public void TestChi2()
		{
			for (int n = 1; n <= 10; n++)
			{
				Console.WriteLine("Chi2(n = {0})", n);
				foreach (var x in new RangeD(0.0, 10.0, 0.1))
				{
					var y = ProbabilityDistribution.Chi2(n, x);
					OutputResult(x, y);
				}
				Console.WriteLine();
			}
		}

		[TestMethod]
		public void TestT()
		{
			for (int n = 1; n <= 10; n++)
			{
				Console.WriteLine("t(n = {0})", n);
				foreach (var x in new RangeD(-4.0, 4.0, 0.1))
				{
					var y = ProbabilityDistribution.T(n, x);
					OutputResult(x, y);
				}
				Console.WriteLine();
			}
		}

		[TestMethod]
		public void TestF()
		{
			Console.WriteLine("F(n1 = 4, n2 = 6)");
			foreach (var x in new RangeD(0, 10, 0.1))
			{
				var y = ProbabilityDistribution.F(4, 6, x);
				OutputResult(x, y);
			}
		}

		[TestMethod]
		public void TestNormalProbability()
		{
			//var p = ProbabilityDistribution.NormalProbability(157.77, 5.14 * 5.14, 150.0, 160.0);
			var p = ProbabilityDistribution.NormalProbability(0, 1, -1.51, 0.43);
			Assert.AreEqual<double>(0.60088, Math.Round(p, 5));
		}

		private static void OutputResult(double x, double y)
		{
			Console.WriteLine("x = {0} => y = {1}", x, y);
		}

		private static void OutputResult(TextWriter writer, double x, double y)
		{
			OutputResult(x, y);

			writer.Write(x);
			writer.Write('\t');
			writer.Write(y);
			writer.WriteLine();
		}
	}
}
