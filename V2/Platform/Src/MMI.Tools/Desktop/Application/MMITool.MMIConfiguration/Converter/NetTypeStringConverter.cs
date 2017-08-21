using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using MMI.Facility.Interface.Data.Config.Net;

namespace MMITool.Addin.MMIConfiguration.Converter
{
    public class NetTypeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var netType = (NetType) value;
            return NetTypeHelper.AllNetTypeDictionary[netType];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var content = (string) value;
            return NetTypeHelper.AllNetTypeDictionary.First(f => f.Value == content).Key;
        }
    }
}