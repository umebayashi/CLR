using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umebayashi.MathEx.Analysis;

namespace Umebayashi.MathEx.Statistics
{
	/// <summary>
	/// 確率分布
	/// </summary>
	public static class ProbabilityDistribution
	{
		#region public method

		#region 確率密度関数

		/// <summary>
		/// 二項分布
		/// </summary>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double Binomial(long n, double p, long x)
		{
			return 
				(n.Factorial() / (x.Factorial() * (n - x).Factorial())) * 
				Math.Pow(p, x) * 
				Math.Pow(1 - p, n - x);
		}

		/// <summary>
		/// 超幾何分布
		/// </summary>
		/// <param name="n_all"></param>
		/// <param name="n_sample"></param>
		/// <param name="p"></param>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double HyperGeometric(long n_all, long n_sample, double p, long x)
		{
			double v1 = Combinatorics.CombinationCount((long)(n_all * p), x);
			double v2 = Combinatorics.CombinationCount(n_all - (long)(n_all * p), n_sample - x);
			double v3 = Combinatorics.CombinationCount(n_all, n_sample);
			return (v1 * v2) / v3;
				//(
				//	Combinatorics.CombinationCount((long)(n_all * p), x) *
				//	Combinatorics.CombinationCount(n_all - (long)(n_all * p), n_sample - x)
				//) /
				//Combinatorics.CombinationCount(n_all, n_sample);
		}

		/// <summary>
		/// ポアソン分布
		/// </summary>
		/// <param name="lambda"></param>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double Poisson(double lambda, long x)
		{
			return
				(Math.Pow(lambda, x) / x.Factorial()) *
				(Math.Pow(Math.E, -1 * lambda));
		}

		/// <summary>
		/// 正規分布
		/// </summary>
		/// <param name="m">平均</param>
		/// <param name="s2">分散</param>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double Normal(double m, double s2, double x)
		{
			var sd = Math.Sqrt(s2);

			return
				(1 / (sd * Math.Sqrt(2 * Math.PI))) *
				(Math.Pow(Math.E, -0.5 * (Math.Pow((x - m) / sd, 2))));
		}

		/// <summary>
		/// カイ２乗分布
		/// </summary>
		/// <param name="df">自由度</param>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double Chi2(int df, double x)
		{
			return
				(1.0 / (Math.Pow(2.0, df / 2.0) * GammaFunction.Gamma(df / 2.0))) *
				(Math.Pow(x, df / 2.0 - 1)) *
				(Math.Pow(Math.E, -1 * (x / 2.0)));
		}

		/// <summary>
		/// t分布
		/// </summary>
		/// <param name="df">自由度</param>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double T(int df, double x)
		{
			return
				(GammaFunction.Gamma((df + 1) / 2)) /
				(
					Math.Sqrt(df * Math.PI) *
					GammaFunction.Gamma(df / 2.0) *
					Math.Pow(1 + x * x / df, (df + 1.0) / 2.0)
				);
		}

		/// <summary>
		/// F分布
		/// </summary>
		/// <param name="df1">自由度1</param>
		/// <param name="df2">自由度2</param>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double F(int df1, int df2, double x)
		{
			double n1n2 = (double)df1 / (double)df2;

			return
				(
					GammaFunction.Gamma((df1 + df2) / 2.0) *
					Math.Pow(n1n2, df2 / 2.0) *
					Math.Pow(x, df1 / 2.0 - 1.0)
				) /
				(
					GammaFunction.Gamma(df1 / 2.0) *
					GammaFunction.Gamma(df2 / 2.0) *
					Math.Pow(1 + n1n2 * x, (df1 + df2) / 2.0)
				);
		}

		#endregion

		#region 累積確率

		/// <summary>
		/// 
		/// </summary>
		/// <param name="m"></param>
		/// <param name="s2"></param>
		/// <param name="x_low"></param>
		/// <param name="x_high"></param>
		/// <returns></returns>
		public static double NormalProbability(double m, double s2, double? x_low, double? x_high)
		{
			//標準化
			double? low = null;
			double? high = null;
			if (x_low.HasValue)
			{
				low = (x_low - m) / Math.Sqrt(s2);
			}
			if (x_high.HasValue)
			{
				high = (x_high - m) / Math.Sqrt(s2);
			}

			if (low.HasValue)
			{
				if (high.HasValue)
				{
					return GammaFunction.P_Normal(high.Value) - GammaFunction.P_Normal(low.Value);
				}
				else
				{
					return 1.0 - GammaFunction.P_Normal(low.Value);
				}
			}
			else
			{
				if (high.HasValue)
				{
					return GammaFunction.P_Normal(high.Value);
				}
				else
				{
					return 1.0;
				}
			}
		}

		#endregion

		#endregion
	}
}
