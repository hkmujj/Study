using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.LightTrans
{
    public class LightTrainsConfig
    {
        [XmlElement("LightTransPage")]
        public List<LightTransPage> LightTransPages { set; get; }
    }

    public class LightTransPage
    {
        [XmlAttribute]
        public LightTransPageID Page { set; get; }

        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlArray]
        [XmlArrayItem("LightUnit")]
        public List<LightUnitConfig> LightUnitConfigs { set; get; }
    }

    public class LightUnitConfig  : CRH2CommunicationPortModel
    {

        [XmlElement("Line")]
        public List<LineConfig> Lines { set; get; }
    }

    [DebuggerDisplay("CarName={CarName}  PointType={PointType}")]
    public class LineConfig
    {
        [XmlAttribute]
        public int CarName { set; get; }

        [XmlAttribute("Type")]
        public EndPointType PointType { set; get; }
    }


}
