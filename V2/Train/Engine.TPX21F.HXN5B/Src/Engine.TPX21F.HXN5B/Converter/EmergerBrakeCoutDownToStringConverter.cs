using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.TPX21F.HXN5B.Converter
{
    public class EmergerBrakeCoutDownToStringConverter : IValueConverter
    {
        /// <summary>ת��ֵ��</summary>
        /// <returns>ת�����ֵ������÷������� null����ʹ����Ч�� null ֵ��</returns>
        /// <param name="value">��Դ���ɵ�ֵ��</param>
        /// <param name="targetType">��Ŀ�����Ե����͡�</param>
        /// <param name="parameter">Ҫʹ�õ�ת����������</param>
        /// <param name="culture">Ҫ����ת�����е������ԡ�</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue && value is int)
            {
                var se = (int)value;
                if (se > 0)
                {
                    return " " + se;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>ת��ֵ��</summary>
        /// <returns>ת�����ֵ������÷������� null����ʹ����Ч�� null ֵ��</returns>
        /// <param name="value">��Ŀ�����ɵ�ֵ��</param>
        /// <param name="targetType">Ҫת���������͡�</param>
        /// <param name="parameter">Ҫʹ�õ�ת����������</param>
        /// <param name="culture">Ҫ����ת�����е������ԡ�</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}