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
			int count = 8;
			int unitlen = 64;

			this.grdColor.RowDefinitions.Clear();
			this.grdColor.ColumnDefinitions.Clear();
			for (int r = 0; r < count; r++)
			{
				this.grdColor.RowDefinitions.Add(new RowDefinition { Height = new GridLength(unitlen) });
			}
			for (int c = 0; c < count; c++)
			{
				this.grdColor.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(unitlen) });
			}

			for (int r = 0; r < count; r++)
			{
				for (int c = 0; c < count; c++)
				{
					var rect = new Rectangle();
					this.grdColor.Children.Add(rect);
					rect.SetValue(Grid.RowProperty, r);
					rect.SetValue(Grid.ColumnProperty, c);
					rect.Fill = new SolidColorBrush(Colors.White);
				}
			}
		}
    }
}
