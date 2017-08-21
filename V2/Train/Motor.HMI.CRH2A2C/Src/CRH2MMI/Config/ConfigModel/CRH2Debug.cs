using System.Xml.Serialization;

namespace CRH2MMI.Config.ConfigModel
{
    [XmlRoot]
    public class CRH2Debug
    {
        [XmlElement]
        public bool AutoLightUpScreen { set; get; }
    }
}
