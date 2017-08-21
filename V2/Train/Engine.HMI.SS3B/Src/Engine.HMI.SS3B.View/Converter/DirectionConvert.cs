using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using CommonUtil.Util;
using Engine.HMI.SS3B.Interface.ViewState;

namespace Engine.HMI.SS3B.View.Converter
{
    public class DirectionConvert:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            return EnumUtil.GetDescription((Direction)value).FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}