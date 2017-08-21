using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace CRH2MMI.Title
{
    [XmlRoot]
    [XmlType("TrainLine")]
    [DebuggerDisplay("Index = {Index}, Name = {Name}")]
    public class TrainLineConfig
    {
        [XmlAttribute]
        public int Index { set; get; }

        [XmlAttribute]
        public string Name { set; get; }
    }

    class TrainLineConfigTest
    {
        public static void Test()
        {
            var data = new List<TrainLineConfig>()
            {
                new TrainLineConfig() {Index = 0, Name = "成渝线"},
                new TrainLineConfig() {Index = 1, Name = "成混线"}
            };
            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml", "Root");
        }

    }
}
