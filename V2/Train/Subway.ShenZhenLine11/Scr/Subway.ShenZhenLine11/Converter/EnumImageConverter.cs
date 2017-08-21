using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Subway.ShenZhenLine11.Converter
{
    public class EnumImageConverter : IValueConverter
    {
        public List<ImageTuple> TupleImages { get; set; }

        public EnumImageConverter()
        {
            TupleImages = new List<ImageTuple>();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var iamge = TupleImages.FirstOrDefault(f => f.Key.Equals(value));
            if (iamge == null)
            {
                return DependencyProperty.UnsetValue;
            }
            return iamge.ImageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ImageTuple : DependencyObject
    {
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", typeof(ImageSource), typeof(ImageTuple), new PropertyMetadata(default(ImageSource)));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static void SetImageSource(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageSourceProperty, value);
        }

        public static ImageSource GetImageSource(DependencyObject obj)
        {
            return obj.GetValue(ImageSourceProperty) as ImageSource;
        }
        public System.Enum Key { internal set; get; }
    }
}