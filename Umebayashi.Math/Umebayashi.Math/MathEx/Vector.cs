using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysMath = System.Math;

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

		protected T[] Data { get; private set; }

		public T this[int index]
		{
			get
			{
				return this.Data[index];
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
			this.Data.ToList().ForEach(action);
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

		#endregion

		#region static method

		public static explicit operator MatrixD(VectorD v)
		{
			return new MatrixD(v.Data, v.Length, 1);
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

			return SysMath.Sqrt(value);
		}

		public static double Norm(this VectorM vector)
		{
			double value = 0.0;
			vector.ForEach(x => value += (double)(x * x));

			return SysMath.Sqrt(value);
		}

		public static double Norm(this VectorI vector)
		{
			double value = 0.0;
			vector.ForEach(x => value += x * x);

			return SysMath.Sqrt(value);
		}

		#endregion
	}
}
 