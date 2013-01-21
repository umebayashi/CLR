using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;

namespace Umebayashi.Enterprise.StoreApp.UI.Validation
{
	public abstract class ValidationRule : DependencyObject
	{
	}

	public class CompareValidationRule : ValidationRule
	{
	}

	public class CustomValidationRule : ValidationRule
	{
	}

	public class RangeValidationRule : ValidationRule
	{
		#region field / property

		public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
			"Min",
			typeof(string),
			typeof(RangeValidationRule),
			new PropertyMetadata(null, null));

		public string Min
		{
			get { return (string)this.GetValue(MinProperty); }
			set { this.SetValue(MinProperty, value); }
		}

		public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
			"Max",
			typeof(string),
			typeof(RangeValidationRule),
			new PropertyMetadata(null, null));

		public string Max
		{
			get { return (string)this.GetValue(MaxProperty); }
			set { this.SetValue(MaxProperty, value); }
		}

		public static readonly DependencyProperty DataTypeProperty = DependencyProperty.Register(
			"DataType",
			typeof(ValidationDataType),
			typeof(RangeValidationRule),
			new PropertyMetadata(null, null));

		public ValidationDataType DataType
		{
			get { return (ValidationDataType)this.GetValue(DataTypeProperty); }
			set { this.SetValue(DataTypeProperty, value); }
		}

		#endregion
	}

	public class RegularExpressionValidationRule : ValidationRule
	{
	}

	public class RequiredValidationRule : ValidationRule
	{
	}

	public enum ValidationDataType
	{
		String,
		Int32,
		Double,
		DateTime
	}
}
