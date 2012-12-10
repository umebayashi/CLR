using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace ColorTile.StoreApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

			this.DataContext = _viewModel;
			this.rdoRed.IsChecked = true;
			this.CreateRectangles();

			DisplayProperties.OrientationChanged += DisplayProperties_OrientationChanged;
			Window.Current.SizeChanged += Current_SizeChanged;
        }

		void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			//throw new NotImplementedException();
			switch (ApplicationView.Value)
			{
				case ApplicationViewState.Filled:
					break;
				case ApplicationViewState.FullScreenLandscape:
					break;
				case ApplicationViewState.FullScreenPortrait:
					break;
				case ApplicationViewState.Snapped:
					break;
			}
		}

		void DisplayProperties_OrientationChanged(object sender)
		{
			var orientation = DisplayProperties.CurrentOrientation;
			switch (orientation)
			{
				case DisplayOrientations.Landscape:
				case DisplayOrientations.LandscapeFlipped:
				case DisplayOrientations.None:
					sldAlpha.Width = 1024;
					sldRed.Width = 1024;
					sldGreen.Width = 1024;
					sldBlue.Width = 1024;
					break;

				case DisplayOrientations.Portrait:
				case DisplayOrientations.PortraitFlipped:
					sldAlpha.Width = 512;
					sldRed.Width = 512;
					sldGreen.Width = 512;
					sldBlue.Width = 512;
					break;
			}
		}

		private ColorTileViewModel _viewModel = new ColorTileViewModel();

        /// <summary>
        /// このページがフレームに表示されるときに呼び出されます。
        /// </summary>
        /// <param name="e">このページにどのように到達したかを説明するイベント データ。Parameter 
        /// プロパティは、通常、ページを構成するために使用します。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

		private void CreateRectangles()
		{
			int size = _viewModel.Size;
			int unitlen = _viewModel.UnitLength;

			this.grdColor.RowDefinitions.Clear();
			this.grdColor.ColumnDefinitions.Clear();
			for (int r = 0; r < size; r++)
			{
				this.grdColor.RowDefinitions.Add(new RowDefinition { Height = new GridLength(unitlen) });
			}
			for (int c = 0; c < size; c++)
			{
				this.grdColor.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(unitlen) });
			}

			for (int r = 0; r < size; r++)
			{
				for (int c = 0; c < size; c++)
				{
					var item = _viewModel.GetItem(r, c);
					var fillBinding = new Binding
					{
						Source = item,
						Path = new PropertyPath("Fill"),
						Mode = BindingMode.OneWay
					};

					var rect = new Rectangle();
					this.grdColor.Children.Add(rect);
					rect.SetValue(Grid.RowProperty, r);
					rect.SetValue(Grid.ColumnProperty, c);
					rect.SetBinding(Rectangle.FillProperty, fillBinding);

					//var toolTip = new ToolTip();
					//var toolTipText = new TextBlock();
					//var toolTipTextBinding = new Binding
					//{
					//	Source = item,
					//	Path = new PropertyPath("ToolTipText"),
					//	Mode = BindingMode.OneWay
					//};
					//toolTipText.SetBinding(TextBlock.TextProperty, toolTipTextBinding);
					//toolTip.SetValue(ToolTip.ContentProperty, toolTip);
					//ToolTipService.SetToolTip(rect, toolTip);
				}
			}
		}
    }
}
