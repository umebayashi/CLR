using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Umebayashi.Enterprise.StoreApp.UI.Validation;

namespace Umebayashi.Enterprise.StoreApp.UI.Converter
{
	public class ValidationResultCollectionConnverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var validationResults = value as ObservableCollection<ValidationResult>;
			if (validationResults != null)
			{
				var builder = new StringBuilder();
				builder.AppendLine();
				foreach (var item in validationResults)
				{
					builder.Append(string.Format("・{0}", item.ErrorMessage)).AppendLine();
				}
				builder.Remove(0, Environment.NewLine.Length);

				return builder.ToString();
			}
			else
			{
				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
