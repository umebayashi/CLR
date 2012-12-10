using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Umebayashi.Games.Common.ViewModels
{
	public class GameBoardViewModel : ViewModelBase
	{
		#region constructor

		public GameBoardViewModel()
		{
			_squares = new List<GameBoardSquareViewModel>();
		}

		#endregion

		#region field / property

		#region Size

		private int _size;

		public int Size
		{
			get { return _size; }
			set
			{
				if (_size != value)
				{
					_size = value;
					OnPropertyChanged("Size");
				}
			}
		}

		#endregion

		#region SquareBackground1

		private Color _squareBackground1;

		public Color SquareColor1
		{
			get { return _squareBackground1; }
			set
			{
				if (_squareBackground1 != value)
				{
					_squareBackground1 = value;
					OnPropertyChanged("SquareBackground1");
				}
			}
		}

		#endregion

		#region SquareBackground2

		private Color _squareBackground2;

		public Color SquareColor2
		{
			get { return _squareBackground2; }
			set
			{
				if (_squareBackground2 != value)
				{
					_squareBackground2 = value;
					OnPropertyChanged("SquareBackground2");
				}
			}
		}

		#endregion

		#region Squares

		private List<GameBoardSquareViewModel> _squares;

		#endregion

		#endregion

		#region method

		protected virtual GameBoardSquareViewModel CreateSquareViewModel(int row, int column, Color background)
		{
			return new GameBoardSquareViewModel { Row = row, Column = column, Background = background };
		}

		protected virtual void OnSizeChanged()
		{
			_squares.Clear();

			for (int r = 0; r < this.Size; r++)
			{
				for (int c = 0; c < this.Size; c++)
				{
					Color background;
					if ((r + c) % 2 == 0)
					{
						background = this.SquareColor1;
					}
					else
					{
						background = this.SquareColor2;
					}
					var square = this.CreateSquareViewModel(r, c, background);

					_squares.Add(square);
				}
			}
		}

		public GameBoardSquareViewModel GetSquareViewModel(int row, int column)
		{
			var square = _squares.Where(x => x.Row == row && x.Column == column).FirstOrDefault();

			return square;
		}

		#endregion
	}
}
