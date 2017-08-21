using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI.Config.ConfigModel
{
    [XmlRoot]
    public class CommunicationOverviewConfig
    {
        [XmlArray]
        [XmlArrayItem("Item")]
        public List<CommunicationOverviewItemConfig> CommunicationOverviewItems{set; get; }

        private static void Test()
        {
            var d = new CommunicationOverviewConfig()
            {
                CommunicationOverviewItems = new List<CommunicationOverviewItemConfig>()
            };

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    d.CommunicationOverviewItems.Add(new CommunicationOverviewItemConfig()
                    {
                        Horizontal = i,
                        Vertical = j,
                        DisplayName = "",
                        LogicIndexName = ""
                    });
                }
            }

            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }
    }

    public class CommunicationOverviewItemConfig : DisplayLocationAttribute
    {
        [XmlAttribute]
        public string DisplayName { set; get; }

        [XmlAttribute]
        public string LogicIndexName { set; get; }
    }

}