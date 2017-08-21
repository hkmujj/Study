using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Engine.LCDM.HXD3.Converter
{
    //public class ButtonConverter:IMultiValueConverter
    //{
    //    public object Convert(object[] values,Type targetType,object parameter,CultureInfo culture)
    //    {
    //        //bool str=(bool)values[0];
    //        //string value=str?(string)values[1]:(string)values[2];
    //        //return value;
    //        return "";
    //    }
    //    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class ButtonContentSelectConveter : DependencyObject, IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty OkContentProperty = DependencyProperty.Register(
            "OkContent", typeof(string), typeof(ButtonContentSelectConveter), new PropertyMetadata(default(string)));

        /// <summary>
        /// 文本框水平对齐方式
        /// </summary>
        public string OkContent { get { return (string)GetValue(OkContentProperty); } set { SetValue(OkContentProperty, value); } }

        public static readonly DependencyProperty ExContentProperty = DependencyProperty.Register(
            "ExContent", typeof(string), typeof(ButtonContentSelectConveter), new PropertyMetadata(default(string)));
        public string ExContent { get { return (string)GetValue(ExContentProperty); } set { SetValue(ExContentProperty, value); } }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool jud = (bool)value;
            string result = jud ? ExContent : OkContent;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
