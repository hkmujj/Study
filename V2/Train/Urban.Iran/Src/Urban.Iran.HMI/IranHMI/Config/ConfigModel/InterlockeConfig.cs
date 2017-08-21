using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Model;

namespace Urban.Iran.HMI.Config.ConfigModel
{
    [XmlRoot]
    public class InterlocksConfig
    {
        public List<InterlocksItem> Items { set; get; }
    }

    public class InterlocksItem : XmlRectangle
    {
        [XmlAttribute]
        public string Text { set; get; }

        public string InBoolNames { set; get; }
    }
}