using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx
{
	public class Complex<T> : IComparable, IComparable<Complex<T>> where T: IComparable
	{
		#region constructor

		public Complex() : this(default(T), default(T))
		{
		}

		public Complex(T real, T imaginary)
		{
			this.Real = real;
			this.Imaginary = imaginary;
		}

		#endregion

		#region field / property

		/// <summary>
		/// 実数部
		/// </summary>
		public T Real { get; private set; }

		/// <summary>
		/// 虚数部
		/// </summary>
		public T Imaginary { get; private set; }

		#endregion

		#region instance method

		public override bool Equals(object obj)
		{
			if (obj is Complex<T>)
			{
				var target = (Complex<T>)obj;
				return (this.Real.Equals(target.Real) && this.Imaginary.Equals(target.Imaginary));
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0} {1}i", this.Real, this.Imaginary);
		}

		public int CompareTo(object obj)
		{
			return this.CompareTo((Complex<T>)obj);
		}

		public int CompareTo(Complex<T> other)
		{
			if (!this.Real.Equals(other.Real))
			{
				return this.Real.CompareTo(other.Real);
			}
			return this.Imaginary.CompareTo(other.Imaginary);
		}

		#endregion
	}

	public class ComplexD : Complex<double>
	{
		#region constructor

		public ComplexD() : base() { }

		public ComplexD(double real, double imaginary) : base(real, imaginary) { }

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

		#endregion

		#region static method

		public static ComplexD Add(ComplexD x, ComplexD y)
		{
			return new ComplexD(x.Real + y.Real, x.Imaginary + y.Imaginary);
		}

		public static ComplexD Subtract(ComplexD x, ComplexD y)
		{
			return new ComplexD(x.Real - y.Real, x.Imaginary - y.Imaginary);
		}

		public static ComplexD Multiply(ComplexD x, ComplexD y)
		{
			return new ComplexD(x.Real * y.Real - x.Imaginary * y.Imaginary, x.Real * y.Imaginary + x.Imaginary * y.Real);
		}

		public static ComplexD Divide(ComplexD x, ComplexD y)
		{
			var real = (x.Real * y.Real + x.Imaginary * y.Imaginary) / (y.Real * y.Real + y.Imaginary * y.Imaginary);
			var imaginary = (x.Imaginary * y.Real - x.Real * y.Imaginary) / (y.Real * y.Real + y.Imaginary * y.Imaginary);
			return new ComplexD(real, imaginary);
		}

		#endregion

		#region operator overload

		public static ComplexD operator +(ComplexD x, ComplexD y)
		{
			return Add(x, y);
		}

		public static ComplexD operator -(ComplexD x, ComplexD y)
		{
			return Subtract(x, y);
		}

		public static ComplexD operator *(ComplexD x, ComplexD y)
		{
			return Multiply(x, y);
		}

		public static ComplexD operator /(ComplexD x, ComplexD y)
		{
			return Divide(x, y);
		}

		public static bool operator ==(ComplexD x, ComplexD y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(ComplexD x, ComplexD y)
		{
			return !x.Equals(y);
		}

		#endregion
	}

	public class ComplexM : Complex<decimal>
	{
		#region constructor

		public ComplexM() : base() { }

		public ComplexM(decimal real, decimal imaginary) : base(real, imaginary) { }

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

		#endregion

		#region static method

		public static ComplexM Add(ComplexM x, ComplexM y)
		{
			return new ComplexM(x.Real + y.Real, x.Imaginary + y.Imaginary);
		}

		public static ComplexM Subtract(ComplexM x, ComplexM y)
		{
			return new ComplexM(x.Real - y.Real, x.Imaginary - y.Imaginary);
		}

		public static ComplexM Multiply(ComplexM x, ComplexM y)
		{
			return new ComplexM(x.Real * y.Real - x.Imaginary * y.Imaginary, x.Real * y.Imaginary + x.Imaginary * y.Real);
		}

		public static ComplexM Divide(ComplexM x, ComplexM y)
		{
			var real = (x.Real * y.Real + x.Imaginary * y.Imaginary) / (y.Real * y.Real + y.Imaginary * y.Imaginary);
			var imaginary = (x.Imaginary * y.Real - x.Real * y.Imaginary) / (y.Real * y.Real + y.Imaginary * y.Imaginary);
			return new ComplexM(real, imaginary);
		}

		#endregion

		#region operator overload

		public static ComplexM operator +(ComplexM x, ComplexM y)
		{
			return Add(x, y);
		}

		public static ComplexM operator -(ComplexM x, ComplexM y)
		{
			return Subtract(x, y);
		}

		public static ComplexM operator *(ComplexM x, ComplexM y)
		{
			return Multiply(x, y);
		}

		public static ComplexM operator /(ComplexM x, ComplexM y)
		{
			return Divide(x, y);
		}

		public static bool operator ==(ComplexM x, ComplexM y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(ComplexM x, ComplexM y)
		{
			return !x.Equals(y);
		}

		#endregion
	}

	public class ComplexI : Complex<int>
	{
		#region constructor

		public ComplexI() : base() { }

		public ComplexI(int real, int imaginary) : base(real, imaginary) { }

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

		#endregion

		#region static method

		public static ComplexI Add(ComplexI x, ComplexI y)
		{
			return new ComplexI(x.Real + y.Real, x.Imaginary + y.Imaginary);
		}

		public static ComplexI Subtract(ComplexI x, ComplexI y)
		{
			return new ComplexI(x.Real - y.Real, x.Imaginary - y.Imaginary);
		}

		public static ComplexI Multiply(ComplexI x, ComplexI y)
		{
			return new ComplexI(x.Real * y.Real - x.Imaginary * y.Imaginary, x.Real * y.Imaginary + x.Imaginary * y.Real);
		}

		public static ComplexI Divide(ComplexI x, ComplexI y)
		{
			throw new InvalidOperationException("Divide operation is not supported");
		}

		#endregion

		#region operator overload

		public static ComplexI operator +(ComplexI x, ComplexI y)
		{
			return Add(x, y);
		}

		public static ComplexI operator -(ComplexI x, ComplexI y)
		{
			return Subtract(x, y);
		}

		public static ComplexI operator *(ComplexI x, ComplexI y)
		{
			return Multiply(x, y);
		}

		public static ComplexI operator /(ComplexI x, ComplexI y)
		{
			return Divide(x, y);
		}

		public static bool operator ==(ComplexI x, ComplexI y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(ComplexI x, ComplexI y)
		{
			return !x.Equals(y);
		}

		#endregion
	}
}
