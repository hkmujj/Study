using System;
using System.Globalization;
using System.Windows.Data;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    /// <summary>
    /// 表盘刻度旋转角度转换
    /// </summary>
    internal class DegreeScaleTextAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (double)value;
            return -v - 90;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}