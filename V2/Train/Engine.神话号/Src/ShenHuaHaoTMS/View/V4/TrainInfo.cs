using System;
using ShenHuaHaoTMS.View.V4;
using System.Xml.Serialization;

namespace ShenHuaHaoTMS.View
{
    [XmlRoot("机车配置")]
    [Serializable]
    public class TrainInfo
    {
        [XmlIgnore]
        public TrainType TrainType { get; set; }

        [XmlIgnore]
        public Int32 TrainID { get; set; }
        
        [XmlElement("机车编号")]
        public Int32 RealTrainID { get; set; }

        [XmlIgnore]
        public Direction Direction { get; set; }

        [XmlIgnore]
        public ControlType ControlType { get; set; }

        [XmlIgnore]
        public Int32 Number { get; set; }

        [XmlIgnore]
        public Int32 Count { get; set; }

        [XmlElement("解除编组反馈时间")]
        public Int32 CountDwon { get; set; }


        [XmlElement("是否显示光标")]
        public Int32 HideCursor { get; set; }

        public TrainInfo()
        {
        }
    }
}
