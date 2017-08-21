using System.Xml.Serialization;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Common.Global
{
    [XmlRoot]
    public class GlobalInfoConfig
    {
        [XmlElement]
        public CRH2CommunicationPortModel Reversal { set; get; }
    }
}
