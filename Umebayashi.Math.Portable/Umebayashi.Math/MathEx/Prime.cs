using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx
{
	/// <summary>
	/// 素数関連ライブラリ
	/// </summary>
	public static class Prime
	{
		#region field / property

		private static long _max = 0;

		private static List<long> _primes = new List<long>();

		#endregion

		#region public method

		/// <summary>
		/// 素数の一覧を取得する
		/// </summary>
		/// <param name="max"></param>
		/// <returns></returns>
		public static IEnumerable<long> GetPrimes(long max)
		{
			//if (_primes.Count() == 0 || _primes.Max() < max)
			if (_primes.Count() == 0 || _max < max)
			{
				CalculatePrimes(max);
			}

			return _primes.Where(x => x <= max).ToArray();
		}

		/// <summary>
		/// 整数を素因数分解する
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static PrimeFactors Factorize(long value)
		{
			switch (value)
			{
				case 1:
					return new PrimeFactors { Factors = new PrimeFactor[0], Sign = 1, Value = 1 };
				case 0:
					return new PrimeFactors { Factors = new PrimeFactor[0], Sign = 1, Value = 0, IsZero = true };
				case -1:
					return new PrimeFactors { Factors = new PrimeFactor[0], Sign = -1, Value = -1 };
			}

			var val = Math.Abs(value);
			var primes = GetPrimes(val);
			if (primes.Max() == val)
			{
				return new PrimeFactors
				{
					Factors = new PrimeFactor[] { new PrimeFactor { Prime = val, Multiplier = 1 } },
					Sign = Math.Sign(value),
					Value = value,
					IsPrime = true
				};
			}

			var v = val;
			var list = new List<PrimeFactor>();
			foreach (var prime in primes)
			{
				while (v % prime == 0 && v > 1)
				{
					var factor = list.Where(x => x.Prime == prime).FirstOrDefault();
					if (factor == null)
					{
						factor = new PrimeFactor { Prime = prime, Multiplier = 0 };
						list.Add(factor);
					}
					factor.Multiplier++;

					v = (long)(v / prime);
				}
			}

			var result = new PrimeFactors { Factors = list.ToArray(), Sign = Math.Sign(value), Value = value };
			return result;
		}

		#endregion

		#region non-public method

		private static void CalculatePrimes(long max)
		{
			if (_primes.Count == 0)
			{
				_primes.Add(2);
				_max = 2;
			}

			var primes = new List<long>();
			primes.AddRange(_primes);

			var current = primes.Max();

			if (current >= max)
			{
				return;
			}

			var list = new List<long>();
			for (long i = _max + 1; i <= max; i++)
			{
				if (i % current != 0)
				{
					list.Add(i);
				}
			}

			foreach (var p in primes)
			{
				var multiplies = list.Where(x => x % p == 0);
				foreach (var m in multiplies)
				{
					if (list.Contains(m))
					{
						list.Remove(m);
					}
				}
			}

			var max_sqrt = Math.Sqrt(max);
			do
			{
				current = list.Min();
				primes.Add(current);

				var multiples = list.Where(x => x % current == 0).ToArray();
				foreach (var m in multiples)
				{
					if (list.Contains(m))
					{
						list.Remove(m);
					}
				}
			} while (current < max_sqrt);

			_primes.Clear();
			_primes.AddRange(primes);
			_primes.AddRange(list);

			_max = max;
		}

		#endregion
	}

	/// <summary>
	/// 素因数
	/// </summary>
	public class PrimeFactor
	{
		/// <summary>
		/// 素数
		/// </summary>
		public long Prime { get; set; }

		/// <summary>
		/// 乗数
		/// </summary>
		public long Multiplier { get; set; }

		#region method

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("({0}^{1})", this.Prime, this.Multiplier);
		}

		#endregion
	}

	/// <summary>
	/// 整数を素因数の積と符合で表現した構造体
	/// </summary>
	public class PrimeFactors
	{
		#region property

		/// <summary>
		/// 素因数
		/// </summary>
		public PrimeFactor[] Factors { get; set; }

		/// <summary>
		/// 符合
		/// </summary>
		public int Sign { get; set; }

		/// <summary>
		/// 元の整数値
		/// </summary>
		public long Value { get; set; }

		/// <summary>
		/// 0かどうか
		/// </summary>
		public bool IsZero { get; set; }

		/// <summary>
		/// 素数かどうか
		/// </summary>
		public bool IsPrime { get; set; }

		#endregion

		#region method

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			var value = new StringBuilder();

			value.AppendFormat("(Value:{0}, Sign:{1}, IsZero:{2}, IsPrime:{3} Factors:[", this.Value, this.Sign, this.IsZero, this.IsPrime);
			for (int i = 0; i < this.Factors.Length; i++)
			{
				value.Append(this.Factors[i].ToString());
			}
			value.Append("])");

			return value.ToString();
		}

		#endregion
	}

	/// <summary>
	/// 有理数
	/// </summary>
	public class Rational
	{
		/// <summary>
		/// 分子
		/// </summary>
		public PrimeFactors Numerator { get; set; }

		/// <summary>
		/// 分母
		/// </summary>
		public PrimeFactors Denominator { get; set; }

		/// <summary>
		/// 符合
		/// </summary>
		public int Sign { get; set; }
	}
}
