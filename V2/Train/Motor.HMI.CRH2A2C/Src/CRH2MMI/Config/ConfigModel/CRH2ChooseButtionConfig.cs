using System.Xml.Serialization;

namespace CRH2MMI.Config.ConfigModel
{
    [XmlType("Button")]
    public class CRH2ChooseButtionConfig : CRH2CommunicationPortRectangleModel
    {
        [XmlAttribute]
        public string Text { set; get; }


    }
}
