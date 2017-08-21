using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class EnumToColorBrush : IValueConverter
    {
        public List<EnumColorTuple> EnumColors { get; set; }

        public EnumToColorBrush()
        {
            EnumColors = new List<EnumColorTuple>();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = EnumColors.FirstOrDefault(f => Equals(f.Key, value));
            if (tmp == null)
            {
                return DependencyProperty.UnsetValue;
            }
            return tmp.ColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EnumColorTuple : DependencyObject
    {
        public static readonly DependencyProperty ColorBrushProperty = DependencyProperty.Register(
            "ColorBrush", typeof(SolidColorBrush), typeof(EnumColorTuple), new PropertyMetadata(default(SolidColorBrush)));

        public SolidColorBrush ColorBrush
        {
            get { return (SolidColorBrush)GetValue(ColorBrushProperty); }
            set { SetValue(ColorBrushProperty, value); }
        }

        public static void SetColorBrush(DependencyObject obj, SolidColorBrush brush)
        {
            obj.SetValue(ColorBrushProperty, brush);
        }

        public static SolidColorBrush GetColorBrush(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(ColorBrushProperty);
        }
        public Enum Key { internal set; get; }
    }
}