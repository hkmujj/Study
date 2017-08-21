using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Excel.Interface;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Config.ConfigModel
{
    [DebuggerDisplay("ProjectType={ProjectType}")]
    [XmlRoot]
    public class ProjectConfig
    {
        [XmlIgnore]
        public ProjectType ProjectType { set; get; }

        public AccessControlConfig AccessControlConfig { set; get; }

        [XmlIgnore]
        public static string File
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\ProjectConfig.xml"); }
        }

    }

    public class AccessControlConfig
    {
        public bool CanSkipBeginViewByClickZeroButton { set; get; }
    }

    [XmlRoot]
    public class CRH3C380BFileConfig : ICombineFullPathPrivoder
    {
        [XmlElement]
        public List<CRH3C380BCommunicationConfigCollection> CRH3C380BCommunicationConfigCollection { set; get; }

        public void CombineFullPath(string appPath)
        {
            CRH3C380BCommunicationConfigCollection.ForEach(e => e.CombineFullPath(appPath));
        }
    }

    public class CRH3C380BCommunicationConfigCollection : ICombineFullPathPrivoder
    {
        [XmlAttribute]
        public ProjectType ProjectType { set; get; }

        [XmlElement]
        public List<CRH3C380BCommunicationConfig> CRH3C380BCommunicationConfig { set; get; }

        public void CombineFullPath(string appPath)
        {
            CRH3C380BCommunicationConfig.ForEach(e => e.CombineFullPath(appPath));
        }
    }

    public class CRH3C380BCommunicationConfig : ExcelReaderConfig, ICombineFullPathPrivoder
    {
        [XmlAttribute]
        public CommunicationDataType DataType { set; get; }

        public void CombineFullPath(string appPath)
        {
            var configPath = Path.Combine(appPath, "Config");
            File = Path.Combine(configPath, File);
        }
    }

    public interface ICombineFullPathPrivoder
    {
        void CombineFullPath(string appPath);
    }
}