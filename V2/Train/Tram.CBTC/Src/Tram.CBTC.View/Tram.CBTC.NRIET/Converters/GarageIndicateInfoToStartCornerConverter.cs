using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Microsoft.Expression.Media;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Road;


namespace Tram.CBTC.NRIET.Converters
{
    public class GarageIndicateInfoToStartCornerConverter : IMultiValueConverter
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

            if (((GarageIndicateInfo)values[0]).GarageIndicate == GarageIndicate.InGarage &&
                ((StationPass)values[1]).IsStation && ((StationPass)values[1]).ID == ((GarageIndicateInfo)values[0]).StationID)
            {
                return CornerType.TopRight;
            }
            else if (((GarageIndicateInfo)values[0]).GarageIndicate == GarageIndicate.OutGarage &&
                ((StationPass)values[1]).IsStation && ((StationPass)values[1]).ID == ((GarageIndicateInfo)values[0]).StationID)
            {
                return CornerType.BottomLeft;
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}