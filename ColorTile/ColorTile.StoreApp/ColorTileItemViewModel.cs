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

		#endregion
	}
}
