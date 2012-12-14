using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Umebayashi.Games.Common.ViewModels;
using Umebayashi.Games.FifteenPuzzle.Core.Models;

namespace Umebayashi.Games.FifteenPuzzle.StoreApp.ViewModels
{
	public class PuzzleBoardViewModel : ViewModelBase
	{
		#region constructor

		public PuzzleBoardViewModel()
		{
			_pieces = new ObservableCollection<PuzzlePieceViewModel>();
			_model = new FifteenPuzzleModel();
		}

		#endregion

		#region field / property

		public int Size
		{
			get { return _model.Size; }
			set
			{
				if (_model.Size != value)
				{
					_model.Size = value;
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

		private FifteenPuzzleModel _model;

		#endregion

		#region method

		private void Reset()
		{
			this.Pieces.Clear();

			_model.Initialize();
			_model.Shuffle();
			foreach (var mdlPiece in _model.Pieces)
			{
				var vmPiece = new PuzzlePieceViewModel(mdlPiece);
				vmPiece.Background = mdlPiece.IsEmpty ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
				vmPiece.BorderBrush = new SolidColorBrush(Colors.Black);
				vmPiece.BorderThickness = new Thickness(5.0);
				vmPiece.TextFontSize = 48.0;
				vmPiece.TextForeground = new SolidColorBrush(Colors.Red);

				this.Pieces.Add(vmPiece);
			}
		}

		

		#endregion
	}
}
