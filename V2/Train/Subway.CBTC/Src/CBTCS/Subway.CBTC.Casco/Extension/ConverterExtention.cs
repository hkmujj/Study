using System.Linq;
using System.Windows;

namespace Subway.CBTC.Casco.Extension
{
    public static class ConverterExtention
    {
        public static bool IsNullOrUnset(this object[] values)
        {
            return values == null || values == DependencyProperty.UnsetValue
                   || values.Any(a => a == null || a == DependencyProperty.UnsetValue);
        }

        public static bool IsNullOrUnset(this object value)
        {
            return value == null || value == DependencyProperty.UnsetValue;
        }
    }
}
