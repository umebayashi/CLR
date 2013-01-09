using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysMath = System.Math;
namespace Umebayashi.MathEx.Statistics
{
	/// <summary>
	/// 基本統計量計算用クラス
	/// </summary>
	public static class BaseStatistics
	{
		#region public method

		/// <summary>
		/// 平均値
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static double Mean(IEnumerable<double> data)
		{
			CheckArgument(data);

			return data.Average();
		}

		/// <summary>
		/// 中央値
		/// </summary>
		/// <param name="sample"></param>
		/// <returns></returns>
		public static double Median(IEnumerable<double> data)
		{
			CheckArgument(data);

			var count = data.Count();
			if (count == 1)
			{
				return data.ElementAt(0);
			}

			var sorted = data.OrderBy(x => x);
			int div, rem;
			//div = SysMath.DivRem(count, 2, out rem);
			div = count / 2;
			rem = count % 2;
			if (rem == 0)
			{
				return Mean(new double[] { sorted.ElementAt(div - 1), sorted.ElementAt(div) });
			}
			else
			{
				return sorted.ElementAt(div);
			}
		}

		/// <summary>
		/// 最頻値
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static IEnumerable<double> Mode(IEnumerable<double> data)
		{
			CheckArgument(data);

			if (data.Count() == 1)
			{
				return new double[] { data.ElementAt(0) };
			}

			var items = data.GroupBy(x => x).Select(x => new { Key = x.Key, ItemCount = x.Count() });
			var max = items.Max(x => x.ItemCount);
			return items.Where(x => x.ItemCount == max).OrderBy(x => x.Key).Select(x => x.Key);
		}

		/// <summary>
		/// 分散
		/// </summary>
		/// <param name="data"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static double Variance(IEnumerable<double> data, VarianceType type = VarianceType.Unbiased)
		{
			CheckArgument(data);

			var mean = Mean(data);
			double n = data.Count();

			if (type == VarianceType.Unbiased && n == 1)
			{
				throw new InvalidOperationException("標本数1の場合不偏分散は計算できません");
			}

			var temp = data.Select(x => (mean - x) * (mean - x)).Sum();
			if (type == VarianceType.Sample)
			{
				return temp / n;
			}
			else
			{
				return temp / (n - 1.0);
			}
		}

		/// <summary>
		/// 標準偏差
		/// </summary>
		/// <param name="data"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static double StdDev(IEnumerable<double> data, VarianceType type = VarianceType.Unbiased)
		{
			CheckArgument(data);

			var variance = Variance(data, type);
			return SysMath.Sqrt(variance);
		}

		/// <summary>
		/// 偏差値
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static IEnumerable<double> StdScore(IEnumerable<double> data)
		{
			CheckArgument(data);

			var mean = Mean(data);
			var sd = StdDev(data, VarianceType.Sample);
			var z = data.Select(x => (x - mean) / sd);

			return z.Select(x => x * 10.0 + 50.0);
		}

		/// <summary>
		/// 歪度
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static double Skewness(IEnumerable<double> data, SkewnessType type = SkewnessType.Moment)
		{
			CheckArgument(data);

			double n = data.Count();

			if (type == SkewnessType.Fisher)
			{
				if (n < 3)
				{
					throw new InvalidOperationException("要素数が3未満の場合フィッシャー法では計算できません");
				}

				var numerator = (Math.Sqrt(n * (n - 1.0)) / (n - 2.0)) * (data.Select(x => x * x * x).Sum() / (double)n);
				var denominator = Math.Pow(data.Select(x => x * x).Sum() / (double)n, 3.0 / 2.0);

				return numerator / denominator;
			}
			else
			{
				var mean = Mean(data);
				var sd = StdDev(data, VarianceType.Unbiased);

				var numerator = data.Select(x => (x - mean) * (x - mean) * (x - mean)).Sum() / n;
				var denominator = sd * sd * sd;

				return numerator / denominator;
			}
		}

		/// <summary>
		/// 尖度
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static double Kurtosis(IEnumerable<double> data, KurtosisType type = KurtosisType.Excess)
		{
			CheckArgument(data);

			double n = data.Count();
			var mean = Mean(data);
			var variance = Variance(data, VarianceType.Unbiased);

			var kurtosis = 0.0;
			if (type == KurtosisType.Excess)
			{
				kurtosis = (data.Select(x => Math.Pow(x - mean, 4.0) / (variance * variance)).Sum() / (double)n) - 3.0;
			}
			else if (type == KurtosisType.Moment)
			{
				kurtosis = (data.Select(x => Math.Pow(x - mean, 4.0) / (variance * variance)).Sum() / (double)n);
			}
			else
			{
				kurtosis = (
					(n + 1) *
					(n - 1) *
					(
						(data.Select(x => Math.Pow(x, 4.0)).Sum() / (double)n) /
						(Math.Pow((data.Select(x => Math.Pow(x, 2.0)).Sum() / (double)n), 2.0)) -
						(3 * (n - 1)) / (n + 1)
					)
				) / ((n - 2) * (n - 3));
			}

			return kurtosis;
		}

		/// <summary>
		/// 共分散
		/// </summary>
		/// <param name="data1"></param>
		/// <param name="data2"></param>
		/// <returns></returns>
		public static double Covariance(IEnumerable<double> data1, IEnumerable<double> data2, VarianceType type = VarianceType.Unbiased)
		{
			CheckArgument(data1, "data1");
			CheckArgument(data2, "data2");
			CheckArgumentLength(data1, data2);

			var n = data1.Count();
			var mean1 = Mean(data1);
			var mean2 = Mean(data2);

			var sum = data1.Select((x, i) => (x - mean1) * (data2.ElementAt(i) - mean2)).Sum();

			if (type == VarianceType.Unbiased)
			{
				return sum / (n - 1);
			}
			else
			{
				return sum / n;
			}
		}

