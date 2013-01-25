using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.Enterprise.StoreApp.UI.Validation
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ValidationPropertyAttribute : System.Attribute
	{
		#region constructor

		public ValidationPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		#endregion

		#region field / property

		public string PropertyName
		{
			get;
			set;
		}

		#endregion
	}
}
