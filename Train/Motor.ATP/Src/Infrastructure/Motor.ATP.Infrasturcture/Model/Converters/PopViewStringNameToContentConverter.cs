using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CommonUtil.Util;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public class PopViewStringNameToContentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || !(values[0] is FrameworkElement)
                || !(values[1] is string))
            {
                return DependencyProperty.UnsetValue;
            }

            try
            {
                var str = FindResoure((DependencyObject)values[0], (string)values[1]);
                return str ?? values[1];
            }
            catch (Exception e)
            {
                AppLog.Debug("Can not found resource where key = {0}, {1}", values[1], e);
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string FindResoure(DependencyObject control, string key)
        {
            while (true)
            {
                var fe = control as FrameworkElement;
                if (fe != null)
                {
                    var obj = fe.Resources[key] as string;
                    if (obj == null)
                    {
                        control = LogicalTreeHelper.GetParent(control);
                        continue;
                    }

                    return obj;
                }

                return Application.Current.Resources[key] as string;
            }
        }
    }
}