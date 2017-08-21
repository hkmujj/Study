using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Utils;

namespace MMITool.Addin.MMIDataDebugger.Converter
{
    public class FloatToByteStringConverter : IValueConverter
    {
        private byte[] m_BytesBuffer = new byte[4];

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                if (value is float)
                {
                    return string.Join(",", System.BitConverter.GetBytes((float) value));
                }
            }

            return DependencyProperty.UnsetValue;

        }

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                if (value is string)
                {
                    try
                    {
                        var s = (string) value;
                        var sa = s.Split(',');
                        var len = Math.Min(sa.Length, 4);
                        for (int i = 0; i < len; ++i)
                        {
                            m_BytesBuffer[i] = System.Convert.ToByte(sa[i]);
                        }
                        for (int i = len; i < 4; ++i)
                        {
                            m_BytesBuffer[i] = 0;
                        }

                        return BitConverter.ToSingle(m_BytesBuffer, 0);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            return 0;
        }
    }
}