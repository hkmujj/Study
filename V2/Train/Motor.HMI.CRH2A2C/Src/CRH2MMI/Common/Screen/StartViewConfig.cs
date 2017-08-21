using System.Xml.Serialization;

namespace CRH2MMI.Common.Screen
{
    [XmlRoot]
    public class StartViewConfig
    {

        public int Speed { set; get; }

        public string MoveTail { set; get; }

        public uint StartPeriod { set; get; }
    }
}
