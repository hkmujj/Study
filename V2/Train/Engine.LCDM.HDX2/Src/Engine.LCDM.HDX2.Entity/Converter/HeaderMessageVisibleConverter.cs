using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Converter
{
    public class HeaderMessageVisibleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (DependencyProperty.UnsetValue == values || values == null || values.Length < 2)
            {
                return Visibility.Hidden;
            }
            var msg = values[0] as Message;

            if (msg != null)
            {
                var stateInterface = values[1] as IStateInterface;

                if (stateInterface == null || (stateInterface.Id != StateInterfaceKey.Root && stateInterface.Id != StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo)))
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}