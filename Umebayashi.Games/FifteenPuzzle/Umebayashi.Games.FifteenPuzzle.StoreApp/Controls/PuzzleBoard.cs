using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Umebayashi.Games.FifteenPuzzle.StoreApp.ViewModels;

// テンプレート コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234235 を参照してください

namespace Umebayashi.Games.FifteenPuzzle.StoreApp.Controls
{
	public sealed class PuzzleBoard : Grid
	{
		#region constructor

		public PuzzleBoard()
		{
		}

		#endregion

		#region field / property

		#region Size

		public const int SIZE_DEFAULT = 0;

		public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
			"Size",
			typeof(int),
			typeof(PuzzleBoard),
			new PropertyMetadata(SIZE_DEFAULT, OnSizeChanged));

		private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as PuzzleBoard;
			if (target != null)
			{
				target.Reset();
			}
		}

		public int Size
		{
			get { return (int)GetValue(SizeProperty); }
			set { SetValue(SizeProperty, value); }
		}

		#endregion

		//private List<PuzzlePieceViewModel> _pieceViewModels = new List<PuzzlePieceViewModel>();
		//private List<PuzzlePiece> _pieces = new List<PuzzlePiece>();

		#endregion

		#region method

		private void Reset()
		{
			this.Children.Clear();
			this.RowDefinitions.Clear();
			this.ColumnDefinitions.Clear();

			//_pieces.Clear();
			//_pieceViewModels.Clear();

			for (int i = 0; i < this.Size; i++)
			{
				this.RowDefinitions.Add(new RowDefinition());
				this.ColumnDefinitions.Add(new ColumnDefinition());
			}

			var vm = this.DataContext as PuzzleBoardViewModel;
			if (vm != null)
			{
				for (int i = 0; i < vm.Pieces.Count; i++)
				{
					var piece = new PuzzlePiece();
					piece.DataContext = vm.Pieces[i];
					piece.SetBinding(Grid.RowProperty, new Binding { Path = new PropertyPath("Row") });
					piece.SetBinding(Grid.ColumnProperty, new Binding { Path = new PropertyPath("Column") });
					this.Children.Add(piece);
				}
			}
		}

		#endregion
	}
}
