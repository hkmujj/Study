using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MMI.Facility.WPFInfrastructure.Converter;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.Converters
{
    public abstract class FindSpeedHookBase
    {
        private List<ISpeedModel> m_SpeedModelBuff;


        protected bool CanConvert(object[] values)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 9 || !(values[0] is ISpeedDialPlate) ||
                !(values[1] is ISpeed) || !(values[2] is SelectObjectContentConverter))
            {
                return false;
            }
            return true;
        }

        protected ISpeedDialPlate GetSpeedDialPlate(object[] values)
        {
            return (ISpeedDialPlate) values[0];
        }

        protected SelectObjectContentConverter GetSelectObjectContentConverter(object[] values)
        {
            return (SelectObjectContentConverter) values[2];
        }

        protected ISpeedModel GetMaxSpeedModel(object[] values)
        {
            var speedModel = (ISpeed) values[1];
            if (m_SpeedModelBuff == null)
            {
                m_SpeedModelBuff = new List<ISpeedModel>()
                {
                    speedModel.TargetSpeed,
                    speedModel.PermittedLimitSpeed,
                    speedModel.ServiceBrakeInterventionSpeed,
                    //speedModel.EmergencyBrakeInterventionSpeed
                };
            }

            return m_SpeedModelBuff.Where(w => w.SpeedColor != ATPColor.None).OrderBy(m => m.Value).LastOrDefault();
        }
    }

    public class FindSpeedHookColorConverter : FindSpeedHookBase, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!CanConvert(values))
            {
                return DependencyProperty.UnsetValue;
            }

            var max = GetMaxSpeedModel(values);

            if (max != null)
            {
                return GetSelectObjectContentConverter(values).Convert(max.SpeedColor, targetType, parameter, culture);
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FindSpeedHookStartAngleConverter : FindSpeedHookBase, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!CanConvert(values))
            {
                return DependencyProperty.UnsetValue;
            }

            var sd = GetSpeedDialPlate(values);

            var max = GetMaxSpeedModel(values);

            if (max != null)
            {
                return (double)sd.ConvertSpeedToDrawArcAngle(max.Value) - 4;
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FindSpeedHookEndAngleConverter : FindSpeedHookBase, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!CanConvert(values))
            {
                return DependencyProperty.UnsetValue;
            }

            var sd = GetSpeedDialPlate(values);

            var max = GetMaxSpeedModel(values);

            if (max != null)
            {
                return (double)sd.ConvertSpeedToDrawArcAngle(max.Value);
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}