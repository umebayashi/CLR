using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.Games.Mandelbrot.Core.Models
{
	public struct Color
	{
		public Color(byte r, byte g, byte b, byte a)
		{
			Red = r;
			Green = g;
			Blue = b;
			Alpha = a;
		}

		public byte Red;

		public byte Green;

		public byte Blue;

		public byte Alpha;

		public override bool Equals(object obj)
		{
			if (obj is Color)
			{
				var target = (Color)obj;
				return (Red == target.Red) && (Green == target.Green) && (Blue == target.Blue) && (Alpha == target.Alpha);
			}

			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("(R:{0}, G:{1}, B:{2}, A:{3}", Red, Green, Blue, Alpha);
		}
	}
}
