using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace Engine.Dial.Turkmenistan.Config
{
    public enum DialType
    {
        Pointer,
        Dial,
    }
    public class DialConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public DialType DialType { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [XmlAttribute]
        public int Index { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        [XmlAttribute]
        public double Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [XmlAttribute]
        public double Height { get; set; }
        /// <summary>
        /// 上边距
        /// </summary>
        [XmlAttribute]
        public double Top { get; set; }
        /// <summary>
        /// 左边距
        /// </summary>
        [XmlAttribute]
        public double Left { get; set; }
        /// <summary>
        /// 右边距
        /// </summary>
        [XmlAttribute]
        public double Right { get; set; }
        /// <summary>
        /// 下边距
        /// </summary>
        [XmlAttribute]
        public double Bottom { get; set; }
        [XmlAttribute]
        public int ZIndex { get; set; }
        /// <summary>
        /// 角度
        /// </summary>
        [XmlAttribute]
        public double Angle { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        [XmlAttribute]
        public string ImagePath { get; set; }
        [XmlAttribute]
        public double InitAngle { get; set; }
        [XmlAttribute]
        public double MaxAngle { get; set; }
        [XmlAttribute]
        public double MinValue { get; set; }
        [XmlAttribute]
        public double MaxValue { get; set; }
    }

    public class TextConfig
    {
        /// <summary>
        /// 字体大小
        /// </summary>
        [XmlAttribute]
        public double FontSize { get; set; }
        /// <summary>
        /// 长宽度
        /// </summary>
        [XmlAttribute]
        public double LongWith { get; set; }
        /// <summary>
        /// 短宽度
        /// </summary>
        [XmlAttribute]
        public double ShortWith { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int ZIndex { get; set; }
        /// <summary>
        /// 上边距
        /// </summary>
        [XmlAttribute]
        public double Top { get; set; }
        /// <summary>
        /// 左边距
        /// </summary>
        [XmlAttribute]
        public double Left { get; set; }
        /// <summary>
        /// 右边距
        /// </summary>
        [XmlAttribute]
        public double Right { get; set; }
        /// <summary>
        /// 下边距
        /// </summary>
        [XmlAttribute]
        public double Bottom { get; set; }
    }
    [XmlRoot]
    public class TurkmenistanDialConfig
    {
        public const string FileName = "TurkmenistanDialConfig.xml";
        [XmlArray]
        public List<DialConfig> Dials { get; set; }
        [XmlArray]
        public List<TextConfig> Texts { get; set; }

    }
}
