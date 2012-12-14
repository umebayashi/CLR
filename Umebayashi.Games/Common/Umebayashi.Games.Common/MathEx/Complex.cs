using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx
{
	public class Complex : IEquatable<Complex>, IFormattable
	{
		#region constructor

		public Complex(double real, double imaginary)
		{
			this.Real = real;
			this.Imaginary = imaginary;
		}

		#endregion

		#region field / property

		/// <summary>
		/// 実数部
		/// </summary>
		public double Real { get; private set; }

		/// <summary>
		/// 虚数部
		/// </summary>
		public double Imaginary { get; private set; }

		/// <summary>
		/// ノルム
		/// </summary>
		public double Norm
		{
			get
			{
				return this.Real * this.Real + this.Imaginary * this.Imaginary;
			}
		}

		#endregion

		#region method

		#region Object

		public override bool Equals(object obj)
		{
			var other = obj as Complex;
			if (other != null)
			{
				return this.Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0} + {1}i", this.Real, this.Imaginary);
		}

		#endregion

		#region IEquatable<Complex>

		public bool Equals(Complex other)
		{
			return (this.Real == other.Real) && (this.Imaginary == other.Imaginary);
		}

		#endregion

		#region IFormattable

		public string ToString(string format, IFormatProvider formatProvider)
		{
			throw new NotImplementedException();
		}

		#endregion

		#endregion

		#region static method

		public static Complex Add(Complex x, Complex y)
		{
			return new Complex(x.Real + y.Real, x.Imaginary + y.Imaginary);
		}

		public static Complex Subtract(Complex x, Complex y)
		{
			return new Complex(x.Real - y.Real, x.Imaginary - y.Imaginary);
		}

		public static Complex Multiply(Complex x, Complex y)
		{
			return new Complex(x.Real * y.Real - x.Imaginary * y.Imaginary, x.Real * y.Imaginary + x.Imaginary * y.Real);
		}

		public static Complex Divide(Complex x, Complex y)
		{
			var real = (x.Real * y.Real + x.Imaginary * y.Imaginary) / (y.Real * y.Real + y.Imaginary * y.Imaginary);
			var imaginary = (x.Imaginary * y.Real - x.Real * y.Imaginary) / (y.Real * y.Real + y.Imaginary * y.Imaginary);
			return new Complex(real, imaginary);
		}

		#endregion

		#region operator overload

		public static Complex operator +(Complex x, Complex y)
		{
			return Add(x, y);
		}

		public static Complex operator -(Complex x, Complex y)
		{
			return Subtract(x, y);
		}

		public static Complex operator *(Complex x, Complex y)
		{
			return Multiply(x, y);
		}

		public static Complex operator /(Complex x, Complex y)
		{
			return Divide(x, y);
		}

		public static bool operator ==(Complex x, Complex y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(Complex x, Complex y)
		{
			return !x.Equals(y);
		}

		#endregion
	}
}
