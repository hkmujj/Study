using System.Xml.Serialization;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Common.Screen
{
    public class ButtonConfig : CRH2CommunicationPortRectangleModel
    {
        [XmlAttribute]
        public string Content { set; get; }

        [XmlAttribute]
        public string Tag { set; get; }
    }
}