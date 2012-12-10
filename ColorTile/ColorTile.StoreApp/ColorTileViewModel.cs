using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace ColorTile.StoreApp
{
	public class ColorTileViewModel : INotifyPropertyChanged
	{
		#region internal type

		private enum SelectionType
		{
			Red,
			Green,
			Blue
		}

		#endregion

		#region constructor

		public ColorTileViewModel()
		{
			_size = 16;
			_unitLength = 32;
			_items = new List<ColorTileItemViewModel>();
			_alphaValue = 255;
			this.CreateItems();
		}

		#endregion

		#region field / property

		#region AlphaValue

		private int _alphaValue;

		public int AlphaValue
		{
			get { return _alphaValue; }
			set
			{
				if (value != _alphaValue)
				{
					_alphaValue = value;
					this.HandlePropertyChanged("AlphaValue");
					this.RefreshItems();
				}
			}
		}
		#endregion

		#region IsRedChecked

		private bool? _isRedChecked;

		public bool? IsRedChecked
		{
			get { return _isRedChecked; }
			set
			{
				if ((value.HasValue != _isRedChecked.HasValue) || (value != _isRedChecked))
				{
					_isRedChecked = value;
					this.HandlePropertyChanged("IsRedChecked");
					this.HandleCheckedChanged(SelectionType.Red, value);
				}
			}
		}

		#endregion

		#region RedValue

		private int _redValue;

		public int RedValue
		{
			get { return _redValue; }
			set
			{
				if (value != _redValue)
				{
					_redValue = value;
					this.HandlePropertyChanged("RedValue");
					this.RefreshItems();
				}
			}
		}

		#endregion

		#region IsGreenChecked

		private bool? _isGreenChecked;

		public bool? IsGreenChecked
		{
			get { return _isRedChecked; }
			set
			{
				if ((value.HasValue != _isGreenChecked.HasValue) || (value != _isGreenChecked))
				{
					_isGreenChecked = value;
					this.HandlePropertyChanged("IsGreenChecked");
					this.HandleCheckedChanged(SelectionType.Green, value);
				}
			}
		}

		#endregion

		#region GreenValue

		private int _greenValue;

		public int GreenValue
		{
			get { return _greenValue; }
			set
			{
				if (value != _greenValue)
				{
					_greenValue = value;
					this.HandlePropertyChanged("GreenValue");
					this.RefreshItems();
				}
			}
		}

		#endregion

		#region IsBlueChecked

		private bool? _isBlueChecked;

		public bool? IsBlueChecked
		{
			get { return _isBlueChecked; }
			set
			{
				if ((value.HasValue != _isBlueChecked.HasValue) || (value != _isBlueChecked))
				{
					_isBlueChecked = value;
					this.HandlePropertyChanged("IsBlueChecked");
					this.HandleCheckedChanged(SelectionType.Blue, value);
				}
			}
		}

		#endregion

		#region BlueValue

		private int _blueValue;

		public int BlueValue
		{
			get { return _blueValue; }
			set
			{
				if (value != _blueValue)
				{
					_blueValue = value;
					this.HandlePropertyChanged("BlueValue");
					this.RefreshItems();
				}
			}
		}

		#endregion

		private List<ColorTileItemViewModel> _items;

		private SelectionType _selectionType;

		private int _size;

		public int Size
		{
			get { return _size; }
		}

		private int _unitLength;

		public int UnitLength
		{
			get { return _unitLength; }
		}

		#endregion

		#region event

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region method

		private void HandlePropertyChanged(string propertyName)
		{
			var handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private void HandleCheckedChanged(SelectionType selectionType, bool? isChecked)
		{
			if (isChecked.HasValue && isChecked.Value)
			{
				_selectionType = selectionType;
				switch (selectionType)
				{
					case SelectionType.Red:
						this.GreenValue = 0;
						this.BlueValue = 0;
						break;
					case SelectionType.Green:
						this.RedValue = 0;
						this.BlueValue = 0;
						break;
					case SelectionType.Blue:
						this.RedValue = 0;
						this.GreenValue = 0;
						break;
				}
				this.RefreshItems();
			}
		}

		private void CreateItems()
		{
			_items.Clear();

			for (int r = 0; r < this.Size; r++)
			{
				for (int c = 0; c < this.Size; c++)
				{
					_items.Add(new ColorTileItemViewModel { Row = r, Column = c });
				}
			}
		}

		private void RefreshItems()
		{
			int diff = (int)(256 / _size);

			for (int r = 0; r < this.Size; r++)
			{
				for (int c = 0; c < this.Size; c++)
				{
					var item = this.GetItem(r, c);
					Color color;
					switch (_selectionType)
					{
						case SelectionType.Red:
							color = Color.FromArgb((byte)this.AlphaValue, (byte)this.RedValue, (byte)(r * diff), (byte)(c * diff));
							item.Fill = new SolidColorBrush(color);
							break;
						case SelectionType.Green:
							color = Color.FromArgb((byte)this.AlphaValue, (byte)(r * diff), (byte)this.GreenValue, (byte)(c * diff));
							item.Fill = new SolidColorBrush(color);
							break;
						case SelectionType.Blue:
							color = Color.FromArgb((byte)this.AlphaValue, (byte)(r * diff), (byte)(c * diff), (byte)this.BlueValue);
							item.Fill = new SolidColorBrush(color);
							break;
					}
				}
			}
		}

		public ColorTileItemViewModel GetItem(int row, int column)
		{
			return _items.Where(x => x.Row == row && x.Column == column).FirstOrDefault();
		}

		#endregion
	}
}
