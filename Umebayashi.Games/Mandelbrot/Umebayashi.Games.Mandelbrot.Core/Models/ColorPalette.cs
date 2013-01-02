using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.Games.Mandelbrot.Core.Models
{
	public class ColorPalette
	{
		public ColorPalette()
		{
			this.ColorMap = new Dictionary<int, Color>();
			this.Initialize();
		}

		private Dictionary<int, Color> ColorMap
		{
			get;
			set;
		}

		public void Initialize()
		{
			this.ColorMap.Clear();
			this.ColorMap.Add(-1, new Color(0, 0, 0, 255));

			int count = 0;
			for (byte r = 1; r <= 8; r++)
			{
				for (byte g = 1; g <= 8; g++)
				{
					for (byte b = 1; b <= 8; b++)
					{
						this.ColorMap.Add(count++, new Color((byte)(r * 32 - 1), (byte)(g * 32 - 1), (byte)(b * 32 - 1), 255));
					}
				}
			}
		}

		public Color GetColor(int key)
		{
			return this.ColorMap[key];
		}
	}
}
