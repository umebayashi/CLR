using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.MathEx.Algebra
{
	/// <summary>
	/// 連立一次方程式（Simultaneous Linear Equation）の解を求める
	/// </summary>
	public static class SLESolver
	{
		public static VectorD Solve(MatrixD a, VectorD b, SLEAlgorithm algorithm)
		{
			switch (algorithm)
			{
				case SLEAlgorithm.GE:
					return SolveByGE(a, b);
				case SLEAlgorithm.GEPP:
					return SolveByGEPP(a, b);
				case SLEAlgorithm.LU:
					return SolveByLU(a, b);
			}

			return null;
		}

		private static VectorD SolveByGE(MatrixD a, VectorD b)
		{
			int r = 0;
			int c = 0;
			var a2 = a.Clone();
			var b2 = b.Clone();

			while (r < a.Rows - 1)
			{
				for (int i = r + 1; i < a2.Rows; i++)
				{
					var x = a2[i, c] * -1.0;
					a2[i, c] = 0;
					for (int j = c + 1; j < a2.Columns; j++)
					{
						a2[i, j] += a2[r, j] * (x / a2[r, c]);
					}

					b2[i] += b2[r] * (x / a2[r, c]);
				}

				r++;
				c++;
			}

			var n = b.Length;
			var result = new VectorD(new double[n]);
			for (int k = n - 1; k >= 0; k--)
			{
				var sum = 0.0;
				for (int j = k + 1; j < n; j++)
				{
					sum += a2[k, j] * result[j];
				}
				result[k] = (b2[k] - sum) / a2[k, k];
			}
			return result;
		}

		private static VectorD SolveByGEPP(MatrixD a, VectorD b)
		{
			return null;
		}

		private static VectorD SolveByLU(MatrixD a, VectorD b)
		{
			return null;
		}
	}

	public enum SLEAlgorithm
	{
		/// <summary>
		/// ガウス消去法(Gaussian Elimination)
		/// </summary>
		GE,

		/// <summary>
		/// 部分ピボット選択付きガウス消去法(Gaussian Elimination with Partial Pivoting)
		/// </summary>
		GEPP,

		/// <summary>
		/// LU分解法
		/// </summary>
		LU
	}
}
