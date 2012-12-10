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
	public abstract class GameBoardSquareViewModel : ViewModelBase
	{
		#region constructor
		#endregion

		#region field / property

		#region Row

		private int _row;

		public int Row
		{
			get { return _row; }
			set
			{
				if (value != _row)
				{
					_row = value;
					OnPropertyChanged("Row");
				}
			}
		}

		#endregion

		#region Column

		private int _column;

		public int Column
		{
			get { return _column; }
			set
			{
				if (value != _column)
				{
					_column = value;
					OnPropertyChanged("Column");
				}
			}
		}

		#endregion

		#region Color

		private Color _color;

		public Color Color
		{
			get { return _color; }
			set
			{
				if (value != _color)
				{
					_color = value;
					OnPropertyChanged("Color");
				}
			}
		}

		#endregion

		#endregion
	}
}
