using System.Xml.Serialization;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.Rac
{
    [XmlRoot]
    public class EmputyRollConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        public GridConfig Grid { set; get; }
    }
}
