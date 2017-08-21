using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Motor.LKJ._2000.Entity.Model;

namespace Motor.LKJ._2000.Entity.Converter
{
    public class LKJStateToImageSourceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue)
            {
                return null;    
            }
            var state = (LKJState) values[0];
            switch (state)
            {
                case LKJState.Shutdown:
                    return null;
                case LKJState.RunInATP:
                    return values[1];
                case LKJState.RunInLKJ:
                    return values[2];
                default:
                    return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}