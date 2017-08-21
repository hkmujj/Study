using System.Xml.Serialization;


namespace CRH2MMI.Config.ConfigModel
{
    [XmlRoot]
    public class CRH2Config
    {
        public const string FileName = "CRH2Config.xml";

        [XmlElement]
        public CRH2Type Type { set; get; }

        [XmlElement]
        public ConfigFileModel DetailConfigFile { set; get; }

        [XmlElement]
        public CRH2Debug DebugConfig { set; get; }

        [XmlElement]
        public CRH2FileConfig CRH2FileConfig { set; get; }

        public CRH2Config()
        {
            DebugConfig = new CRH2Debug();
        }
    }
}
