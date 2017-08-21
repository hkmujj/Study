using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using CommonUtil.Util;
//using CommonUtil.Util;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.IndexDescription;


namespace MMI.Facility.DataType.Config
{
    [XmlRoot]
    public class ProjectIndexDescriptionConfig : NotificationObject, IProjectIndexDescriptionConfig
    {
        private ProjectType m_ProjectType;
        private ObservableCollection<string> m_AppNames;

        private const string ColName = "Name";
        private const string ColIndex = "Index";

        [XmlIgnore]
        public ProjectType ProjectType
        {
            get { return m_ProjectType; }
            set
            {
                if (value == m_ProjectType)
                {
                    return;
                }
                m_ProjectType = value;
                RaisePropertyChanged(() => ProjectType);
            }
        }

        [XmlArray]
        [XmlArrayItem("Name")]
        public ObservableCollection<string> AppNames
        {
            set
            {
                if (value == m_AppNames)
                {
                    return;
                }
                m_AppNames = value;
                AppNameCollection = new ReadOnlyCollection<string>(value);
                RaisePropertyChanged(() => AppNames);
            }
            get { return m_AppNames; }
        }

        [XmlIgnore]
        public string ConfigFile { set; get; }

        [XmlArray]
        [XmlArrayItem("Item")]
        public ObservableCollection<ProjectIndexDescriptionConfigItem> ConfigItems { set; get; }


        [XmlIgnore]
        public ReadOnlyCollection<string> AppNameCollection { get; private set; }

        [XmlIgnore]
        public IReadOnlyDictionary<string, int> InBoolDescriptionDictionary { get; private set; }

        [XmlIgnore]
        public IReadOnlyDictionary<string, int> InFloatDescriptionDictionary { get; private set; }

        [XmlIgnore]
        public IReadOnlyDictionary<string, int> OutBoolDescriptionDictionary { get; private set; }

        [XmlIgnore]
        public IReadOnlyDictionary<string, int> OutFloatDescriptionDictionary { get; private set; }

        public void Initalize(string systemConfigPath)
        {
            foreach (var it in ConfigItems)
            {
                var dic = new Dictionary<string, int>();

                LoadFile(systemConfigPath, it, dic);

                UpdateDescriptionDic(it, dic);
            }
        }

        private void UpdateDescriptionDic(ProjectIndexDescriptionConfigItem it, Dictionary<string, int> dic)
        {
            switch (it.IndexType)
            {
                case AppIndexType.InBool:
                    if (InBoolDescriptionDictionary != null)
                    {
                        SysLog.Warn(
                            "InBoolDescriptionDictionary has been setted ! the new dictionary will replace it , which file ={0} ",
                            it.File);
                    }
                    InBoolDescriptionDictionary = DictionaryExtention.AsReadOnlyDictionary(dic);
                    break;
                case AppIndexType.OutBool:
                    if (OutBoolDescriptionDictionary != null)
                    {
                        SysLog.Warn(
                            "InBoolDescriptionDictionary has been setted ! the new dictionary will replace it , which file ={0} ",
                            it.File);
                    }
                    OutBoolDescriptionDictionary = DictionaryExtention.AsReadOnlyDictionary(dic);
                    break;
                case AppIndexType.InFloat:
                    if (InFloatDescriptionDictionary != null)
                    {
                        SysLog.Warn(
                            "InBoolDescriptionDictionary has been setted ! the new dictionary will replace it , which file ={0} ",
                            it.File);
                    }
                    InFloatDescriptionDictionary = DictionaryExtention.AsReadOnlyDictionary(dic);
                    break;
                case AppIndexType.OutFloat:
                    if (OutFloatDescriptionDictionary != null)
                    {
                        SysLog.Warn(
                            "InBoolDescriptionDictionary has been setted ! the new dictionary will replace it , which file ={0} ",
                            it.File);
                    }
                    OutFloatDescriptionDictionary = DictionaryExtention.AsReadOnlyDictionary(dic);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void LoadFile(string systemConfigPath, ProjectIndexDescriptionConfigItem it, Dictionary<string, int> dic)
        {
            var file = Path.Combine(systemConfigPath, it.File);
            it.File = file;
            if (File.Exists(file))
            {
                var ds = it.Adapter();
                if (ds.Tables.Count != 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (Convert.IsDBNull(row[it.Coloumns[1].Name]))
                        {
                            SysLog.Debug("Index is DbNull in file {0}, where Name={1}, Index={2}",
                                it.File,
                                row[it.Coloumns[0].Name], row[it.Coloumns[1].Name]);
                        }
                        else
                        {
                            try
                            {
                                dic.Add(row[it.Coloumns[0].Name].ToString(), Convert.ToInt32(row[it.Coloumns[1].Name]));
                            }
                            catch (Exception)
                            {
                                SysLog.Warn("Can not convert index in file {0}, where Name={1}, Index={2}",
                                    it.File,
                                    row[it.Coloumns[0].Name], row[it.Coloumns[1].Name]);
                            }
                        }
                    }
                }
                else
                {
                    SysLog.Warn("there is not any index description in file:{0}", file);
                }
            }
            else
            {
                SysLog.Warn("Can not found file {0} when initalize index descriptions.", it.File);
            }
        }

        private static void Test()
        {
            var d = new ProjectIndexDescriptionConfig()
            {
                AppNames = new ObservableCollection<string>(new List<string>()
                {
                    "a",
                    "b",
                    "c"
                }),
                ConfigItems =
                    new ObservableCollection<ProjectIndexDescriptionConfigItem>(new List
                        <ProjectIndexDescriptionConfigItem>()
                    {
                        new ProjectIndexDescriptionConfigItem()
                        {
                            File = "xm.xml",
                            IndexType = AppIndexType.InBool,
                            SheetNames = new List<string>(){ }
                        },
                        new ProjectIndexDescriptionConfigItem() {File = "x.xml", IndexType = AppIndexType.OutBool}
                    }),
                ProjectType = ProjectType.Signal

            };
            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");

            var dt = DataSerialization.DeserializeFromXmlFile<ProjectIndexDescriptionConfig>("D:\\a.xml");
        }
    }
}