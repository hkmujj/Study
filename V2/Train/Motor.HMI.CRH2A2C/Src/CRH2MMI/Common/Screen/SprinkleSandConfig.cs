using System.Collections.Generic;
using System.Xml.Serialization;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.Common.Screen
{
    [XmlRoot]
    public class SprinkleSandConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlArray]
        [XmlArrayItem("Grid")]
        public List<GridConfig> GridContents { set; get; }

        public RemarksConfig RemarksConfig { set; get; }

        public List<ButtonConfig> ButtonConfigs { set; get; }
    }

    
}