		/// <summary>
		/// 共分散
		/// </summary>
		/// <param name="data"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static double Covariance(IEnumerable<VectorD> data, int index1, int index2, VarianceType type = VarianceType.Unbiased)
		{
			CheckArgument(data);

			return Covariance(data.Select(x => x[index1]), data.Select(x => x[index2]), type);
		}

		/// <summary>
		/// 相関係数
		/// </summary>
		/// <param name="data1"></param>
		/// <param name="data2"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static double Correlation(IEnumerable<double> data1, IEnumerable<double> data2)
		{
			CheckArgument(data1, "data1");
			CheckArgument(data2, "data2");
			CheckArgumentLength(data1, data2);

			var cov = Covariance(data1, data2);
			var sd1 = StdDev(data1);
			var sd2 = StdDev(data2);

			return cov / (sd1 * sd2);
		}

		/// <summary>
		/// 相関係数
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static double Correlation(IEnumerable<VectorD> data, int index1, int index2)
		{
			CheckArgument(data);

			return Correlation(data.Select(x => x[index1]), data.Select(x => x[index2]));
		}

		/// <summary>
		/// 順位相関係数
		/// </summary>
		/// <param name="data1"></param>
		/// <param name="data2"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static double RankCorrelation(IEnumerable<double> data1, IEnumerable<double> data2, RankCorrelationType type)
		{
			CheckArgument(data1);
			CheckArgument(data2);
			CheckArgumentLength(data1, data2);

			if (type == RankCorrelationType.Spearmans)
			{
				return RankCorrelation_Spearmans(data1, data2);
			}
			else
			{
				return RankCorrelation_Kendalls(data1, data2);
			}
		}

		#endregion

		#region non public method

		private static void CheckArgument(IEnumerable<double> data, string parameterName = "data")
		{
			if (data == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			else if (data.Count() == 0)
			{
				throw new ArgumentException("data length is 0", parameterName);
			}
		}

		private static void CheckArgument(IEnumerable<VectorD> data, string parameterName = "data")
		{
			if (data == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			else if (data.Count() == 0)
			{
				throw new ArgumentException("data length is 0", parameterName);
			}
			else
			{
				var first = data.First();
				var len = first.Length;
				if (data.Where(x => x.Length != len).Count() > 0)
				{
					throw new ArgumentException("vector length not match", parameterName);
				}
			}
		}

		private static void CheckArgumentLength(IEnumerable<double> data1, IEnumerable<double> data2)
		{
			if (data1.Count() != data2.Count())
			{
				throw new InvalidOperationException("data length not match");
			}
		}

		/// <summary>
		/// スピアマンの順位相関係数
		/// </summary>
		/// <param name="data1"></param>
		/// <param name="data2"></param>
		/// <returns></returns>
		private static double RankCorrelation_Spearmans(IEnumerable<double> data1, IEnumerable<double> data2)
		{
			var len = data1.Count();
			var d2 = data1.Select((x, i) => (x - data2.ElementAt(i)) * (x - data2.ElementAt(i))).Sum();
			var value = 1.0 - ((6.0 * d2) / (len * (len * len - 1)));

			return value;
		}

		/// <summary>
		/// ケンドールの順位相関係数
		/// </summary>
		/// <param name="data1"></param>
		/// <param name="data2"></param>
		/// <returns></returns>
		private static double RankCorrelation_Kendalls(IEnumerable<double> data1, IEnumerable<double> data2)
		{
			var d1 = data1.ToArray();
			var d2 = data2.ToArray();
			var cgen = new CombinationGenerator<int>();
			var cmbs = cgen.Generate(Enumerable.Range(0, d1.Length), 2);
			int p = 0;
			foreach (var c in cmbs)
			{
				//if ((d1[c[0]] <= d1[c[1]]) && (d2[c[0]] <= d2[c[1]]))
				if (d1[c[0]].CompareTo(d1[c[1]]) == d2[c[0]].CompareTo(d2[c[1]]))
				{
					p++;
				}
			}
			var value = (4.0 * p) / (d1.Length * (d1.Length - 1)) - 1.0;

			return value;
		}

		#endregion
	}

	/// <summary>
	/// 分散の種類
	/// </summary>
	public enum VarianceType
	{
		/// <summary>
		/// 不偏分散
		/// </summary>
		Unbiased,

		/// <summary>
		/// 標本分散
		/// </summary>
		Sample
	}

	/// <summary>
	/// 歪度の種類
	/// </summary>
	public enum SkewnessType
	{
		/// <summary>
		/// モーメント法
		/// </summary>
		Moment,

		/// <summary>
		/// フィッシャー法
		/// </summary>
		Fisher
	}

	/// <summary>
	/// 尖度の種類
	/// </summary>
	public enum KurtosisType
	{
		/// <summary>
		/// 超過法
		/// </summary>
		Excess,

		/// <summary>
		/// モーメント法
		/// </summary>
		Moment,

		/// <summary>
		/// フィッシャー法
		/// </summary>
		Fisher
	}

	public enum RankCorrelationType
	{
		/// <summary>
		/// スピアマンの順位相関係数
		/// </summary>
		Spearmans,

		/// <summary>
		/// ケンドールの順位相関係数
		/// </summary>
		Kendalls
	}
}
