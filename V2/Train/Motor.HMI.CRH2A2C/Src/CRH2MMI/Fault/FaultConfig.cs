using System.Xml.Serialization;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.Fault
{
    public class FaultConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlElement]
        public double UpdateIntervale { set; get; }

        [XmlAttribute]
        public string Speed { set; get; }

        [XmlAttribute]
        public string Distance { set; get; }

    }
}
