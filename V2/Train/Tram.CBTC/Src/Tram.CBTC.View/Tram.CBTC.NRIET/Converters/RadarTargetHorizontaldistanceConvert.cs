using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.NRIET.Converters
{
    public class RadarTargetHorizontaldistanceConvert : IMultiValueConverter
    {
        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 4)
            {
                return DependencyProperty.UnsetValue;
            }

            //理论值，默认范围：0-100
            double CurDistance = (double)values[0];
            double MinDistance = (double)values[1];
            double MaxDistance = (double)values[2];


            //极值检测
            double tempVal = 0;
            if (MinDistance > MaxDistance)
            {
                tempVal = MinDistance;
                MinDistance = MaxDistance;
                MaxDistance = tempVal;
            }

            //边界检测
            if (CurDistance < MinDistance)
            {
                CurDistance = MinDistance;
            }

            if (CurDistance > MaxDistance)
            {
                CurDistance = MaxDistance;
            }

            //实际高度像素值
            double MinActualDistance = 0;
            double MaxActualDistance = (double)values[3];

            //减去雷达椭圆的宽度
            MaxActualDistance -= 11;

            double CurActualDistance = 0;


            //等比例转换
            if (Math.Abs(MaxDistance - (MinDistance)) > double.Epsilon)
            {
                CurActualDistance = ((CurDistance / (MaxDistance - (MinDistance))) *
                                     (MaxActualDistance - (MinActualDistance)));
            }

            return CurActualDistance;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}