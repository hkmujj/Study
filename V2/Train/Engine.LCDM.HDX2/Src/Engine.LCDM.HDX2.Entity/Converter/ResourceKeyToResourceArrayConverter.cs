using System;
using System.Globalization;
using System.Windows;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class ResourceKeyToStringResourceArrayConverter : ResourceKeyToResourceConverter
    {
        public string[] Separator { set; get; }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var res = base.Convert(values, targetType, parameter, culture);

            if (res != DependencyProperty.UnsetValue)
            {
                var s = res as string;
                if (s != null)
                {
                    return s.Split(Separator, StringSplitOptions.None);
                }

                return res;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}