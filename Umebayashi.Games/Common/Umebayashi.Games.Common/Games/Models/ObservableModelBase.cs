using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Umebayashi.Games.Models
{
	public class ObservableModelBase : INotifyPropertyChanged
	{
		#region event

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region method

		protected virtual void OnPropertyChanged(string propertyName)
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
