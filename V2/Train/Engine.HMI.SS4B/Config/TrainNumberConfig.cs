using CommonUtil.Util;
using System.Xml.Serialization;

namespace SS4B_TMS.Config
{
    public class TrainNumberConfig
    {
        [XmlElement]
        public string Number { get; set; }

        public const string FileName = "TrainConfig.xml";

        public static void Test()
        {
            var s = new TrainNumberConfig();
            s.Number = "10001";
            DataSerialization.SerializeToXmlFile(s, @"d:\\TrainConfig.xml");
        }
    }
}