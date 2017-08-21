using System.Xml.Serialization;

namespace Motor.HMI.CRH5G.Config.ConfigModel
{
    public class CommunicationDataModel
    {
        [XmlAttribute]
        public CommunicationDataType DataType { set; get; }

        [XmlAttribute]
        public string Name { set; get; }

    }
}