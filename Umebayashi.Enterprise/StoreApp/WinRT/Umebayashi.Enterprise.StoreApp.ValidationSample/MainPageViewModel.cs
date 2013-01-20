using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umebayashi.Enterprise.StoreApp.UI.Validation;

namespace Umebayashi.Enterprise.StoreApp.ValidationSample
{
	public class MainPageViewModel : INotifyPropertyChanged, IValidateField
	{
		#region constructor

		public MainPageViewModel()
		{
		}

		#endregion

		#region field / property

		private int _compareValueTarget;

		public int CompareValueTarget
		{
			get { return _compareValueTarget; }
			set
			{
				this.SetValue<int>(ref _compareValueTarget, value, "CompareValueTarget");
			}
		}

		private int _rangeValueTarget;

		[Range(0, 100)]
		public int RangeValueTarget
		{
			get { return _rangeValueTarget; }
			set
			{
				Validator.ValidateProperty(value, new ValidationContext(this) { MemberName = "RangeValueTarget" });
				this.SetValue<int>(ref _rangeValueTarget, value, "RangeValueTarget");
			}
		}

		#endregion

		#region method

		protected void OnPropertyChanged(string propertyName)
		{
			var handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected void SetValue<T>(ref T field, T value, string propertyName) where T : IEquatable<T>
		{
			if ((field == null) || (!field.Equals(value)))
			{
				field = value;
				this.OnPropertyChanged(propertyName);
			}
		}

		#endregion

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
