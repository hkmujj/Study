using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Station;
using Motor.HMI.CRH1A.SystemMenu;

namespace Motor.HMI.CRH1A.Config.ConfigModel
{
    [XmlRoot]
    public class CRH1ADetailConfig
    {
        [XmlIgnore]
        public string File { set; get; }

        [XmlArray]
        public List<PasswordConfig> PasswordConfigs { set; get; }

        [XmlArray]
        [XmlArrayItem("Value")]
        public List<int> DefaultFaultLogicCollection { set; get; }

        [XmlElement]
        public StationConfig StationConfig { set; get; }

        [XmlElement]
        public SystemMenuConfig SystemMenuConfig { set; get; }

        public List<MainNode> MainNode { get; set; }

        private static void Test()
        {
            var a = new CRH1ADetailConfig();
            a.MainNode = new List<MainNode>()
            {
                new MainNode()
                {
                    Unit = "1",Num = 10
                },new MainNode()
                {
                    Unit = "2",Num = 20
                }
            };

            DataSerialization.SerializeToXmlFile(a, @"d:\a.xml");
        }
    }
    [XmlType("MainNodeNum")]
    public class MainNode
    {
        public string Unit { get; set; }
        public int Num { get; set; }
    }
}
