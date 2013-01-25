using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umebayashi.Enterprise.StoreApp.UI.Validation;
using Umebayashi.Enterprise.StoreApp.UI.ViewModel;

namespace Umebayashi.Enterprise.StoreApp.ValidationSample
{
	public class MainPageViewModel : ValidationViewModel
	{
		#region constructor

		public MainPageViewModel() : base()
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
		[ValidateOnChange(false)]
		public int RangeValueTarget
		{
			get { return _rangeValueTarget; }
			set
			{
				this.SetValue<int>(ref _rangeValueTarget, value, "RangeValueTarget");
			}
		}

		private ObservableCollection<ValidationResult> _rangeValueValidationResults =
			new ObservableCollection<ValidationResult>();

		[ValidationProperty("RangeValueTarget")]
		public ObservableCollection<ValidationResult> RangeValueValidationResults
		{
			get { return _rangeValueValidationResults; }
			set
			{
				_rangeValueValidationResults = value;
				OnPropertyChanged("RangeValueValidationResults");
			}
		}

		#endregion

		//#region method

		//protected void OnPropertyChanged(string propertyName)
		//{
		//	var handler = this.PropertyChanged;
		//	if (handler != null)
		//	{
		//		handler(this, new PropertyChangedEventArgs(propertyName));
		//	}
		//}

		//protected void SetValue<T>(ref T field, T value, string propertyName) where T : IEquatable<T>
		//{
		//	var context = new ValidationContext(this) { MemberName = propertyName };
		//	var results = new List<ValidationResult>();
		//	Validator.TryValidateProperty(value, context, results);

		//	if ((field == null) || (!field.Equals(value)))
		//	{
		//		field = value;
		//		this.OnPropertyChanged(propertyName);
		//	}
		//}

		//#endregion

		//#region INotifyPropertyChanged

		//public event PropertyChangedEventHandler PropertyChanged;

		//#endregion
	}
}
