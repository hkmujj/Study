using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CommonUtil.Model;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Model;
using Motor.HMI.CRH1A.Config.ConfigModel;

namespace Motor.HMI.CRH1A.SystemMenu
{
    [XmlRoot]
    public class SystemMenuConfig 
    {

        [XmlElement]
        public SystemMenuRegion Region { set; get; } 
    }

    [DebuggerDisplay("Rectangle={Rectangle} RowCount={RowCount} ColumnCount={ColumnCount}")]
    public class SystemMenuRegion : XmlRectangle
    {
        [XmlAttribute]
        public uint RowCount { set; get; }

        [XmlAttribute]
        public uint ColumnCount { set; get; }

        [XmlAttribute]
        public int XOffset { set; get; }

        [XmlAttribute]
        public int YOffset { set; get; }

        [XmlAttribute]
        public int XInterval { set; get; }

        [XmlAttribute]
        public int YInterval { set; get; }

        [XmlAttribute]
        public int ButtonWidth { set; get; }

        [XmlAttribute]
        public int ButtonHeight { set; get; }

        [XmlElement("ButtonConfig")]
        public List<SystemMenuButtonConfig> ButtonConfigs { set; get; }
    }

    [DebuggerDisplay(" Text={Text} RowIndex={RowIndex} ColoumnIndex={ColoumnIndex}  Goto={Goto}  ")]
    public class SystemMenuButtonConfig
    {

        [XmlAttribute]
        public int ColoumnIndex { set; get; }

        [XmlAttribute]
        public int RowIndex { set; get; }

        [XmlAttribute]
        public ViewConfig Goto { set; get; }

        [XmlAttribute]
        public string Text { set; get; }
    }
}
