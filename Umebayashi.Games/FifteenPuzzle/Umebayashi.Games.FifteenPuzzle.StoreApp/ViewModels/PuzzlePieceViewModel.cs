using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Umebayashi.Games.Common.ViewModels;
using Umebayashi.Games.FifteenPuzzle.Core.Models;

namespace Umebayashi.Games.FifteenPuzzle.StoreApp.ViewModels
{
	public class PuzzlePieceViewModel : ViewModelBase
	{
		#region constructor

		public PuzzlePieceViewModel(FifteenPuzzlePieceModel model)
		{
			_model = model;
			_model.PropertyChanged += PuzzlePieceModel_PropertyChanged;

			_textVisibility = model.IsEmpty ? Visibility.Collapsed : Visibility.Visible;
		}

		#endregion

		#region field / property

		private FifteenPuzzlePieceModel _model;

		public int Number
		{
			get { return _model.Number; }
			set
			{
				if (_model.Number != value)
				{
					_model.Number = value;
					this.OnPropertyChanged("Number");
				}
			}
		}

		public int Row
		{
			get { return _model.Row; }
			set
			{
				if (_model.Row != value)
				{
					_model.Row = value;
					this.OnPropertyChanged("Row");
				}
			}
		}

		public int Column
		{
			get { return _model.Column; }
			set
			{
				if (_model.Column != value)
				{
					_model.Column = value;
					this.OnPropertyChanged("Column");
				}
			}
		}

		public bool IsEmpty
		{
			get { return _model.IsEmpty; }
			set
			{
				if (_model.IsEmpty != value)
				{
					_model.IsEmpty = value;
					this.OnPropertyChanged("IsEmpty");
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
					this.OnPropertyChanged("Background");
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
					this.OnPropertyChanged("BorderBrush");
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
					this.OnPropertyChanged("BorderThickness");
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
					this.OnPropertyChanged("TextFontSize");
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
					this.OnPropertyChanged("TextForeground");
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
					this.OnPropertyChanged("TextVisibility");
				}
			}
		}

		#endregion

		#region method

		private void PuzzlePieceModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.OnPropertyChanged(e.PropertyName);
		}

		public void OnPointerPressed(object sender, PointerRoutedEventArgs e)
		{
			_model.MovePiece();
		}

		#endregion
	}
}
