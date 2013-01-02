using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.Games.Mandelbrot.Core.Models
{
	public class CoordinateManager
	{
		public CoordinateManager(int screenHeight, int screenWidth, Point defaultLeftTop, Point defaultRightBottom)
		{
			this.ScreenHeight = screenHeight;
			this.ScreenWidth = screenWidth;
			this.DefaultLeftTop = defaultLeftTop;
			this.DefaultRightBottom = defaultRightBottom;

			this.ZoomLevel = 0;
			this.XMin = this.DefaultLeftTop.X;
			this.YMax = this.DefaultLeftTop.Y;
			this.XMax = this.DefaultRightBottom.X;
			this.YMin = this.DefaultRightBottom.Y;
			this.XDiff = (this.XMax - this.XMin) / (double)this.ScreenWidth;
			this.YDiff = (this.YMax - this.YMin) / (double)this.ScreenHeight;
		}

		/// <summary>
		/// 
		/// </summary>
		public int ScreenHeight { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public int ScreenWidth { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public Point DefaultLeftTop { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public Point DefaultRightBottom { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public int ZoomLevel { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public double XDiff { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public double YDiff { get; private set; }

		/// <summary>
		/// X軸の最大値
		/// </summary>
		public double XMax { get; private set; }

		/// <summary>
		/// X軸の最小値
		/// </summary>
		public double XMin { get; private set; }

		/// <summary>
		/// Y軸の最大値
		/// </summary>
		public double YMax { get; private set; }

		/// <summary>
		/// Y軸の最小値
		/// </summary>
		public double YMin { get; private set; }

		/// <summary>
		/// コントロール上の座標を数値座標に変換する
		/// </summary>
		/// <param name="screenPos"></param>
		/// <returns></returns>
		public Point ScreenToCoord(int screenX, int screenY)
		{
			var coordX = this.XMin + screenX * this.XDiff;
			var coordY = this.YMax - screenY * this.YDiff;
			return new Point(coordX, coordY);
		}

		public void ZoomIn(int screenX, int screenY)
		{
			this.ZoomLevel++;
			var c = this.ScreenToCoord(screenX, screenY);

			double p = Math.Pow(0.1, this.ZoomLevel);
			double coordWRange = p * (this.DefaultRightBottom.X - this.DefaultLeftTop.X);
			double coordHRange = p * (this.DefaultLeftTop.Y - this.DefaultRightBottom.Y);

			this.XMin = c.X - (coordWRange / 2.0);
			this.YMax = c.Y + (coordHRange / 2.0);
			this.XMax = this.XMin + coordWRange;
			this.YMin = this.YMax - coordHRange;
			this.XDiff = (this.XMax - this.XMin) / (double)this.ScreenWidth;
			this.YDiff = (this.YMax - this.YMin) / (double)this.ScreenHeight;
		}

		public void ZoomOut(Point center)
		{
		}
	}
}
