using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
