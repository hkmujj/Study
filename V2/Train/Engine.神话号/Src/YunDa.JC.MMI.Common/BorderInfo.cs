using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace YunDa.JC.MMI.Common
{
    [TypeConverter(typeof(BorderInfoConverter))]
    [Serializable]
    public class BorderInfo
    {
        [Browsable(true)]
        public Color BorderColor 
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }
        private Color _borderColor = Color.White;

        [Browsable(true)]
        public Int32 BorderWidth 
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }
        private Int32 _borderWidth = 1;

        public BorderInfo()
        { 
        }
    }

    public class BorderInfoConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(BorderInfo)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(BorderInfo)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is BorderInfo)
            {
                BorderInfo bi = (BorderInfo)value;
                return bi;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(BorderInfo),attributes);
        }
    }
}
