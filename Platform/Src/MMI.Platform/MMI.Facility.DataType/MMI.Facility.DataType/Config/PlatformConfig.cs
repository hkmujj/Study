using System.Xml.Serialization;

namespace MMI.Facility.DataType.Config
{
    [XmlRoot]
    public class PlatformConfig
    {
        [XmlIgnore]
        public const string FileName = "PlatformConfig.xml";

        [XmlElement]
        public bool WaitForDebugger { set; get; }
    }
}