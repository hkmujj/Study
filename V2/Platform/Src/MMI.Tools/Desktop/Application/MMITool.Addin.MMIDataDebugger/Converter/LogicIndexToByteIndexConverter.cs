using System;
using System.Globalization;
using System.Windows.Data;

namespace MMITool.Addin.MMIDataDebugger.Converter
{
    public class LogicIndexToByteIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (int) value;
            return string.Format("{0}--{1}", index/8, index%8);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}