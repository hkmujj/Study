using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace MMI.Facility.WPFInfrastructure.Converter
{
    /// <summary>
    /// 选择对象的内容
    /// </summary>
    public class SelectObjectContentConverter :IValueConverter
    {
        /// <summary>
        /// 对象的内容 集合
        /// </summary>
        public List<ObjectContentPair> ObjectContentCollection { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public SelectObjectContentConverter()
        {
            ObjectContentCollection = new List<ObjectContentPair>();
        }

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <param name="value">绑定源生成的值。</param><param name="targetType">绑定目标属性的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ei = ObjectContentCollection.FirstOrDefault(f => Equals(f.Key, value));
            if (ei == null)
            {
                return DependencyProperty.UnsetValue;
            }
            return ei.Content;
        }

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <param name="value">绑定目标生成的值。</param><param name="targetType">要转换到的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 对象的内容
    /// </summary>
    public class ObjectContentPair : DependencyObject
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof (object), typeof (ObjectContentPair), new PropertyMetadata(default(object)));

        /// <summary>
        /// 内容
        /// </summary>
        public object Content
        {
            get { return (object) GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

 
        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(
            "Key", typeof (object), typeof (ObjectContentPair), new PropertyMetadata(default(object)));

        /// <summary>
        /// key
        /// </summary>
        public object Key
        {
            get { return (object) GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }
    }
}