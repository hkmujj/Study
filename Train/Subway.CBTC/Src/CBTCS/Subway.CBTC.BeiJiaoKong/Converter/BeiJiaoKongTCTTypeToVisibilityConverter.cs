using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Subway.CBTC.BeiJiaoKong.Models.Domain;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongTCTTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                switch ((TCTType)value)
                {
                    case TCTType.ShenZhen:
                        return Visibility.Hidden;

                    case TCTType.GuiYang:
                        return Visibility.Visible;

                    default:
                        return false;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}