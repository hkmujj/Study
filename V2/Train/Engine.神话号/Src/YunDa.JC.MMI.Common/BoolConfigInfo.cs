using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace YunDa.JC.MMI.Common
{
    [TypeConverter(typeof(BoolConfigInfoConverter))]
    [Serializable]
    public class BoolConfigInfo
    {
        [Browsable(true)]
        public Int32 BoolLogic
        {
            set { _boolLogic = value; }
            get { return _boolLogic; }
        }
        private Int32 _boolLogic;

        [Browsable(true)]
        public bool InputData
        {
            set { _inputData = value; }
            get { return _inputData; }
        }
        private bool _inputData=false;

        public BoolConfigInfo()
        {
        }

    }
    public class BoolConfigInfoConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(BoolConfigInfo)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(BoolConfigInfo)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is BoolConfigInfo)
            {
                BoolConfigInfo bi = (BoolConfigInfo)value;
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
            return TypeDescriptor.GetProperties(typeof(BoolConfigInfo), attributes);
        }
    }
}
