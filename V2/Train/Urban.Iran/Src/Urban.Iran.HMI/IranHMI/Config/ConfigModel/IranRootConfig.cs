using System.Collections.Generic;
using System.Xml.Serialization;
using Mmi.Communication.Index.Adapter;

namespace Urban.Iran.HMI.Config.ConfigModel
{
    [XmlRoot]
    public class IranRootConfig
    {
        [XmlArray]
        [XmlArrayItem("CommunicationConfig")]
        public List<CommunicationIndexConfig> CommunicationConfigs { set; get; }

        public void Correct(string rootConfigDirectory)
        {
            CommunicationConfigs.ForEach(e => e.Correct(rootConfigDirectory));
        }
    }
}