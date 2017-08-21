using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Subway.XiaMenLine1.Subsystem.Converter
{
    public class DateTimeToTitleShowingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var dt = (DateTime) value;

            return string.Format("{0}    {1}    {2}", GetDayOfWeek(dt), dt.ToString("yyyy-M-d"),
                dt.ToString("hh: mm: ss"));
        }

        private string GetDayOfWeek(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "周日";
                case DayOfWeek.Monday:
                    return "周一";
                case DayOfWeek.Tuesday:
                    return "周二";
                case DayOfWeek.Wednesday:
                    return "周三";
                case DayOfWeek.Thursday:
                    return "周四";
                case DayOfWeek.Friday:
                    return "周五";
                case DayOfWeek.Saturday:
                    return "周六";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}