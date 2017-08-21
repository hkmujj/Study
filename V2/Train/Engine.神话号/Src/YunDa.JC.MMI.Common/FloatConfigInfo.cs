using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YunDa.JC.MMI.Common
{
    [TypeConverter(typeof(FloatConfigInfoConverter))]
    [Serializable]
    public class FloatConfigInfo
    {
        [Browsable(true)]
        public Int32 FloatLogic
        {
            set { _floatLogic = value; }
            get { return _floatLogic; }
        }
        private Int32 _floatLogic;
        [Browsable(true)]
        public float InputData
        {
            set { _inputData = value; }
            get { return _inputData;}
        }
        private float _inputData;
    }

    public class FloatConfigInfoConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(FloatConfigInfo)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(FloatConfigInfo)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is FloatConfigInfo)
            {
                FloatConfigInfo bi = (FloatConfigInfo)value;
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
            return TypeDescriptor.GetProperties(typeof(FloatConfigInfo), attributes);
        }
    }
}
