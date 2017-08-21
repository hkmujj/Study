using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace General.CIR.Converter
{
	public class StringLengthConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = value == null || value == DependencyProperty.UnsetValue;
			object result;
			if (flag)
			{
				result = DependencyProperty.UnsetValue;
			}
			else
			{
				result = value.ToString().PadLeft(2, ' ');
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
