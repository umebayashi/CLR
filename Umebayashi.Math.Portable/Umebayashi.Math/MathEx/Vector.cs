using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx
{
	public abstract class Vector<T> where T : IComparable
	{
		#region constructor

		public Vector(params T[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException();
			}
			var temp = values.ToArray();
			this.Data = (T[])temp.Clone();
		}

		#endregion

		#region field / property

		protected internal T[] Data { get; private set; }

		public T this[int index]
		{
			get
			{
				return this.Data[index];
			}
			set
			{
				this.Data[index] = value;
			}
		}

		/// <summary>
		/// ベクトルの要素の個数
		/// </summary>
		public int Length
		{
			get { return this.Data.Length; }
		}

		/// <summary>
		/// ベクトルの要素の最大値
		/// </summary>
		public T Max
		{
			get { return this.Data.Max(x => x); }
		}

		/// <summary>
		/// ベクトルの要素の最小値
		/// </summary>
		public T Min
		{
			get { return this.Data.Min(x => x); }
		}

		#endregion

		#region instance method

		public override bool Equals(object obj)
		{
			if (obj is Vector<T>)
			{
				var target = (Vector<T>)obj;
				if (this.Length == target.Length)
				{
					for (int i = 0; i < this.Length; i++)
					{
						if (!this[i].Equals(target[i]))
						{
							return false;
						}
					}
					return true;
				}
				return false;
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
			for (int i = 0; i < this.Length; i++)
			{
				result.Append(this[i]);
				if (i < this.Length - 1)
				{
					result.Append(", ");
				}
			}
			result.Append("]");

			return result.ToString();
		}

		/// <summary>
		/// ベクトルの要素に対して順番に処理を行う
		/// </summary>
		/// <param name="action"></param>
		public void ForEach(Action<T> action)
		{
			//this.Data.ToList().ForEach(action);
			foreach (var item in this.Data)
			{
				action(item);
			}
		}

		/// <summary>
		/// ベクトルの要素に対してアキュムレータ関数を適用する
		/// </summary>
		/// <param name="func"></param>
		public T Aggregate(Func<T, T, T> func)
		{
			return this.Data.Aggregate(func);
		}

		/// <summary>
		/// ベクトルの要素のソートを行う
		/// </summary>
		/// <param name="descending"></param>
		public void Sort(bool descending = false)
		{
			if (descending)
			{
				this.Data = this.Data.OrderByDescending(x => x).ToArray();
			}
			else
			{
				this.Data = this.Data.OrderBy(x => x).ToArray();
			}
		}

		/// <summary>
		/// ベクトルの要素を交換する
		/// </summary>
		/// <param name="index1"></param>
		/// <param name="index2"></param>
		public void Swap(int index1, int index2)
		{
			var tmp = this[index1];
			this[index1] = this[index2];
			this[index2] = tmp;
		}

		#endregion

		#region static method

		#endregion
	}

	/// <summary>
	/// 
	/// </summary>
	public class VectorD : Vector<double>
	{
		#region constructor

		public VectorD(params double[] values) : base(values) { }

		#endregion

		#region method

		public VectorD Clone()
		{
			return new VectorD(this.Data);
		}

		/// <summary>
		/// ベクトルの和集合を返す
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public VectorD Union(VectorD target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}

			var union = this.Data.Union(target.Data).ToArray();
			return new VectorD(union);
		}

		/// <summary>
		/// ベクトルの要素の最大値と最小値を取得する
		/// </summary>
		/// <returns></returns>
		public VectorD Intersect(VectorD target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}

			var intersect = this.Data.Intersect(target.Data).ToArray();
			return new VectorD(intersect);
		}

		/// <summary>
		/// ベクトルを行列に変換する
		/// </summary>
		/// <param name="rows"></param>
		/// <param name="columns"></param>
		/// <returns></returns>
		public MatrixD ToMatrix(int rows, int columns)
		{
			return new MatrixD(this.Data, rows, columns);
		}

		/// <summary>
		/// Householder変換
		/// </summary>
		/// <param name="firstValue">変換後のベクトルの最初の成分</param>
		/// <returns></returns>
		public VectorD HouseholderTransform(out double firstValue)
		{
			double[] array = new double[this.Length];
			Array.Copy(this.Data, array, this.Length);

			double norm = this.Norm();
			if (norm != 0)
			{
				if (array[0] < 0) { norm = -norm; }
				array[0] += norm;
				double weight = 1.0 / Math.Sqrt(array[0] * norm * 2);
				for (int i = 0; i < array.Length; i++)
				{
					array[i] *= weight;
				}
			}
			firstValue = -norm;

			return new VectorD(array);
		}

		#endregion

		#region operator

		/// <summary>
		/// ベクトルから行列への変換演算子
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static explicit operator MatrixD(VectorD v)
		{
			return new MatrixD(v.Data, v.Length, 1);
		}

		/// <summary>
		/// ベクトルの和を計算する
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		public static VectorD operator +(VectorD v1, VectorD v2)
		{
			if (v1.Length != v2.Length)
			{
				throw new InvalidOperationException("ベクトルの長さが異なります");
			}

			var result = new VectorD(new double[v1.Length]);
			for (int i = 0; i < v1.Length; i++)
			{
				result[i] = v1[i] + v2[i];
			}
			return result;
		}

		/// <summary>
		/// ベクトルの差を計算する
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		public static VectorD operator -(VectorD v1, VectorD v2)
		{
			if (v1.Length != v2.Length)
			{
				throw new InvalidOperationException("ベクトルの長さが異なります");
			}

			var result = new VectorD(new double[v1.Length]);
			for (int i = 0; i < v1.Length; i++)
			{
				result[i] = v1[i] - v2[i];
			}
			return result;
		}

		/// <summary>
		/// ベクトルの積(内積)を計算する
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		public static double operator *(VectorD v1, VectorD v2)
		{
			if (v1.Length != v2.Length)
			{
				throw new InvalidOperationException("ベクトルの長さが異なります");
			}

			double result = 0;
			for (int i = 0; i < v1.Length; i++)
			{
				result += v1[i] * v2[i];
			}

			return result;
		}

		/// <summary>
		/// ベクトルとスカラーの積を計算する
		/// </summary>
		/// <param name="v"></param>
		/// <param name="d"></param>
		/// <returns></returns>
		public static VectorD operator *(VectorD v, double d)
		{
			var result = new VectorD(new double[v.Length]);
			for (int i = 0; i < v.Length; i++)
			{
				result[i] = v[i] * d;
			}

			return result;
		}

		/// <summary>
		/// ベクトルとスカラーの商を計算する
		/// </summary>
		/// <param name="v"></param>
		/// <param name="d"></param>
		/// <returns></returns>
		public static VectorD operator /(VectorD v, double d)
		{
			var result = new VectorD(new double[v.Length]);
			for (int i = 0; i < v.Length; i++)
			{
				result[i] = v[i] / d;
			}

			return result;
		}

		#endregion
	}

	/// <summary>
	/// 
	/// </summary>
	public class VectorM : Vector<decimal>
	{
		#region constructor

		public VectorM(params decimal[] values) : base(values) { }

		#endregion

		#region static method

		#endregion
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class VectorC : Vector<ComplexD>
	{
		#region constructor

		public VectorC(params ComplexD[] values) : base(values) { }

		#endregion

		#region static method

		#endregion
	}

	public class VectorI : Vector<int>
	{
		#region constructor

		public VectorI(params int[] values) : base(values) { }

		#endregion

		#region static method

		#endregion
	}

	/// <summary>
	/// Vectorクラスの拡張メソッドを定義する
	/// </summary>
	public static class VectorExtension
	{
		#region Prod

		/// <summary>
		/// ベクトル要素の積を求める
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static double Prod(this VectorD vector)
		{
			double value = 1.0;
			vector.ForEach(x => value *= x);

			return value;
		}

		/// <summary>
		/// ベクトル要素の積を求める
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static decimal Prod(this VectorM vector)
		{
			decimal value = 1m;
			vector.ForEach(x => value *= x);

			return value;
		}

		/// <summary>
		/// ベクトル要素の積を求める
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static int Prod(this VectorI vector)
		{
			int value = 1;
			vector.ForEach(x => value *= x);

			return value;
		}

		/// <summary>
		/// ベクトル要素の積を求める
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static ComplexD Prod(this VectorC vector)
		{
			ComplexD value = new ComplexD();
			vector.ForEach(x => value *= x);

			return value;
		}

		#endregion

		#region Sum

		/// <summary>
		/// ベクトルの要素の和を求める
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static double Sum(this VectorD vector)
		{
			double value = 0.0;
			vector.ForEach(x => value += x);

			return value;
		}

		/// <summary>
		/// ベクトルの要素の和を求める
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static decimal Sum(this VectorM vector)
		{
			decimal value = 0m;
			vector.ForEach(x => value += x);

			return value;
		}

		/// <summary>
		/// ベクトルの要素の和を求める
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static int Sum(this VectorI vector)
		{
			int value = 0;
			vector.ForEach(x => value += x);

			return value;
		}

		/// <summary>
		/// ベクトルの要素の和を求める
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static ComplexD Sum(this VectorC vector)
		{
			var value = new ComplexD();
			vector.ForEach(x => value += x);

			return value;
		}

		#endregion

		#region Norm

		public static double Norm(this VectorD vector)
		{
			double value = 0.0;
			vector.ForEach(x => value += x * x);

			return Math.Sqrt(value);
		}

		public static double Norm(this VectorM vector)
		{
			double value = 0.0;
			vector.ForEach(x => value += (double)(x * x));

			return Math.Sqrt(value);
		}

		public static double Norm(this VectorI vector)
		{
			double value = 0.0;
			vector.ForEach(x => value += x * x);

			return Math.Sqrt(value);
		}

		#endregion
	}
}
 