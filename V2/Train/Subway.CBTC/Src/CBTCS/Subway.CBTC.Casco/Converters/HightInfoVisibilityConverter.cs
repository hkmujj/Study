using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Msg.Details;
using Microsoft.Practices.ServiceLocation;
using Subway.CBTC.Casco.ViewModel;

namespace Subway.CBTC.Casco.Converters
{
    public class HightInfoVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return Visibility.Hidden;
            }
            var tempValue = (List<IInformationContent>)value;
            var va = tempValue.FirstOrDefault();
            if (va != null)
            {
                var va1 = ServiceLocator.Current.GetInstance<DomainViewModel>().CBTC.Other.ShowingDateTime - va.HappenDate;
                if (va1.TotalSeconds <= 10)
                {
                    return Visibility.Hidden;
                }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
