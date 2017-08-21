using System.Xml.Serialization;

namespace Urban.Iran.HMI.Config.ConfigModel
{
    [XmlRoot]
    public class IranDetailConfig
    {
        [XmlElement]
        public PasswordConfig PasswordConfig { set; get; }

        [XmlElement]
        public DoorConfig DoorConfig { set; get; }

        [XmlElement]
        public BogiesConfig BogiesConfig { set; get; }

        [XmlElement]
        public CarConfig CarConfig { set; get; }

        [XmlElement]
        public CommunicationOverviewConfig CommunicationOverviewConfig { set; get; }

    }
}