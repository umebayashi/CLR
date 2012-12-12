using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Umebayashi.Games.Common.ViewModels;

namespace Umebayashi.Games.FifteenPuzzle.StoreApp.ViewModels
{
	public class PuzzleBoardViewModel : ViewModelBase
	{
		#region constructor

		public PuzzleBoardViewModel()
		{
			_pieces = new ObservableCollection<PuzzlePieceViewModel>();
		}

		#endregion

		#region field / property

		private int _size;

		public int Size
		{
			get { return _size; }
			set
			{
				if (_size != value)
				{
					_size = value;
					this.OnPropertyChanged("Size");
					this.Reset();
				}
			}
		}

		private ObservableCollection<PuzzlePieceViewModel> _pieces;

		public ObservableCollection<PuzzlePieceViewModel> Pieces
		{
			get { return _pieces; }
		}

		#endregion

		#region method

		private void Reset()
		{
			this.Pieces.Clear();

			int max = this.Size * this.Size;
			int number = 1;
			for (int r = 0; r < this.Size; r++)
			{
				for (int c = 0; c < this.Size; c++)
				{
					var piece = new PuzzlePieceViewModel
					{
						Number = number++,
						Background = new SolidColorBrush(Colors.White),
						BorderBrush = new SolidColorBrush(Colors.Black),
						BorderThickness = new Thickness(5.0),
						TextFontSize = 48.0,
						TextForeground = new SolidColorBrush(Colors.Red),
						Row = r,
						Column = c
					};

					if (piece.Number == max)
					{
						piece.IsHidden = true;
						piece.Background = new SolidColorBrush(Colors.Black);
					}

					this.Pieces.Add(piece);
				}
			}
		}

		#endregion
	}
}
