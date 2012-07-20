using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Analysis
{
	/// <summary>
	/// ベータ関数
	/// </summary>
	public static class BetaFunction
	{
		/// <summary>
		/// ベータ関数
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static double Beta(double x, double y)
		{
			return Math.Exp(
				GammaFunction.LogGamma(x) +
				GammaFunction.LogGamma(y) -
				GammaFunction.LogGamma(x + y)
			);
		}

		/// <summary>
		/// 不完全ベータ関数
		/// </summary>
		/// <param name="x"></param>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="convergent"></param>
		/// <returns></returns>
		public static double P_Beta(double x, double a, double b, out bool convergent)
		{
			convergent = true;

			if (a <= 0)
			{
				return double.PositiveInfinity;
			}
			if (b <= 0)
			{
				if (x < 1) return 0;
				if (x == 1) return 1;
				return double.PositiveInfinity;
			}
			if (x > (a + 1) / (a + b + 2))
			{
				return 1 - P_Beta(1 - x, b, a, out convergent);
			}
			if (x <= 0) return 0;

			double p1 = 0, q1 = 1, q2 = 1;
			double p2 = Math.Exp(
				a * Math.Log(x) +
				b * Math.Log(1 - x) +
				GammaFunction.LogGamma(a + b) -
				GammaFunction.LogGamma(a) -
				GammaFunction.LogGamma(b)) / a;

			for (int k = 0; k < 200; )
			{
				double previous = p2;
				double d = -1 * (a + k) * (a + b + k) * x / ((a + 2 * k) * (a + 2 * k + 1));
				p1 = p1 * d + p2;
				q1 = q1 * d + q2;
				k++;
				d = k * (b - k) * x / ((a + 2 * k - 1) * (a + 2 * k));
				p2 = p2 * d + p1;
				q2 = q2 * d + q1;
				if (q2 == 0)
				{
					p2 = double.PositiveInfinity;
					continue;
				}
				p1 /= q2;
				q1 /= q2;
				p2 /= q2;
				q2 = 1;
				if (p2 == previous) return p2;
			}

			convergent = false;

			return p2;
		}

		/// <summary>
		/// 不完全ベータ関数
		/// </summary>
		/// <param name="x"></param>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static double Q_Beta(double x, double a, double b)
		{
			bool convergent;
			var result = 1 - P_Beta(x, a, b, out convergent);

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 自由度dfのt分布の下側確率を求める
		/// </summary>
		/// <param name="x">確率変数</param>
		/// <param name="df">自由度</param>
		/// <returns></returns>
		public static double P_T(double x, int df)
		{
			bool convergent;
			var result = 1 - 0.5 * P_Beta(df / (df + x * x), 0.5 * df, 0.5, out convergent);

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 自由度dfのt分布の上側確率を求める
		/// </summary>
		/// <param name="x">確率変数</param>
		/// <param name="df">自由度</param>
		/// <returns></returns>
		public static double Q_T(double x, int df)
		{
			bool convergent;
			var result = 0.5 * P_Beta(df / (df + x * x), 0.5 * df, 0.5, out convergent);

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 自由度df1, df2のF分布の下側確率を求める
		/// </summary>
		/// <param name="x">確率変数</param>
		/// <param name="df1">自由度1</param>
		/// <param name="df2">自由度2</param>
		/// <returns></returns>
		public static double P_F(double x, int df1, int df2)
		{
			if (x <= 0) return 0;

			bool convergent;
			var result = P_Beta(df1 / (df1 + df2 / x), 0.5 * df1, 0.5 * df2, out convergent);

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 自由度df1, df2のF分布の上側確率を求める
		/// </summary>
		/// <param name="x">確率変数</param>
		/// <param name="df1">自由度1</param>
		/// <param name="df2">自由度2</param>
		/// <returns></returns>
		public static double Q_F(double x, int df1, int df2)
		{
			if (x <= 0) return 1;

			bool convergent;
			var result = P_Beta(df2 / (df2 + df1 * x), 0.5 * df2, 0.5 * df1, out convergent);

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 二項分布の下側確率を求める
		/// </summary>
		/// <param name="n"></param>
		/// <param name="p"></param>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double P_Binomial(int n, double p, int x)
		{
			if (x < 0) return 0;
			if (x >= n) return 1;

			bool convergent;
			var result = P_Beta(1 - p, x + 1, n - x, out convergent);

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 二項分布の上側確立を求める
		/// </summary>
		/// <param name="n"></param>
		/// <param name="p"></param>
		/// <param name="k"></param>
		/// <returns></returns>
		public static double Q_Binomial(int n, double p, int k)
		{
			if (k <= 0) return 1;
			if (k > n) return 0;

			bool convergent;
			var result = P_Beta(p, k, n - k + 1, out convergent);

			return GetResult(result, convergent);
		}

		private static double GetResult(double result, bool convergent)
		{
			if (convergent)
			{
				return result;
			}
			else
			{
				throw new ArithmeticException("関数が収束しません");
			}
		}
	}
}
