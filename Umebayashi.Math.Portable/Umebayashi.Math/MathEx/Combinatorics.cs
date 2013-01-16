using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Umebayashi.MathEx.Algebra;

namespace Umebayashi.MathEx
{
	public static class Combinatorics
	{
		/// <summary>
		/// 順列の場合の数
		/// </summary>
		/// <param name="n"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public static long PermutationCount(long n, long x)
		{
			long result = 1;
			for (int i = 0; i < x; i++)
			{
				result = result * (n - i);
			}
			return result;
		}

		/// <summary>
		/// 組合せの場合の数
		/// </summary>
		/// <param name="n"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public static long CombinationCount(long n, long x)
		{
			return (long)(PermutationCount(n, x) / x.Factorial());
		}
	}

	public abstract class CombinatoricsGenerator<T> where T: IEquatable<T>, IComparable<T>
	{
		public CombinatoricsGenerator()
		{
			this.Result = new List<T[]>();
		}

		protected List<T[]> Result { get; set; }

		public abstract IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false);

		protected void SortResult()
		{
			this.Result.Sort((x, y) =>
			{
				for (int i = 0; i < x.Length; i++)
				{
					if (x[i].Equals(y[i])) continue;
					return x[i].CompareTo(y[i]);
				}

				return 0;
			});
		}
	}

	public class CombinationGenerator<T> : CombinatoricsGenerator<T> where T : IEquatable<T>, IComparable<T>
	{
		public CombinationGenerator()
			: base()
		{
		}

		private int SourceLength { get; set; }
		private int TargetLength { get; set; }
		private List<bool[]> Patterns { get; set; }

		/// <summary>
		/// 要素の組み合わせの一覧を作成する
		/// </summary>
		/// <param name="source"></param>
		/// <param name="length"></param>
		/// <param name="allowDuplicate"></param>
		/// <returns></returns>
		public override IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false)
		{
			this.SourceLength = source.Count();
			this.TargetLength = length;
			this.Patterns = new List<bool[]>();

			if (allowDuplicate)
			{
				throw new NotImplementedException();
			}
			else
			{
				bool[] flags = new bool[this.SourceLength];
				for (int i = 0; i < this.TargetLength; i++)
				{
					flags[i] = true;
				}
				this.EnumPatterns(flags);

				foreach (var pattern in this.Patterns)
				{
					this.Result.Add(source.Where((x, i) => pattern[i]).ToArray());
				}
			}

			this.SortResult();

			return this.Result;
		}

		private void EnumPatterns(bool[] flags)
		{
			this.Patterns.Add(flags);

			var stack = new Stack<bool[]>();
			stack.Push(flags);

			while (stack.Count() > 0)
			{
				var flags1 = stack.Pop();
				for (int i = 0; i < flags1.Length - 1; i++)
				{
					if (flags1[i] && !flags1[i + 1])
					{
						var flags2 = new bool[this.SourceLength];
						Array.Copy(flags1, flags2, flags1.Length);

						flags2[i] = flags1[i + 1];
						flags2[i + 1] = flags1[i];

						//if (!this.Patterns.Exists(x => x.SequenceEqual(flags2)))
						if (this.Patterns.Where(x => x.SequenceEqual(flags2)).Count() == 0)
						{
							this.Patterns.Add(flags2);
							stack.Push(flags2);
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// 要素の順列の一覧を作成する
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PermutationGenerator<T> : CombinatoricsGenerator<T> where T : IEquatable<T>, IComparable<T>
	{
		public PermutationGenerator(): base()
		{
		}

		public override IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false)
		{
			// 組み合わせの一覧を取得する
			var genCombination = new CombinationGenerator<T>();
			var combinations = genCombination.Generate(source, length, allowDuplicate).ToArray();

			// 置換群の一覧を取得する(組み合わせの並べ替えに使用)
			var permutationGroups = PermutationGroup.Calculate(length).ToArray();

			if (allowDuplicate)
			{
				throw new NotImplementedException();
			}
			else
			{
				foreach (var combination in combinations)
				{
					foreach (var permutationGroup in permutationGroups)
					{
						var copy = new T[combination.Length];

						for (int i = 0; i < copy.Length; i++)
						{
							copy[i] = combination[permutationGroup.Vector[i]];
						}

						this.Result.Add(copy);
					}
				}
			}

			this.SortResult();

			return this.Result;
		}
	}
}
