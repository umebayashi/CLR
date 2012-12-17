using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.Games.Mandelbrot.Core.Models
{
	public struct Pixel
	{
		public Point CoordPos { get; set; }

		public Point ScreenPos { get; set; }

		public Color Color { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is Pixel)
			{
				var target = (Pixel)obj;
				return (this.CoordPos.Equals(target.CoordPos) && this.ScreenPos.Equals(target.ScreenPos) && this.Color.Equals(target.Color));
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("CoordPos:{0}, ScreenPos:{1}, Color:{2}", this.CoordPos, this.ScreenPos, this.Color);
		}
	}
}
