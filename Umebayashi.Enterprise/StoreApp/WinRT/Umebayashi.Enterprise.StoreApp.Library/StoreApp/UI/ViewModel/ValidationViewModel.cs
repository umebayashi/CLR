using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umebayashi.Enterprise.StoreApp.UI.Validation;

namespace Umebayashi.Enterprise.StoreApp.UI.ViewModel
{
	public class ValidationViewModel : BaseViewModel
	{
		#region constructor

		public ValidationViewModel() : base()
		{
			var type = this.GetType();

			foreach (var propInfo in type.GetRuntimeProperties())
			{
				var attrValidationProperty = propInfo.GetCustomAttribute<ValidationPropertyAttribute>();
				if (attrValidationProperty != null)
				{
					var validationResults = propInfo.GetValue(this) as ObservableCollection<ValidationResult>;
					_validationResults.Add(attrValidationProperty.PropertyName, validationResults);
				}

				var attrValidateOnChange = propInfo.GetCustomAttribute<ValidateOnChangeAttribute>();
				if (attrValidateOnChange != null)
				{
					_validateOnChanges.Add(propInfo.Name, attrValidateOnChange);
				}
			}
		}

		#endregion

		#region field / property

		private Dictionary<string, ValidateOnChangeAttribute> _validateOnChanges =
			new Dictionary<string, ValidateOnChangeAttribute>();

		private Dictionary<string, ObservableCollection<ValidationResult>> _validationResults =
			new Dictionary<string, ObservableCollection<ValidationResult>>();

		#endregion

		#region method

		protected override void SetValue<T>(ref T field, T value, string propertyName)
		{
			if (_validateOnChanges.ContainsKey(propertyName) && _validateOnChanges[propertyName].ValidateOnChange)
			{
				ValidateProperty(propertyName, value);
			}

			SetValueCore<T>(ref field, value, propertyName);
		}

		public IEnumerable<ValidationResult> ValidateProperty(string propertyName)
		{
			var propInfo = this.GetType().GetRuntimeProperty(propertyName);
			var value = propInfo.GetValue(this);
			var results = ValidateProperty(propertyName, value);
			return results;
		}

		private IEnumerable<ValidationResult> ValidateProperty(string propertyName, object value)
		{
			var context = new ValidationContext(this) { MemberName = propertyName };
			var results = new List<ValidationResult>();
			Validator.TryValidateProperty(value, context, results);

			return results;
		}

		#endregion
	}
}
