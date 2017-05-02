﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Subway.WuHanLine6.Converter
{
    /// <summary>
    /// 枚举类转化为图片Source
    /// </summary>
    public class EnumToImageConverter : IValueConverter
    {
        /// <summary>
        /// 图片源
        /// </summary>
        public List<ImageTuple> ImageSource { get; set; }

        /// <summary>
        ///
        /// </summary>
        public EnumToImageConverter()
        {
            ImageSource = new List<ImageTuple>();
        }

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <param name="value">绑定源生成的值。</param><param name="targetType">绑定目标属性的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imag = ImageSource.FirstOrDefault(f => Equals(f.Key, value));
            if (imag != null)
            {
                return imag.ImageSource;
            }
            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <param name="value">绑定目标生成的值。</param><param name="targetType">要转换到的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}