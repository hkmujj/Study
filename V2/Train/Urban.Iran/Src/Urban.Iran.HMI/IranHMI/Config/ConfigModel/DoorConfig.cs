using System.Collections.Generic;
using System.Xml.Serialization;
using Urban.Domain.TrainState.Model;

namespace Urban.Iran.HMI.Config.ConfigModel
{
    [XmlRoot]
    public class DoorConfig
    {
        [XmlArray]
        public List<DoorListening> DoorListeningModelCollection { set; get; }
    }


    [XmlRoot]
    public class DoorListening : ListeningModel
    {
        [XmlAttribute]
        public string CarName { set; get; }

        [XmlAttribute]
        public string DoorName { set; get; }

        [XmlAttribute("OutRectangleTrainsideFalse")]
        public string OutRectangleTrainsideFalse { get; set; }
        [XmlAttribute("OutRectangleTrainsideTrue")]
        public string OutRectangleTrainsideTrue { get; set; }
    }

}