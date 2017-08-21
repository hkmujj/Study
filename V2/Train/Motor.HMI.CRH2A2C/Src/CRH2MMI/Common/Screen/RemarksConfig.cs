using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Model;
using CommonUtil.Util.Extension;

namespace CRH2MMI.Common.Screen
{
    /// <summary>
    /// 备注配置
    /// </summary>
    [XmlRoot]
    public class RemarksConfig
    {
        [XmlElement(Type = typeof(TextRemarkItem))]
        [XmlElement(Type = typeof(RoudnessRemarkItem))]
        public List<RemarkItem> RemarkItems { set; get; }
    }

    public class RemarkItem : XmlRectangle
    {
        [XmlAttribute]
        public bool BackgroudColorVisible { set; get; }

        [XmlAttribute("ForgroundColor")]
        public string ForgroundColorString { set; get; }

        [XmlIgnore]
        public Color ForgroundColor
        {
            get { return ForgroundColorString != null ? ColorTranslator.FromHtml(ForgroundColorString) : Color.White; }
        }

        [XmlAttribute("BackgroundColor")]
        public string BackgroundColorString { set; get; }

        [XmlIgnore]
        public Color BackgroundColor
        {
            get
            {
                return BackgroundColorString != null ? ColorTranslator.FromHtml(BackgroundColorString) : Color.White;
            }
        }
    }

    [XmlType]
    public class TextRemarkItem : RemarkItem
    {
        [XmlAttribute]
        public string Text { set; get; }
    }

    [XmlType]
    public class RoudnessRemarkItem : RemarkItem
    {
        [XmlIgnore]
        public int R { get { return Math.Min(Rectangle.Width/2, Rectangle.Height/2); } }

        [XmlIgnore]
        public Point Center { get { return Rectangle.GetCenterPoint(); } }
    }
}