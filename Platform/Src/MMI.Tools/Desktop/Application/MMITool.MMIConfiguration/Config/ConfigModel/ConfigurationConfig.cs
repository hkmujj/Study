using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using MMITool.Common.Util;

namespace MMITool.Addin.MMIConfiguration.Config.ConfigModel
{
    [XmlRoot]
    public class ConfigurationConfig
    {
        [XmlElement]
        public string TargetExeFilePath { set; get; }

        [XmlArray]
        [XmlArrayItem("Path")]
        public List<string> SelectableList { set; get; }

        [XmlIgnore]
        public string TargetExeFileFullPath { private set; get; }

        public void UpdateFullPath(string relativeParentPath)
        {
            if (!Path.IsPathRooted(TargetExeFilePath))
            {
                TargetExeFileFullPath = Path.Combine(relativeParentPath,
                    TargetExeFilePath);
                TargetExeFileFullPath = Path.GetFullPath(TargetExeFileFullPath);
            }
            else
            {
                TargetExeFileFullPath = TargetExeFilePath;
            }
        }

        public ConfigurationConfig(ConfigurationConfig other, bool withList = false)
        {
            SelectableList = new List<string>();
            if (other != null)
            {
                if (withList)
                {
                    SelectableList = new List<string>(other.SelectableList);
                }
                TargetExeFilePath = other.TargetExeFilePath;
            }
        }

        public ConfigurationConfig()
        {
            SelectableList = new List<string>();
        }

        private void Test()
        {
            var d = new ConfigurationConfig()
            {
                TargetExeFileFullPath = "D:\\a.xml",
                SelectableList = new List<string>() {"D:\\a.xml"}
            };
            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }
    }
}