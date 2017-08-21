using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Model;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.TelentControl
{
    [XmlRoot]
    public class TeleControlConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlArray]
        public List<TeleRectangleConfig> TeleRectangles { set; get; }

        [XmlArray]
        public List<TeleOutBoolConfig> TeleOutBoolConfigs { set; get; }
    }

    public class TeleOutBoolConfig : CRH2CommunicationPortModel
    {
        [XmlAttribute]
        public TeleControlBtnType Type { set; get; }

        [XmlAttribute]
        public int Unit { set; get; }
    }

    public class TeleRectangleConfig : XmlRectangle
    {
        [XmlAttribute]
        public TeleControlBtnType Type { set; get; }

    }


}
