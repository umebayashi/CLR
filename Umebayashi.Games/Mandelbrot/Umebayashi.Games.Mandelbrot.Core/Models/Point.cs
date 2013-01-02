using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.Games.Mandelbrot.Core.Models
{
	public struct Point
	{
		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X;

		public double Y;

		public override bool Equals(object obj)
		{
			if (obj is Point)
			{
				var target = (Point)obj;
				return (X == target.X) && (Y == target.Y);
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("(X:{0}, Y:{1})", X, Y);
		}
	}
}
