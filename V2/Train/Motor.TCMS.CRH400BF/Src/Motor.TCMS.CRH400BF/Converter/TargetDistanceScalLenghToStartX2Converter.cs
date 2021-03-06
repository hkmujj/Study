﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Motor.TCMS.CRH400BF.Model.Interface;

namespace Motor.TCMS.CRH400BF.Converter
{
    // <summary>
    /// 目标距离到起始 X 坐标的转换
    /// </summary>
    public class TargetDistanceScalLenghToStartX2Converter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Any(a => a == DependencyProperty.UnsetValue) || !( values[0] is ITargitDistanceScaleItem ))
            {
                return DependencyProperty.UnsetValue;
            }
            return ( 1 - ( (ITargitDistanceScaleItem)values[0] ).DegreeLength ) * System.Convert.ToDouble(values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
