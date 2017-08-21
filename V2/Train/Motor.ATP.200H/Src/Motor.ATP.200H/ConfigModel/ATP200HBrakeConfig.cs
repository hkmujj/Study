using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace Motor.ATP._200H.ConfigModel
{
    public class ATP200HBrakeConfig
    {
        public const string FileName = "BrakeImageConfig.xml";
        /// <summary>
        /// 当前项目名称
        /// </summary>
        [XmlElement]
        public string ProjectName { get; set; }
        [XmlElement]
        public List<BrakeConfig> BrakeConfig { get; set; }

        // ReSharper disable once UnusedMember.Local
        private static void Test()
        {
            var a = new ATP200HBrakeConfig();
            a.ProjectName = "ChengDu";
            a.BrakeConfig = new List<BrakeConfig>()
            {
                new BrakeConfig()
                {
                    ProjectName = "ChengDu",
                    Display = DisplayType.Brake7
                },
                new BrakeConfig()
                {
                    ProjectName = "Other",
                    Display = DisplayType.Normal
                }
            };
            DataSerialization.SerializeToXmlFile(a, @"d:\a.xml");
        }
    }

    public class BrakeConfig
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [XmlAttribute]
        public string ProjectName { get; set; }

        /// <summary>
        /// 显示方式
        /// </summary>
        [XmlAttribute]
        public DisplayType Display { get; set; }
    }
}