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

		public static int CALL_EDITPATTERNS_A = 0;
		public static int CALL_EDITPATTERNS_B = 0;

		private List<T[]> Result { get; set; }
		private int SourceLength { get; set; }
		private int TargetLength { get; set; }
		private List<bool[]> Patterns { get; set; }

		public IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false)
		{
			CALL_EDITPATTERNS_A = 0;
			CALL_EDITPATTERNS_B = 0;

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
				this.Patterns.Add(flags);

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
			CALL_EDITPATTERNS_A++;

			for (int i = 0; i < flags.Length - 1; i++)
			{
				if (flags[i] && !flags[i + 1])
				{
					this.EnumPatterns(flags, i);
				}
			}
		}

		private void EnumPatterns(bool[] flags, int index)
		{
			CALL_EDITPATTERNS_B++;

			var flags2 = new bool[this.SourceLength];
			Array.Copy(flags, flags2, flags.Length);

			flags2[index] = flags[index + 1];
			flags2[index + 1] = flags[index];

			if (!this.Patterns.Exists(x => x.SequenceEqual(flags2)))
			{
				this.Patterns.Add(flags2);
				EnumPatterns(flags2);
			}
		}
	}
}
