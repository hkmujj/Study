using System.Xml.Serialization;
using CommonUtil.Util;

namespace Urban.ATC.Siemens.WPF.Control.Config
{
    [XmlRoot]
    public class StartConfig
    {
        public const string File = "StartTimeConfig";
        [XmlElement]
        public int StartTime { get; set; }

        private static void Test()
        {
            var date = new StartConfig();
            date.StartTime = 20;
            DataSerialization.SerializeToXmlFile(date, @"d:\StartTimeConfig");
        }
    }
}