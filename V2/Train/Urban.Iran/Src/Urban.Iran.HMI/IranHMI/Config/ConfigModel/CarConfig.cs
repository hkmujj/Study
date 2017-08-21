using System.Collections.Generic;
using System.Xml.Serialization;
using Urban.Domain.TrainState.Model;

namespace Urban.Iran.HMI.Config.ConfigModel
{
    [XmlRoot]
    public class CarConfig
    {
        [XmlArray]
        public List<CarListening> CarListeningCollection { set; get; }
    }

    [XmlRoot]
    public class CarListening
    {
        [XmlAttribute]
        public string CarName { set; get; }

        [XmlArray]
        [XmlArrayItem("CarBrakingListening")]
        public List<ListeningModel> CarBrakingListeningCollection { set; get; }
    }

}