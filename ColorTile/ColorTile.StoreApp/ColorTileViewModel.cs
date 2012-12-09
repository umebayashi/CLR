using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			_size = 8;
		}

		#endregion

		#region field / property

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
				}
			}
		}

		#endregion

		private SelectionType _selectionType;

		private int _size;

		public int Size
		{
			get { return _size; }
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
			}
		}

		#endregion
	}
}
