using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MMI.Facility.Interface.Data.Config.Net;

namespace MMITool.Addin.MMIConfiguration.Converter
{
    public class NetDataProtocolTypeToStringConverter : IValueConverter
    {
        public static List<Tuple<NetDataProtocolType, string>> ItemTable { private set; get; }

        public static IEnumerable<string> ShowItems { get { return ItemTable.Select(s => s.Item2); } }

        static NetDataProtocolTypeToStringConverter()
        {
            ItemTable = new List<Tuple<NetDataProtocolType, string>>
            {
                new Tuple<NetDataProtocolType, string>(NetDataProtocolType.PackageIdOnly, "包头只有索引"),
                new Tuple<NetDataProtocolType, string>(NetDataProtocolType.BussnessIdAndPackageId, "包头包含业务类型和索引"),
            };
        }

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || !(value is NetDataProtocolType))
            {
                return DependencyProperty.UnsetValue;
            }

            var t = (NetDataProtocolType) value;
            var it = ItemTable.Find(f => f.Item1 == t);
            if (it != null)
            {
                return it.Item2;
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
            if (value == DependencyProperty.UnsetValue || !(value is string))
            {
                return DependencyProperty.UnsetValue;
            }

            var t = (string)value;
            var it = ItemTable.Find(f => f.Item2 == t);
            if (it != null)
            {
                return it.Item1;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}