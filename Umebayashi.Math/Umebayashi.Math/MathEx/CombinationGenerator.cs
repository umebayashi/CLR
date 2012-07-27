using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.MathEx
{
	public class CombinationGenerator<T> where T: IEquatable<T>, IComparable<T>
	{
		public CombinationGenerator()
		{
		}

		private List<T[]> Result { get; set; }
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
		public IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false)
		{
			this.Result = new List<T[]>();
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

			this.Result.Sort((x, y) =>
			{
				for (int i = 0; i < x.Length; i++)
				{
					if (x[i].Equals(y[i])) continue;
					return x[i].CompareTo(y[i]);
				}

				return 0;
			});

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

						if (!this.Patterns.Exists(x => x.SequenceEqual(flags2)))
						{
							this.Patterns.Add(flags2);
							stack.Push(flags2);
						}
					}
				}
			}
		}
	}
}
