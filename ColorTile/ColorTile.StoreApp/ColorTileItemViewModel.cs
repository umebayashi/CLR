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
	public class ColorTileItemViewModel : INotifyPropertyChanged
	{
		#region constructor
		#endregion

		#region field / property

		private int _row;

		public int Row
		{
			get { return _row; }
			set
			{
				if (value != _row)
				{
					_row = value;
					HandlePropertyChanged("Row");
				}
			}
		}

		private int _column;

		public int Column
		{
			get { return _column; }
			set
			{
				_column = value;
				HandlePropertyChanged("Column");
			}
		}

		private Brush _fill;

		public Brush Fill
		{
			get { return _fill; }
			set
			{
				if (value != _fill)
				{
					_fill = value;
					HandlePropertyChanged("Fill");

					this.UpdateToolTipText();
				}
			}
		}

		private string _toolTipText;

		public string ToolTipText
		{
			get { return _toolTipText; }
			set
			{
				if (value != _toolTipText)
				{
					_toolTipText = value;
					HandlePropertyChanged("ToolTipText");
				}
			}
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

		private void UpdateToolTipText()
		{
			var fill = this.Fill as SolidColorBrush;
			if (fill != null)
			{
				this.ToolTipText = string.Format("R:{0} / G:{1} / B:{2}",
					fill.Color.R,
					fill.Color.G,
					fill.Color.B);
			}
		}

		#endregion
	}
}
