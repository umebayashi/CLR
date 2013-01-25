using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.Enterprise.StoreApp.UI.Validation
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ValidateOnChangeAttribute : System.Attribute
	{
		#region constructor

		public ValidateOnChangeAttribute(bool validateOnChange = true)
		{
			this.ValidateOnChange = validateOnChange;
		}

		#endregion

		#region field / property

		public bool ValidateOnChange { get; set; }

		#endregion
	}
}
