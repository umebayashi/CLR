using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Umebayashi.Games.Mandelbrot.StoreApp.ViewModels;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Umebayashi.Games.Mandelbrot.StoreApp.Controls
{
	public sealed partial class MandelbrotViewer : UserControl
	{
		public MandelbrotViewer()
		{
			this.InitializeComponent();

			var vm = new MandelbrotViewModel();
			this.imgMain.DataContext = vm;
			this.imgMain.Loaded += vm.Image_Loaded;
			this.imgMain.DoubleTapped += vm.Image_DoubleTapped;
		}
	}
}
