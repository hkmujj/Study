using System.Xml.Serialization;

namespace Subway.ATC.Casco.Config
{
    [XmlRoot]
    public class CascoATCConfig
    {
        [XmlIgnore]
        public const string FileName = "Subway.ATC.Casco.Config.xml";

        public UsedEnv UsedEnv { set; get; }
    }
}