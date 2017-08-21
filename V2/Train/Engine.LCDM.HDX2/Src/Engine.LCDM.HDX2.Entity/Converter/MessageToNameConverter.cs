using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class MessageToNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == values)
            {
                return DependencyProperty.UnsetValue;
            }
            var msg = values[0] as Message;
            if (msg == null)
            {
                return DependencyProperty.UnsetValue;
            }
            var lan = (ResourceType)values[1] ;
            switch (lan)
            {
                case ResourceType.Ch:
                    return msg.Content.NameCH;
                    break;
                case ResourceType.En:
                    return msg.Content.NameEN;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}