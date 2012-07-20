using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Statistics
{
	[TestClass]
	public class RegressionAnalysisTest
	{
		[TestMethod]
		public void TestAnalyzeSingle01()
		{
			var dependent = new double[] {
				286, 851, 589, 389, 158, 1037, 463, 563, 372, 1020
			};
			var explanatory = new double[] {
				107, 336, 233, 82, 61, 378, 129, 313, 142, 428
			};

			var result = RegressionAnalysis.AnalyzeSingle(dependent, explanatory);

			Assert.AreEqual<double>(99.075, Math.Round(result.Intercept, 3));
			Assert.AreEqual<double>(2.145, Math.Round(result.Slope, 3));
			Assert.AreEqual<double>(0.8934, Math.Round(result.DeterminationCoefficient, 4));

			//Assert.AreEqual<double>(744983.8, Math.Round(result.VarianceAnalysisTable.RegressionVariationSquareSum, 1));
			//Assert.AreEqual<double>(88711.8, Math.Round(result.VarianceAnalysisTable.ResidialVariationSquareSum, 1));
			//Assert.AreEqual<double>(833695.6, Math.Round(result.VarianceAnalysisTable.TotalVariationSquareSum, 1));
			//Assert.AreEqual<int>(1, result.VarianceAnalysisTable.RegressionVariationFreedomDegree);
			//Assert.AreEqual<int>(8, result.VarianceAnalysisTable.ResidialVariationFreedomDegree);
			//Assert.AreEqual<double>(744983.8, Math.Round(result.VarianceAnalysisTable.RegressionVariationMeanSquare, 1));
			//Assert.AreEqual<double>(11089.0, Math.Round(result.VarianceAnalysisTable.ResidialVariationMeanSquare, 1));
			//Assert.AreEqual<double>(67.18, Math.Round(result.VarianceAnalysisTable.F, 2));
		}
	}
}
