using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umebayashi.MathEx;

namespace Umebayashi.Games.Mandelbrot.Core.Models
{
	public class MandelbrotModel
	{
		#region constructor

		public MandelbrotModel(int calculationLimit, int screenHeight, int screenWidth)
		{
			this.CalculationLimit = calculationLimit;
			this.Pixels = new List<Pixel>();

			this.CoordManager = new CoordinateManager(
				screenHeight, 
				screenWidth, 
				new Point(-2.5, 1.5), 
				new Point(1.5, -1.5));
			this.Palette = new ColorPalette();
		}

		#endregion

		#region field / property

		public int CalculationLimit 
		{ 
			get; 
			private set; 
		}

		public IList<Pixel> Pixels
		{
			get;
			private set;
		}

		private CoordinateManager CoordManager
		{
			get;
			set;
		}

		private ColorPalette Palette
		{
			get;
			set;
		}

		#endregion

		#region method

		public void Calculate()
		{
			this.Pixels.Clear();

			for (int y = 0; y < this.CoordManager.ScreenHeight; y++)
			{
				for (int x = 0; x < this.CoordManager.ScreenWidth; x++)
				{
					Point coordPos = this.CoordManager.ScreenToCoord(x, y);
					int colorKey = this.CalculateInternal(coordPos);

					Pixel pixel = this.GetPixel(new Point(x, y), coordPos, colorKey);
					this.Pixels.Add(pixel);
				}
			}
		}

		private int CalculateInternal(Point coord)
		{
			Complex c = new Complex(coord.X, coord.Y);
			Complex z = new Complex(0, 0);
			for (int count = 0; count < this.CalculationLimit; count++)
			{
				if (z.Norm > 4.0)
				{
					return count;
				}
				z = z * z + c;
			}
			return -1;
		}

		private Pixel GetPixel(Point screenPos, Point coordPos, int colorKey)
		{
			var pixel = new Pixel
			{
				ScreenPos = screenPos,
				CoordPos = coordPos,
				Color = this.Palette.GetColor(colorKey)
			};

			return pixel;
		}

		public void ZoomIn(double screenX, double screenY)
		{
			this.CoordManager.ZoomIn((int)screenX, (int)screenY);
		}

		public void ZoomOut(Point screenCenter)
		{
		}

		#endregion
	}
}
