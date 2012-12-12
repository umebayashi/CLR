using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Analysis
{
	/// <summary>
	/// ガンマ関数
	/// </summary>
	public static class GammaFunction
	{
		private static readonly double LOG_2PI = Math.Log(2 * Math.PI);
		private static readonly double LOG_PI = Math.Log(Math.PI);
		private static readonly int N = 8;

		//private static readonly double B0 = 1.0;
		//private static readonly double B1 = -1.0 / 2.0;
		private static readonly double B2 = 1.0 / 6.0;
		private static readonly double B4 = -1.0 / 30.0;
		private static readonly double B6 = 1.0 / 42.0;
		private static readonly double B8 = -1.0 / 30.0;
		private static readonly double B10 = 5.0 / 66.0;
		private static readonly double B12 = -691.0 / 2730.0;
		private static readonly double B14 = 7.0 / 6.0;
		private static readonly double B16 = -3617.0 / 510.0;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double LogGamma(double x)
		{
			double v = 1.0;
			while (x < N)
			{
				v *= x;
				x++;
			}
			double w = 1 / (x * x);
			double ret = 0;
			ret = B16 / (16 * 15);
			ret = ret * w + B14 / (14 * 13);
			ret = ret * w + B12 / (12 * 11);
			ret = ret * w + B10 / (10 * 9);
			ret = ret * w + B8 / (8 * 7);
			ret = ret * w + B6 / (6 * 5);
			ret = ret * w + B4 / (4 * 3);
			ret = ret * w + B2 / (2 * 1);
			ret = ret / x + 0.5 * LOG_2PI - Math.Log(v) - x + (x - 0.5) * Math.Log(x);

			return ret;
		}

		/// <summary>
		/// ガンマ関数
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Gamma(double x)
		{
			if (x < 0.0)
			{
				return Math.PI / (Math.Sin(Math.PI * x) * Math.Exp(LogGamma(1 - x)));
			}

			return Math.Exp(LogGamma(x));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="x">確率変数</param>
		/// <param name="logGamma_a"></param>
		/// <param name="convergent">関数が収束したかどうか</param>
		/// <returns></returns>
		public static double P_Gamma(double a, double x, double logGamma_a, out bool convergent)
		{
			convergent = true;

			if (x >= 1 + a)
			{
				return 1 - Q_Gamma(a, x, logGamma_a, out convergent);
			}
			if (x == 0)
			{
				return 0;
			}

			double result = Math.Exp(a * Math.Log(x) - x - logGamma_a) / a;
			double term = result;

			for (int k = 1; k < 1000; k++)
			{
				term *= x / (a + k);
				double previous = result;
				result += term;
				if (result == previous)
				{
					return result;
				}
			}

			convergent = false;
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="x"></param>
		/// <param name="logGamma_a"></param>
		/// <param name="convergent"></param>
		/// <returns></returns>
		public static double Q_Gamma(double a, double x, double logGamma_a, out bool convergent)
		{
			convergent = true;

			if (x < 1 + a)
			{
				return 1 - P_Gamma(a, x, logGamma_a, out convergent);
			}

			double w = Math.Exp(a * Math.Log(x) - x - logGamma_a);
			double la = 1, lb = 1 + x - a, result = w / lb;

			for (int k = 2; k < 1000; k++)
			{
				double temp = ((k - 1 - a) * (lb - la) + (k + x) * lb) / k;
				la = lb;
				lb = temp;
				w *= (k - 1 - a) / k;
				temp = w / (la * lb);
				double previous = result;
				result += temp;
				if (result == previous)
				{
					return result;
				}
			}

			convergent = false;

			return result;
		}

		/// <summary>
		/// 自由度dfのカイ２乗分布の下側確率を求める
		/// </summary>
		/// <param name="x">確率変数</param>
		/// <param name="df">自由度</param>
		/// <returns></returns>
		public static double P_Chi2(double x, int df)
		{
			bool convergent;
			var result = 
				P_Gamma(0.5 * df, 0.5 * x, GammaFunction.LogGamma(0.5 * df), out convergent);

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 自由度dfのカイ２乗分布の上側確率を求める
		/// </summary>
		/// <param name="x">確率変数</param>
		/// <param name="df">自由度</param>
		/// <returns></returns>
		public static double Q_Chi2(double x, int df)
		{
			bool convergent;
			var result = 
				Q_Gamma(0.5 * df, 0.5 * x, GammaFunction.LogGamma(0.5 * df), out convergent);

			return GetResult(result, convergent);
		}

		/// <summary>
		/// Gaussの誤差関数
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double ERF(double x)
		{
			bool convergent;
			double result;
			if (x >= 0)
			{
				result = P_Gamma(0.5, x * x, LOG_PI / 2.0, out convergent);
			}
			else
			{
				result = -1 * P_Gamma(0.5, x * x, +LOG_PI / 2.0, out convergent);
			}

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 1 - ERF(x)
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double ERFC(double x)
		{
			bool convergent;
			double result;
			if (x >= 0)
			{
				result = Q_Gamma(0.5, x * x, LOG_PI / 2.0, out convergent);
			}
			else
			{
				result = P_Gamma(0.5, x * x, LOG_PI / 2.0, out convergent) + 1;
			}

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 標準正規分布の下側確率を求める
		/// </summary>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double P_Normal(double x)
		{
			bool convergent;
			double result;
			if (x >= 0)
			{
				result = 0.5 * (1 + P_Gamma(0.5, 0.5 * x * x, LOG_PI / 2.0, out convergent));
			}
			else
			{
				result = 0.5 * Q_Gamma(0.5, 0.5 * x * x, LOG_PI / 2.0, out convergent);
			}

			return GetResult(result, convergent);
		}

		/// <summary>
		/// 標準正規分布の上側確率を求める
		/// </summary>
		/// <param name="x">確率変数</param>
		/// <returns></returns>
		public static double Q_Normal(double x)
		{
			bool convergent;
			double result;
			if (x >= 0)
			{
				result = 0.5 * Q_Gamma(0.5, 0.5 * x * x, LOG_PI / 2.0, out convergent);
			}
			else
			{
				result = 0.5 * (1 + P_Gamma(0.5, 0.5 * x * x, LOG_PI / 2.0, out convergent));
			}

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
