using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Umebayashi.Games.Common.ViewModels;

// テンプレート コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234235 を参照してください

namespace Umebayashi.Games.Common.Controls
{
	public class GameBoardControl : Grid
	{
		#region constructor

		public GameBoardControl()
		{
		}

		#endregion

		#region field / property

		#region Size

		public const int SIZE_DEFAULT = 8;

		public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
			"Size",
			typeof(int),
			typeof(GameBoardControl),
			new PropertyMetadata(SIZE_DEFAULT, OnSizeChanged));

		private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as GameBoardControl;
			if (target != null)
			{
				target.SetSize();
			}
		}

		public int Size
		{
			get { return (int)GetValue(SizeProperty); }
			set { SetValue(SizeProperty, value); }
		}

		#endregion

		#region SquareColor1

		private static readonly Color SQUARE_COLOR_1_DEFAULT = Colors.White;

		public static readonly DependencyProperty SquareColor1Property = DependencyProperty.Register(
			"SquareColor1",
			typeof(Color),
			typeof(GameBoardControl),
			new PropertyMetadata(SQUARE_COLOR_1_DEFAULT, OnSquareColor1Changed));

		private static void OnSquareColor1Changed(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as GameBoardControl;
			if (target != null)
			{
			}
		}

		public Color SquareColor1
		{
			get { return (Color)GetValue(SquareColor1Property); }
			set { SetValue(SquareColor1Property, value); }
		}

		#endregion

		#region SquareColor2

		private static readonly Color SQUARE_COLOR_2_DEFAULT = Colors.Black;

		public static readonly DependencyProperty SquareColor2Property = DependencyProperty.Register(
			"SquareColor2",
			typeof(Color),
			typeof(GameBoardControl),
			new PropertyMetadata(SQUARE_COLOR_1_DEFAULT, OnSquareColor2Changed));

		private static void OnSquareColor2Changed(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as GameBoardControl;
			if (target != null)
			{
			}
		}

		public Color SquareColor2
		{
			get { return (Color)GetValue(SquareColor2Property); }
			set { SetValue(SquareColor2Property, value); }
		}

		#endregion

		#region ViewModel

		private GameBoardViewModel _viewModel;

		protected GameBoardViewModel ViewModel
		{
			get { return _viewModel; }
		}

		#endregion

		#endregion

		#region method

		public virtual void SetViewModel(GameBoardViewModel viewModel)
		{
			_viewModel = viewModel;
			this.DataContext = viewModel;
		}

		protected virtual void SetSize()
		{
			this.Children.Clear();
			this.RowDefinitions.Clear();
			this.ColumnDefinitions.Clear();

			for (int i = 0; i < this.ViewModel.Size; i++)
			{
				this.RowDefinitions.Add(new RowDefinition());
				this.ColumnDefinitions.Add(new ColumnDefinition());
			}

			if (this.ViewModel != null)
			{
				for (int r = 0; r < this.ViewModel.Size; r++)
				{
					for (int c = 0; c < this.ViewModel.Size; c++)
					{
						var vmSquare = this.ViewModel.GetSquareViewModel(r, c);
						var rectangle = new Rectangle();

						rectangle.SetValue(Grid.RowProperty, r);
						rectangle.SetValue(Grid.ColumnProperty, c);

						this.Children.Add(rectangle);
					}
				}
			}
		}

		#endregion
	}
}
