using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umebayashi.MathEx.Algebra;

namespace Umebayashi.MathEx
{
	/// <summary>
	/// 行列クラスの基本クラス
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Matrix<T> where T: IComparable
	{
		#region constructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="rows"></param>
		/// <param name="columns"></param>
		/// <param name="byRow"></param>
		public Matrix(IEnumerable<T> data, int rows, int columns, bool byRow = false)
		{
			if (data == null)
			{
				throw new ArgumentNullException();
			}
			else if (data.Count() != rows * columns)
			{
				throw new ArgumentException("value length is not match with [row] * [columns]");
			}

			var list = new List<T[]>();
			var source = data.ToArray();
			for (int r = 0; r < rows; r++)
			{
				var array = new T[columns];
				for (int c = 0; c < columns; c++)
				{
					if (byRow)
					{
						array[c] = source[r * columns + c];
					}
					else
					{
						array[c] = source[r + c * rows];
					}
				}
				list.Add(array);
			}

			this.Data = list.ToArray();
			this.Rows = rows;
			this.Columns = columns;
			this.ByRow = byRow;
		}

		protected Matrix(T[][] data, int rows, int columns, bool byRow)
		{
			this.Data = data;
			this.Rows = rows;
			this.Columns = columns;
			this.ByRow = byRow;
		}

		#endregion

		#region field / property

		internal T[][] Data { get; set; }

		public int Rows { get; private set; }

		public int Columns { get; private set; }

		public bool ByRow { get; private set; }

		public T this[int row, int column]
		{
			get
			{
				return this.Data[row][column];
			}
		}

		#endregion

		#region method

		public override bool Equals(object obj)
		{
			if (obj is Matrix<T>)
			{
				var target = (Matrix<T>)obj;

				if (this.Rows != target.Rows || this.Columns != target.Columns)
				{
					return false;
				}
				return Enumerable.SequenceEqual(this.Data.SelectMany(x => x), target.Data.SelectMany(x => x));
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			var result = new StringBuilder();

			result.Append("[");
			for (int r = 0; r < this.Rows; r++)
			{
				result.Append("[");
				for (int c = 0; c < this.Columns; c++)
				{
					result.Append(this[r, c]);
					if (c < this.Columns - 1)
					{
						result.Append(",");
					}
				}
				result.Append("]");
				if (r < this.Rows - 1)
				{
					result.Append(",");
				}
			}
			result.Append("]");

			return result.ToString();
		}

		internal void CheckRowColumnCount(Matrix<T> other)
		{
			if (this.Rows != other.Rows)
			{
				throw new ArgumentException("行数が一致しません");
			}
			if (this.Columns != other.Columns)
			{
				throw new ArgumentException("列数が一致しません");
			}
		}

		#endregion
	}

	/// <summary>
	/// 要素がdouble型の行列クラス
	/// </summary>
	public class MatrixD : Matrix<double>
	{
		#region constructor

		public MatrixD(IEnumerable<double> data, int rows, int columns, bool byRow = false): base(data, rows, columns, byRow) { }

		protected MatrixD(double[][] data, int rows, int columns, bool byRow) : base(data, rows, columns, byRow) { }

		#endregion

		#region method

		public MatrixD Clone()
		{
			var list = new List<double[]>();
			for (int r = 0; r < this.Rows; r++)
			{
				var array = new double[this.Columns];
				Array.Copy(this.Data[r], array, array.Length);
				list.Add(array);
			}

			return new MatrixD(list.ToArray(), this.Rows, this.Columns, this.ByRow);
		}

		/// <summary>
		/// 転置行列を取得する
		/// </summary>
		/// <returns></returns>
		public MatrixD Transpose()
		{
			var newData = this.Data.Transpose().SelectMany(x => x).ToArray();
			return new MatrixD(newData, this.Columns, this.Rows, this.ByRow);
		}

		/// <summary>
		/// 行列式を計算する
		/// </summary>
		/// <returns></returns>
		public double Determinant()
		{
			if (this.Rows != this.Columns)
			{
				throw new InvalidOperationException("行数と列数が一致しない場合行列式は計算できません");
			}

			int size = this.Rows;

			// 0,1,...n-1の順列および置換符号情報を取得
			var permutations = Permutation.Calculate(size);

			var det = 0.0;
			for (int i = 0; i < permutations.Length; i++)
			{
				// a0i0*a1i1*...a(n-1)i(n-1)を計算
				var value = 1.0;
				for (int j = 0; j < size; j++)
				{
					value *= this[j, permutations[i].Vector[j]];
				}
				det += value * permutations[i].Sign;
			}

			return det;
		}

		/// <summary>
		/// 余因子を取得する
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public double Cofactor(int x, int y)
		{
			if (this.Columns != this.Rows)
			{
				throw new InvalidOperationException("正方行列でないため余因子は取得できません");
			}
			if (this.Rows < 2)
			{
				throw new InvalidOperationException("2次以上の正方行列である必要があります");
			}
			if ((x < 0) || (this.Columns <= x))
			{
				throw new IndexOutOfRangeException("xの値が範囲外です");
			}
			if ((y < 0) || (this.Rows <= y))
			{
				throw new IndexOutOfRangeException("yの値が範囲外です");
			}

			var list = new List<double>();
			for (int r = 0; r < this.Rows; r++)
			{
				if (r == y)
				{
					continue;
				}

				for (int c = 0; c < this.Columns; c++)
				{
					if (c == x)
					{
						continue;
					}

					if (this.ByRow)
					{
						list.Add(this[r, c]);
					}
					else
					{
						list.Add(this[c, r]);
					}
				}
			}

			var m = new MatrixD(list.ToArray(), this.Rows - 1, this.Columns - 1, this.ByRow);
			var sign = (x + y) % 2 == 0 ? 1 : -1;

			return m.Determinant() * sign;
		}

		/// <summary>
		/// 余因子行列を取得する
		/// </summary>
		/// <returns></returns>
		public MatrixD Cofactor()
		{
			if (this.Columns != this.Rows)
			{
				throw new InvalidOperationException("正方行列でないため余因子行列は取得できません");
			}
			if (this.Rows < 2)
			{
				throw new InvalidOperationException("2次以上の正方行列である必要があります");
			}

			var list = new List<double>();
			for (int r = 0; r < this.Rows; r++)
			{
				for (int c = 0; c < this.Columns; c++)
				{
					if (this.ByRow)
					{
						list.Add(this.Cofactor(c, r));
					}
					else
					{
						list.Add(this.Cofactor(r, c));
					}
				}
			}

			var m = new MatrixD(list.ToArray(), this.Rows, this.Columns, this.ByRow).Transpose();
			return m;
		}

		/// <summary>
		/// 逆行列を取得する
		/// </summary>
		/// <returns></returns>
		public MatrixD Inverse()
		{
			if (this.Columns != this.Rows)
			{
				throw new InvalidOperationException("正方行列でないため逆行列は取得できません");
			}
			if (this.Rows < 2)
			{
				throw new InvalidOperationException("2次以上の正方行列である必要があります");
			}

			var det = this.Determinant();
			if (det == 0)
			{
				throw new ArithmeticException("行列式が0のため逆行列を計算できません");
			}

			return (1 / det) * this.Cofactor();
		}

		/// <summary>
		/// 行列の固有値・固有ベクトルを計算する
		/// </summary>
		/// <returns></returns>
		public MatrixDEigen[] Eigens()
		{
			if (this.Columns != this.Rows)
			{
				throw new InvalidOperationException("正方行列でないため固有値・固有ベクトルは取得できません");
			}
			if (this.Rows < 2)
			{
				throw new InvalidOperationException("2次以上の正方行列である必要があります");
			}

			return null;
		}

		/// <summary>
		/// 行列をベクトルに変換する
		/// </summary>
		/// <returns></returns>
		public VectorD ToVector()
		{
			var list = new List<double>();
			for (int c = 0; c < this.Columns; c++)
			{
				for (int r = 0; r < this.Rows; r++)
				{
					list.Add(this[r, c]);
				}
			}
			return new VectorD(list.ToArray());
		}

		#endregion

		#region static method

		/// <summary>
		/// 行列を加算する
		/// </summary>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <returns></returns>
		public static MatrixD Add(MatrixD item1, MatrixD item2)
		{
		    item1.CheckRowColumnCount(item2);

			var vX = item1.ToVector();
			var vY = item2.ToVector();

			var data = new double[vX.Length];
			int index = 0;
			vX.ForEach(x => data[index++] += x);
			index = 0;
			vY.ForEach(a => data[index++] += a);

			return new MatrixD(data, item1.Rows, item1.Columns, item1.ByRow);
		}

		/// <summary>
		/// 行列を減算する
		/// </summary>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <returns></returns>
		public static MatrixD Subtract(MatrixD item1, MatrixD item2)
		{
			item1.CheckRowColumnCount(item2);

			var vX = item1.ToVector();
			var vY = item2.ToVector();

			var data = new double[vX.Length];
			int index = 0;
			vX.ForEach(x => data[index++] += x);
			index = 0;
			vY.ForEach(a => data[index++] -= a);

			return new MatrixD(data, item1.Rows, item1.Columns, item1.ByRow);
		}

		/// <summary>
		/// 行列の内積を計算する
		/// </summary>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <returns></returns>
		public static MatrixD Multiply(MatrixD item1, MatrixD item2)
		{
			if (item1 == null)
			{
				throw new ArgumentNullException("item1");
			}
			if (item2 == null)
			{
				throw new ArgumentNullException("item2");
			}
			if (item1.Columns != item2.Rows)
			{
				throw new ArgumentException("item1の列数とitem2の行数が一致していません");
			}

			var list = new List<double[]>();
			for (int r = 0; r < item1.Rows; r++)
			{
				var array = new double[item2.Columns];
				for (int c2 = 0; c2 < item2.Columns; c2++)
				{
					for (int c1 = 0; c1 < item1.Columns; c1++)
					{
						array[c2] += item1[r, c1] * item2[c1, r];
					}
				}
				list.Add(array);
			}
			var data = list.SelectMany(x => x).ToArray();

			return new MatrixD(data, item1.Rows, item2.Columns, item1.ByRow);
		}

		public static MatrixD Multiply(double coefficient, MatrixD item)
		{
			var clone = item.Clone();
			for (int r = 0; r < clone.Rows; r++)
			{
				for (int c = 0; c < clone.Columns; c++)
				{
					clone.Data[r][c] *= coefficient;
				}
			}
			return clone;
		}

		/// <summary>
		/// 行列のクロス積を計算する
		/// </summary>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <returns></returns>
		public static MatrixD CrossProd(MatrixD item1, MatrixD item2)
		{
			if (item1 == null)
			{
				throw new ArgumentNullException("x");
			}
			if (item2 == null)
			{
				item2 = item1;
			}
			else if (item1.Rows != item2.Rows || item1.Columns != item2.Columns)
			{
				throw new ArgumentException("xとyの行数または列数が一致しません");
			}

			item1 = item1.Transpose();

			return Multiply(item1, item2);
		}

		#endregion

		#region operator overload

		public static MatrixD operator +(MatrixD item1, MatrixD item2)
		{
			return Add(item1, item2);
		}

		public static MatrixD operator -(MatrixD item1, MatrixD item2)
		{
			return Subtract(item1, item2);
		}

		public static MatrixD operator *(MatrixD item1, MatrixD item2)
		{
			return Multiply(item1, item2);
		}

		public static MatrixD operator *(double coefficient, MatrixD item)
		{
			return Multiply(coefficient, item);
		}

		#endregion
	}

	/// <summary>
	/// 行列ノルムの計算方法
	/// </summary>
	public enum MatrixNormType
	{
		/// <summary>
		/// ワンノルム
		/// </summary>
		O,

		/// <summary>
		/// 無限ノルム
		/// </summary>
		I,

		/// <summary>
		/// フロベニウスノルム
		/// </summary>
		F,

		/// <summary>
		/// 最大ノルム
		/// </summary>
		M
	}

	/// <summary>
	/// 行列の固有値・固有ベクトル
	/// </summary>
	public class MatrixDEigen
	{
		/// <summary>
		/// 固有値
		/// </summary>
		public double Value { get; set; }

		/// <summary>
		/// 固有ベクトル
		/// </summary>
		public VectorD Vector { get; set; }
	}

	/// <summary>
	/// Matrixクラスの拡張メソッドを定義する
	/// </summary>
	public static class MatrixExtension
	{
		#region Norm

		/// <summary>
		/// 行列ノルムを計算する
		/// </summary>
		/// <param name="matrix"></param>
		/// <param name="normType"></param>
		/// <returns></returns>
		public static double Norm(this MatrixD matrix, MatrixNormType normType = MatrixNormType.O)
		{
			switch (normType)
			{
				case MatrixNormType.O:
					return NormO(matrix);
				case MatrixNormType.I:
					return NormI(matrix);
				case MatrixNormType.F:
					return NormF(matrix);
				case MatrixNormType.M:
					return NormM(matrix);
				default:
					return NormO(matrix);
			}
		}

		private static double NormO(MatrixD matrix)
		{
			var sum = new double[matrix.Columns];
			for (int r = 0; r < matrix.Rows; r++)
			{
				for (int c = 0; c < matrix.Columns; c++)
				{
					sum[c] += matrix[r, c];
				}
			}

			return sum.Max();
		}

		private static double NormI(MatrixD matrix)
		{
			var sum = new double[matrix.Rows];
			for (int r = 0; r < matrix.Rows; r++)
			{
				for (int c = 0; c < matrix.Columns; c++)
				{
					sum[r] += matrix[r, c];
				}
			}

			return sum.Max();
		}

		private static double NormF(MatrixD matrix)
		{
			var vector = matrix.ToVector();
			return vector.Norm();
		}

		private static double NormM(MatrixD matrix)
		{
			return matrix.ToVector().Max;
		}

		#endregion
	}
}
