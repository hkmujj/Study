using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Motor.HMI.CRH380D.Interfaces;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Converter
{
    public class EventInfoPopUpConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return false;
            }

            if ((EventLevel)value == EventLevel.AWarring || (EventLevel)value == EventLevel.BWarring)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}