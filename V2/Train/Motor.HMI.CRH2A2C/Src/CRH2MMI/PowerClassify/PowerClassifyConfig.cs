using System.Collections.Generic;
using System.Xml.Serialization;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.PowerClassify
{
    public class PowerClassifyConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlArray]
        [XmlArrayItem("Mtr")]
        public List<PowerClassifyUnitModel> MtrInBools { set; get; }

        [XmlArray]
        [XmlArrayItem("ACK1")]
        public List<PowerClassifyUnitModel> ACK1InBools { set; get; }

        [XmlArray]
        [XmlArrayItem("ACK2")]
        public List<PowerClassifyUnitModel> ACK2InBools { set; get; }

        [XmlArray]
        [XmlArrayItem("BKK")]
        public List<PowerClassifyUnitModel> BKKInBools { set; get; }

        [XmlArray]
        [XmlArrayItem("BKK2")]
        public List<PowerClassifyUnitModel> BKK2InBools { set; get; }

        [XmlArray]
        [XmlArrayItem("BKK")]
        public List<PowerClassifyButtonModel> BKKOutBools { set; get; }

        [XmlArray]
        [XmlArrayItem("APU")]
        public List<PowerClassifyUnitModel> APUInBools { set; get; }

    }

    public class PowerClassifyUnitModel : CRH2CommunicationPortModel
    {
        [XmlAttribute]
        public int CarNo { set; get; }

        [XmlAttribute]
        public int UnitNo { set; get; }
    }

    public class PowerClassifyButtonModel : CRH2CommunicationPortRectangleModel
    {
        [XmlAttribute]
        public string Text { set; get; }
    }
}
