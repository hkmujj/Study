using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Converter
{
    public class EnumToImageConverter : IValueConverter
    {
        public List<EnumImageTuple> EnumImages { set; get; }

        public EnumToImageConverter()
        {
            EnumImages = new List<EnumImageTuple>();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ei = EnumImages.FirstOrDefault(f => Equals(f.Key, value));
            if (ei == null)
            {
                return DependencyProperty.UnsetValue;
            }
            return ei.ImageSource;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class EnumImageTuple : DependencyObject
    {
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", typeof (ImageSource), typeof (EnumImageTuple), new PropertyMetadata(default(ImageSource)));

        public static void SetImageSource(DependencyObject element, ImageSource value)
        {
            element.SetValue(ImageSourceProperty, value);
        }

        public static ImageSource GetImageSource(DependencyObject element)
        {
            return (ImageSource) element.GetValue(ImageSourceProperty);
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, ImageSource); }
        }

        public Enum Key { internal set; get; }

        
    }
}