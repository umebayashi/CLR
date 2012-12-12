using System;
using System.Collections.Generic;
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
	public class PuzzlePieceViewModel : ViewModelBase
	{
		#region constructor
		#endregion

		#region field / property

		private int _number;

		public int Number
		{
			get { return _number; }
			set
			{
				if (_number != value)
				{
					_number = value;
					OnPropertyChanged("Number");
				}
			}
		}

		private bool _isHidden;

		public bool IsHidden
		{
			get { return _isHidden; }
			set
			{
				if (_isHidden != value)
				{
					_isHidden = value;
					OnPropertyChanged("IsHidden");

					this.TextVisibility = value ? Visibility.Collapsed : Visibility.Visible;
				}
			}
		}

		private Brush _background;

		public Brush Background
		{
			get { return _background; }
			set
			{
				if (_background != value)
				{
					_background = value;
					OnPropertyChanged("Background");
				}
			}
		}

		private Brush _borderBrush;

		public Brush BorderBrush
		{
			get { return _borderBrush; }
			set
			{
				if (_borderBrush != value)
				{
					_borderBrush = value;
					OnPropertyChanged("BorderBrush");
				}
			}
		}

		private Thickness _borderThickness;

		public Thickness BorderThickness
		{
			get { return _borderThickness; }
			set
			{
				if (_borderThickness != value)
				{
					_borderThickness = value;
					OnPropertyChanged("BorderThickness");
				}
			}
		}

		private double _textFontSize;

		public double TextFontSize
		{
			get { return _textFontSize; }
			set
			{
				if (_textFontSize != value)
				{
					_textFontSize = value;
					OnPropertyChanged("TextFontSize");
				}
			}
		}

		private Brush _textForeground;

		public Brush TextForeground
		{
			get { return _textForeground; }
			set
			{
				if (_textForeground != value)
				{
					_textForeground = value;
					OnPropertyChanged("TextForeground");
				}
			}
		}

		private Visibility _textVisibility;

		public Visibility TextVisibility
		{
			get { return _textVisibility; }
			set
			{
				if (_textVisibility != value)
				{
					_textVisibility = value;
					OnPropertyChanged("TextVisibility");
				}
			}
		}

		private int _row;

		public int Row
		{
			get { return _row; }
			set
			{
				if (_row != value)
				{
					_row = value;
					OnPropertyChanged("Row");
				}
			}
		}

		private int _column;

		public int Column
		{
			get { return _column; }
			set
			{
				if (_column != value)
				{
					_column = value;
					OnPropertyChanged("Column");
				}
			}
		}
		#endregion
	}
}
