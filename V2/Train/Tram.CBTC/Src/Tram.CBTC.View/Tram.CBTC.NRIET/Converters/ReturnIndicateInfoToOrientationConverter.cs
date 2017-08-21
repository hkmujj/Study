using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Microsoft.Expression.Media;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Road;


namespace Tram.CBTC.NRIET.Converters
{
    public class ReturnIndicateInfoToOrientationConverter : IMultiValueConverter
    {

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 2 || !(values[0] is ReturnIndicateInfo) || !(values[1] is StationPass))
            {
                return DependencyProperty.UnsetValue;
            }

            if (((ReturnIndicateInfo)values[0]).ReturnIndicate == ReturnIndicate.InReturn &&
                ((StationPass)values[1]).IsStation && ((StationPass)values[1]).ID == ((ReturnIndicateInfo)values[0]).StationID)
            {
                return ArrowOrientation.Left;
            }
            else if (((ReturnIndicateInfo)values[0]).ReturnIndicate == ReturnIndicate.OutReurn &&
                ((StationPass)values[1]).IsStation && ((StationPass)values[1]).ID == ((ReturnIndicateInfo)values[0]).StationID)
            {
                return ArrowOrientation.Right;
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}