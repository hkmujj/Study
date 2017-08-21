using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using LightRail.HMI.SZLHLF.Model.TrainModel;

namespace LightRail.HMI.SZLHLF.Converter
{
    public class DirectionArrowRightMulConverter : IMultiValueConverter
    {
        private enum DirectionArrowStatus
        {
            /// <summary>
            ///左侧
            DirectionLeft = 0,
            ///左侧
            DirectionRight,
            ///不需显示
            Normal,
            /// </summary>
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 3 || (!(values[0] is DirectionArrow) && !(values[1] is Drivers) && !(values[2] is Drivers)))
            {
                return DependencyProperty.UnsetValue;
            }

            Array xArray = parameter as Array;

            switch ((DirectionArrow)values[0])
            {
                case DirectionArrow.Front:
                    if (values[2] != DependencyProperty.UnsetValue && (Drivers)values[2] == Drivers.Occupy)
                    {
                        return xArray.GetValue((int)DirectionArrowStatus.DirectionRight);
                    }
                    break;
                case DirectionArrow.After:
                    if (values[1] != DependencyProperty.UnsetValue && (Drivers)values[1] == Drivers.Occupy)
                    {
                        return xArray.GetValue((int)DirectionArrowStatus.DirectionRight);
                    }
                    break;

                case DirectionArrow.UnNormal:
                    return DependencyProperty.UnsetValue;
                default:
                    return DependencyProperty.UnsetValue;
            }
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}