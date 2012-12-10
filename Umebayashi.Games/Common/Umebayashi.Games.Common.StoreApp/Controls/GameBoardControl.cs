using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
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
			this.Loaded += GameBoardControl_Loaded;
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
			}
		}

		public int Size
		{
			get { return (int)GetValue(SizeProperty); }
			set { SetValue(SizeProperty, value); }
		}

		#endregion

		#region SquareColor1

		private static readonly Color SQUARE_BACKGROUND_1_DEFAULT = Colors.White;

		public static readonly DependencyProperty SquareBackground1Property = DependencyProperty.Register(
			"SquareBackground1",
			typeof(Color),
			typeof(GameBoardControl),
			new PropertyMetadata(SQUARE_BACKGROUND_1_DEFAULT, OnSquareBackground1Changed));

		private static void OnSquareBackground1Changed(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as GameBoardControl;
			if (target != null)
			{
			}
		}

		public Color SquareBackground1
		{
			get { return (Color)GetValue(SquareBackground1Property); }
			set { SetValue(SquareBackground1Property, value); }
		}

		#endregion

		#region SquareColor2

		private static readonly Color SQUARE_BACKGROUND_2_DEFAULT = Colors.Black;

		public static readonly DependencyProperty SquareBackground2Property = DependencyProperty.Register(
			"SquareBackground2",
			typeof(Color),
			typeof(GameBoardControl),
			new PropertyMetadata(SQUARE_BACKGROUND_1_DEFAULT, OnSquareBackground2Changed));

		private static void OnSquareBackground2Changed(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as GameBoardControl;
			if (target != null)
			{
			}
		}

		public Color SquareBackground2
		{
			get { return (Color)GetValue(SquareBackground2Property); }
			set { SetValue(SquareBackground2Property, value); }
		}

		#endregion

		#endregion

		#region method

		private void GameBoardControl_Loaded(object sender, RoutedEventArgs e)
		{
			this.InitializeGameBoard();
		}

		protected virtual void InitializeGameBoard()
		{
			if (DesignMode.DesignModeEnabled)
			{
				this.Background = new SolidColorBrush(Colors.White);
			}
			else
			{
				this.Children.Clear();
				this.RowDefinitions.Clear();
				this.ColumnDefinitions.Clear();

				for (int i = 0; i < this.Size; i++)
				{
					this.RowDefinitions.Add(new RowDefinition());
					this.ColumnDefinitions.Add(new ColumnDefinition());
				}

				for (int r = 0; r < this.Size; r++)
				{
					for (int c = 0; c < this.Size; c++)
					{
						var ctSquare = this.CreateSquareControl();
						ctSquare.SetValue(Grid.RowProperty, r);
						ctSquare.SetValue(Grid.ColumnProperty, c);

						Color background;
						if ((r + c) % 2 == 0)
						{
							background = this.SquareBackground1;
						}
						else
						{
							background = this.SquareBackground2;
						}
						ctSquare.Background = new SolidColorBrush(background);

						this.Children.Add(ctSquare);
					}
				}
			}
		}

		protected virtual GameBoardSquareControl CreateSquareControl()
		{
			return new GameBoardSquareControl();
		}

		#endregion
	}
}
