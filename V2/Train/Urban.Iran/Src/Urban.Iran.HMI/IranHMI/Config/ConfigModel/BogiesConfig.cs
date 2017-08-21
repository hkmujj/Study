using System.Collections.Generic;
using System.Xml.Serialization;
using Urban.Domain.TrainState.Model;

namespace Urban.Iran.HMI.Config.ConfigModel
{
    [XmlRoot]
    public class BogiesConfig
    {
        [XmlArray]
        public List<BogiesListening> BogiesListeningCollection { set; get; }
    }


    [XmlRoot]
    public class BogiesListening
    {
        [XmlAttribute]
        public string CarName { set; get; }

        [XmlArray]
        public List<AxisListening> AxisListeningCollection { set; get; }
    }

    [XmlRoot]
    public class AxisListening : ListeningModel
    {
        [XmlAttribute]
        public string AxisName { set; get; }

    }
}