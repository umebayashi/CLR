using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
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
			_model = new MandelbrotModel(512);
			_imageHeight = 512;
			_imageWidth = 512;
		}

		#endregion

		#region field / property

		private MandelbrotModel _model;

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
			this.InitMandelbrot();
		}

		public void Image_DoubleTapped(object sender, DoubleTappedRoutedEventArgs args)
		{
			var point = args.GetPosition((UIElement)sender);
		}

		#endregion

		#region method

		public async void InitMandelbrot()
		{
			var bitmap = new WriteableBitmap((int)this.ImageWidth, (int)this.ImageHeight);
			byte[] pixels = new byte[4 * bitmap.PixelHeight * bitmap.PixelWidth];

			for (int y = 0; y < bitmap.PixelHeight; y++)
			{
				for (int x = 0; x < bitmap.PixelWidth; x++)
				{
					int index = 4 * (y * bitmap.PixelWidth + x);
					pixels[index + 0] = 255;	// Blue
					pixels[index + 1] = 0;		// Green
					pixels[index + 2] = 255;	// Red
					pixels[index + 3] = 255;	// Alpha
				}
			}

			using (var stream = bitmap.PixelBuffer.AsStream())
			{
				await stream.WriteAsync(pixels, 0, pixels.Length);
			}
			bitmap.Invalidate();

			this.ImageSource = bitmap;
		}

		#endregion
	}
}
