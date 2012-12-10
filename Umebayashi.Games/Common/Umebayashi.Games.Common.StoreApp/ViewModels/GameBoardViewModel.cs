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
	public abstract class GameBoardViewModel : ViewModelBase
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

		#region SquareColor1

		private Color _squareColor1;

		public Color SquareColor1
		{
			get { return _squareColor1; }
			set
			{
				if (_squareColor1 != value)
				{
					_squareColor1 = value;
					OnPropertyChanged("SquareColor1");
				}
			}
		}

		#endregion

		#region SquareColor2

		private Color _squareColor2;

		public Color SquareColor2
		{
			get { return _squareColor2; }
			set
			{
				if (_squareColor2 != value)
				{
					_squareColor2 = value;
					OnPropertyChanged("SquareColor2");
				}
			}
		}

		#endregion

		#region Squares

		private List<GameBoardSquareViewModel> _squares;

		#endregion

		#endregion

		#region method

		protected abstract GameBoardSquareViewModel CreateSquareViewModel(int row, int column, Color color);

		protected virtual void OnSizeChanged()
		{
			_squares.Clear();

			for (int r = 0; r < this.Size; r++)
			{
				for (int c = 0; c < this.Size; c++)
				{
					Color color;
					if ((r + c) % 2 == 0)
					{
						color = this.SquareColor1;
					}
					else
					{
						color = this.SquareColor2;
					}
					var square = this.CreateSquareViewModel(r, c, color);

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
