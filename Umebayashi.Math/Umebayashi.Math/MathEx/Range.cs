using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx
{
	public abstract class Range<T, S> : IEnumerable<T>
	{
		public Range(T start, T end, S step)
		{
			this.Start = start;
			this.End = end;
			this.Step = step;
		}

		public T Start { get; private set; }

		public T End { get; private set; }

		public S Step { get; private set; }

		public abstract IEnumerator<T> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}

	public class RangeD : Range<double, double>
	{
		public RangeD(double start, double end, double step)
			: base(start, end, step)
		{
		}

		public override IEnumerator<double> GetEnumerator()
		{
			decimal mStart = Convert.ToDecimal(this.Start);
			decimal mEnd = Convert.ToDecimal(this.End);
			decimal mStep = Convert.ToDecimal(this.Step);
			decimal mValue = mStart;

			while (mValue <= mEnd)
			{
				yield return Convert.ToDouble(mValue);
				mValue += mStep;
			}
		}
	}
}
