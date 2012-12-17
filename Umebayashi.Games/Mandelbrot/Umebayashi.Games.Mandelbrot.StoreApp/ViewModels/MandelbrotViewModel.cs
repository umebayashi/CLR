using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Umebayashi.Games.Common.ViewModels;
using Umebayashi.Games.Mandelbrot.Core;
using Umebayashi.Games.Mandelbrot.Core.Models;

namespace Umebayashi.Games.Mandelbrot.StoreApp.ViewModels
{
	public class MandelbrotViewModel : ViewModelBase
	{
		#region constructor

		public MandelbrotViewModel()
		{
			_imageHeight = 600;
			_imageWidth = 800;

			this.Model = new MandelbrotModel(512, (int)this.ImageHeight, (int)this.ImageWidth);
		}

		#endregion

		#region field / property

		private MandelbrotModel Model { get; set; }

		#region ImageSource

		private BitmapSource _imageSource;

		public BitmapSource ImageSource
		{
			get { return _imageSource; }
			set
			{
				if (_imageSource != value)
				{
					_imageSource = value;
					this.OnPropertyChanged("ImageSource");
				}
			}
		}

		#endregion

		#region ImageHeight

		private double _imageHeight;

		public double ImageHeight
		{
			get { return _imageHeight; }
			set
			{
				if (_imageHeight != value)
				{
					_imageHeight = value;
					this.OnPropertyChanged("ImageHeight");
				}
			}
		}

		#endregion

		#region ImageWidth

		private double _imageWidth;

		public double ImageWidth
		{
			get { return _imageWidth; }
			set
			{
				if (_imageWidth != value)
				{
					_imageWidth = value;
					this.OnPropertyChanged("ImageWidth");
				}
			}
		}

		#endregion

		#endregion

		#region method (event handler)

		public void Image_Loaded(object sender, RoutedEventArgs args)
		{
			this.DrawMandelbrot();
		}

		public void Image_DoubleTapped(object sender, DoubleTappedRoutedEventArgs args)
		{
			var point = args.GetPosition((UIElement)sender);
			this.Model.ZoomIn(point.X, point.Y);

			this.DrawMandelbrot();
		}

		#endregion

		#region method

		public async void DrawMandelbrot()
		{
			//this.Model.Calculate();
			await ThreadPool.RunAsync(this.CalculateAsync);

			var bitmap = new WriteableBitmap((int)this.ImageWidth, (int)this.ImageHeight);
			byte[] buffer = new byte[4 * bitmap.PixelHeight * bitmap.PixelWidth];

			foreach (var pixel in this.Model.Pixels)
			{
				int index = 4 * ((int)pixel.ScreenPos.Y * bitmap.PixelWidth + (int)pixel.ScreenPos.X);
				buffer[index + 0] = pixel.Color.Blue;
				buffer[index + 1] = pixel.Color.Green;
				buffer[index + 2] = pixel.Color.Red;
				buffer[index + 3] = pixel.Color.Alpha;
			}

			using (var stream = bitmap.PixelBuffer.AsStream())
			{
				await stream.WriteAsync(buffer, 0, buffer.Length);
			}
			bitmap.Invalidate();

			this.ImageSource = bitmap;
		}

		private void CalculateAsync(IAsyncAction asyncAction)
		{
			this.Model.Calculate();
		}

		#endregion
	}
}
