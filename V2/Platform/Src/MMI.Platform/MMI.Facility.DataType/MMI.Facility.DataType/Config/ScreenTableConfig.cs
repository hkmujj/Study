using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Util;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.DataType.Config
{
    [XmlRoot]
    public class ScreenTableConfig : IScreenTableConfig
    {
        private List<IScreenItem> m_ScreenItems;
        public const string FileName = "ScreenTableConfig.xml";

        [XmlArray("ScreenCollection")]
        [XmlArrayItem("ScreenItem")]
        public List<ScreenItem> ConcreateScreenItems { set; get; }

        /// <summary>
        /// 屏元素表。
        /// </summary>
        [XmlIgnore]
        public List<IScreenItem> ScreenItems
        {
            get { return m_ScreenItems ?? (m_ScreenItems = ConcreateScreenItems.Select(s => (IScreenItem)s).ToList()); }
        }

        // ReSharper disable once UnusedMember.Local
        private static void Tets()
        {
            var data = new ScreenTableConfig()
            {
                ConcreateScreenItems =
                    new List<ScreenItem>()
                    {
                        new ScreenItem() {Key = "300S", ProjectCollection = new List<string>() {"300S"}},
                        new ScreenItem() {Key = "CRH2", ProjectCollection = new List<string>() {"CRH2", "CRH2_2"}}
                    }
            };

            DataSerialization.SerializeToXmlFile(data, Path.Combine("D:\\", FileName));
        }

    }

    [XmlRoot]
    public class ScreenItem : IScreenItem
    {
        /// <summary>
        /// 关键字，主控发送
        /// </summary>
        [XmlAttribute]
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Project")]
        public List<string> ProjectCollection { get; set; }
    }
}