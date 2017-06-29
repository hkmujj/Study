using System.Xml.Serialization;
using Subway.CBTC.BeiJiaoKong.Models.Domain;

namespace Subway.CBTC.BeiJiaoKong.Config
{
    [XmlRoot]
    public class BeiJiaoKongProjectConfig
    {
        public const string FileName = "BeiJiaoKongProjectConfig.xml";
        [XmlElement]
        public TCTType Type { get; set; }
    }
}