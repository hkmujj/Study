using System.Xml.Serialization;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Common.Screen
{
    [XmlRoot]
    public class TMSPowerConfig
    {
        [XmlElement]
        public CRH2CommunicationPortModel PowerOn { set; get; }
    }
}
