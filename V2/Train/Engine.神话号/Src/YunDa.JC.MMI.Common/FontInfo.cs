using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace YunDa.JC.MMI.Common
{
    public enum Alignment
    {
        Near,
        Center,
        Far
    }

    [TypeConverter(typeof(FontInfoConverter))]
    [Serializable]
    public class FontInfo
    {
        [Browsable(true)]
        public Font DefFont
        {
            get { return _defFont; }
            set { _defFont = value; }
        }
        private Font _defFont = new Font("宋体", 13);

        [Browsable(true)]
        public Color FontColor
        {
            get { return _fontColor; }
            set
            {
                if (_fontColor == value) return;
                _fontColor = value;
            }
        }
        private Color _fontColor = Color.White;

        [Description("垂直方向的对齐方式")]
        [Browsable(true)]
        public Alignment Vertical
        {
            get { return _vertical; }
            set { _vertical = value; }
        }
        private Alignment _vertical = Alignment.Center;

        [Description("水平方向的对齐方式")]
        [Browsable(true)]
        public Alignment Horizontal
        {
            get { return _horizintal; }
            set { _horizintal = value; }
        }
        private Alignment _horizintal = Alignment.Center;

        public FontInfo()
        {
        }
    }

    public class FontInfoConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(FontInfo)) return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(FontInfo)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is FontInfo)
            {
                FontInfo fi = (FontInfo)value;
                return fi;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(FontInfo), attributes);
        }
    }
}
