using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongFlickingConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            switch ((ReturnState)value)
            {
                case ReturnState.AutoReturn:
                    return true;

                case ReturnState.AutoReturnActived:
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}