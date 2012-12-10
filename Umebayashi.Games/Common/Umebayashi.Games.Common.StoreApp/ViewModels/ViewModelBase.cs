using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.Games.Common.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged
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
