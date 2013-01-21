using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;

namespace Umebayashi.Enterprise.StoreApp.UI.Validation
{
	public class ValidationRuleCollection : DependencyObject, IList<ValidationRule>, IEnumerable<ValidationRule>
	{
		#region constructor
		#endregion

		#region field / property

		private List<ValidationRule> validationRules = new List<ValidationRule>();

		#endregion

		#region IList<ValidationRule>

		public int IndexOf(ValidationRule item)
		{
			return this.validationRules.IndexOf(item);
		}

		public void Insert(int index, ValidationRule item)
		{
			this.validationRules.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			this.validationRules.RemoveAt(index);
		}

		public ValidationRule this[int index]
		{
			get
			{
				return this.validationRules[index];
			}
			set
			{
				this.validationRules[index] = value;
			}
		}

		public void Add(ValidationRule item)
		{
			this.validationRules.Add(item);
		}

		public void Clear()
		{
			this.validationRules.Clear();
		}

		public bool Contains(ValidationRule item)
		{
			return this.validationRules.Contains(item);
		}

		public void CopyTo(ValidationRule[] array, int arrayIndex)
		{
			this.validationRules.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return this.validationRules.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(ValidationRule item)
		{
			return this.validationRules.Remove(item);
		}

		public IEnumerator<ValidationRule> GetEnumerator()
		{
			return this.validationRules.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}
