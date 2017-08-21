using System.Collections.Generic;
using System.Xml.Serialization;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.CutState
{
    [XmlRoot]
    public class CutInfoConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlElement("Grid")]
        public List<GridConfig> GridLine { set; get; }
    }
}
