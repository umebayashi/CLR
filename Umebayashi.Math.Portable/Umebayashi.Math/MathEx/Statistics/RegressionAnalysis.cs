using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Statistics
{
	/// <summary>
	/// 回帰分析
	/// </summary>
	public static class RegressionAnalysis
	{
		/// <summary>
		/// 単回帰分析を実行する
		/// </summary>
		/// <param name="dependent">従属変数(y)</param>
		/// <param name="explanatory">説明変数(x)</param>
		/// <returns></returns>
		public static SingleRegressionAnalysisResult AnalyzeSingle(IEnumerable<double> dependent, IEnumerable<double> explanatory)
		{
			var result = new SingleRegressionAnalysisResult();

			var n = explanatory.Count();
			var xsum = explanatory.Sum();
			var ysum = dependent.Sum();
			var xysum = explanatory.Select((x, i) => x * dependent.ElementAt(i)).Sum();
			var x2sum = explanatory.Select(x => x * x).Sum();
			var y2sum = dependent.Select(y => y * y).Sum();
			var xvar = BaseStatistics.Variance(explanatory);
			var cov = BaseStatistics.Covariance(explanatory, dependent);

			var sy2 = y2sum - (ysum * ysum) / n;
			var sxy = xysum - xsum * ysum / n;
			var a = (x2sum * ysum - xysum * xsum) / (n * x2sum - xsum * xsum);
			var b = cov / xvar;
			var sr = b * sxy;
			var se = sy2 - sr;
			
			// 切片の計算
			result.Intercept = a;

			// 傾きの計算
			result.Slope = b;

			// 決定係数の計算
			result.DeterminationCoefficient = sr / sy2;

			// 分散分析表の計算
			result.VarianceAnalysisTable = new SingleVarianceAnalysisTable
			{
				RegressionVariationSquareSum = sr,
				ResidialVariationSquareSum = se,
				TotalVariationSquareSum = sy2,
				RegressionVariationFreedomDegree = 1,
				ResidialVariationFreedomDegree = n - 2,
				RegressionVariationMeanSquare = sr,
				ResidialVariationMeanSquare = se / (n - 2),
				F = sr / (se / (n - 2))
			};

			return result;
		}

		/// <summary>
		/// 重回帰分析を実行する
		/// </summary>
		/// <param name="dependent"></param>
		/// <param name="explanatories"></param>
		/// <returns></returns>
		public static MultipleRegressionAnalysisResult AnalyzeMultiple(IEnumerable<double> dependent, IEnumerable<double>[] explanatories)
		{
			throw new NotImplementedException();
		}
	}

	public class SingleRegressionAnalysisResult
	{
		/// <summary>
		/// 切片 ("Y = a + bX"のaにあたる)
		/// </summary>
		public double Intercept { get; set; }

		/// <summary>
		/// 傾き ("Y = a + bX"のbにあたる)
		/// </summary>
		public double Slope { get; set; }

		/// <summary>
		/// 決定係数 (R2)
		/// </summary>
		public double DeterminationCoefficient { get; set; }

		/// <summary>
		/// 分散分析表
		/// </summary>
		public SingleVarianceAnalysisTable VarianceAnalysisTable { get; set; }
	}

	public class SingleVarianceAnalysisTable
	{
		/// <summary>
		/// 回帰変動平方和
		/// </summary>
		public double RegressionVariationSquareSum { get; set; }

		/// <summary>
		/// 残差変動平方和
		/// </summary>
		public double ResidialVariationSquareSum { get; set; }

		/// <summary>
		/// 全変動平方和
		/// </summary>
		public double TotalVariationSquareSum { get; set; }

		/// <summary>
		/// 回帰変動自由度
		/// </summary>
		public int RegressionVariationFreedomDegree { get; set; }

		/// <summary>
		/// 残差変動自由度
		/// </summary>
		public int ResidialVariationFreedomDegree { get; set; }

		/// <summary>
		/// 回帰変動平均平方
		/// </summary>
		public double RegressionVariationMeanSquare { get; set; }

		/// <summary>
		/// 残差変動平均平方
		/// </summary>
		public double ResidialVariationMeanSquare { get; set; }

		/// <summary>
		/// F値
		/// </summary>
		public double F { get; set; }
	}

	public class MultipleRegressionAnalysisResult
	{
	}
}
