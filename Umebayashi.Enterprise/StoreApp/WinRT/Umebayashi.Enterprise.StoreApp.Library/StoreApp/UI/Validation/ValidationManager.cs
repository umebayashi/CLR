using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;

namespace Umebayashi.Enterprise.StoreApp.UI.Validation
{
	public class ValidationManager : DependencyObject
	{
		public static readonly DependencyProperty ValidationRulesProperty = DependencyProperty.RegisterAttached(
			"ValidationRules",
			typeof(ValidationRuleCollection),
			typeof(ValidationManager),
			new PropertyMetadata(null, null));

		public static ValidationRuleCollection GetValidationRules(DependencyObject target)
		{
			return (ValidationRuleCollection)target.GetValue(ValidationRulesProperty);
		}

		public static void SetValidationRules(DependencyObject target, ValidationRuleCollection value)
		{
			target.SetValue(ValidationRulesProperty, value);
		}
	}
}
