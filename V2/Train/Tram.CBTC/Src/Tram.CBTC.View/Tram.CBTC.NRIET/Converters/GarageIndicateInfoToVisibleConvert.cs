using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Road;


namespace Tram.CBTC.NRIET.Converters
{
    public class GarageIndicateInfoToVisibleConvert : IMultiValueConverter
    {

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 2 || !(values[0] is GarageIndicateInfo) || !(values[1] is StationPass))
            {
                return DependencyProperty.UnsetValue;
            }

            if (!((StationPass)values[1]).IsStation || ((StationPass)values[1]).ID != ((GarageIndicateInfo)values[0]).StationID)
            {
                return Visibility.Hidden;
            }

            if (((GarageIndicateInfo)values[0]).GarageIndicate == GarageIndicate.InGarage ||
                ((GarageIndicateInfo)values[0]).GarageIndicate == GarageIndicate.OutGarage)
            {
                return Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}