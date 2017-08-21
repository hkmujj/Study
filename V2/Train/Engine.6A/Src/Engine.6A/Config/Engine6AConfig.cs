using System.Xml.Serialization;
using CommonUtil.Util;
using Engine._6A.Enums;

namespace Engine._6A.Config
{
    public class Engine6AConfig
    {
        public const string FileName = "Engine6AConfig.xml";
        [XmlElement]
        public Engine6AType Type { get; set; }

        private static void Test()
        {
            var s = new Engine6AConfig();
            s.Type = Engine6AType.Alex6;
            DataSerialization.SerializeToXmlFile(s, @"d:\text.xml");
        }
    }
}