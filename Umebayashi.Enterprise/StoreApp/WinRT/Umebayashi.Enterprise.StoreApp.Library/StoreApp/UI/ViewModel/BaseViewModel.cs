using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.Enterprise.StoreApp.UI.ViewModel
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		#region constructor

		public BaseViewModel()
		{
		}

		#endregion

		#region field / property
		#endregion

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

		protected void SetValueCore<T>(ref T field, T value, string propertyName) where T : IEquatable<T>
		{
			if ((field == null) || (!field.Equals(value)))
			{
				field = value;
				this.OnPropertyChanged(propertyName);
			}
		}

		protected virtual void SetValue<T>(ref T field, T value, string propertyName) where T : IEquatable<T>
		{
			SetValueCore<T>(ref field, value, propertyName);
		}

		#endregion
	}
}
