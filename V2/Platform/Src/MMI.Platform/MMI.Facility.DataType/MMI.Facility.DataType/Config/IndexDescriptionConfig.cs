using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Util;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Attributes;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.IndexDescription;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.DataType.Config
{
    [ConfigureDescription("索引含义配置", FileName)]
    public class IndexDescriptionConfig : NotificationObject, IIndexDescriptionConfig
    {
        private ObservableCollection<ProjectIndexDescriptionConfig> m_ProjectIndexDescriptionConfigs;
        public const string FileName = "IndexDescriptionConfig.xml";

        public ObservableCollection<ProjectIndexDescriptionConfig> ProjectIndexDescriptionConfigs
        {
            set
            {
                if (Equals(value, m_ProjectIndexDescriptionConfigs))
                {
                    return;
                }
                m_ProjectIndexDescriptionConfigs = value;
                RaisePropertyChanged(() => ProjectIndexDescriptionConfigs);
            }
            get { return m_ProjectIndexDescriptionConfigs; }
        }

        [XmlIgnore]
        public ReadOnlyCollection<IProjectIndexDescriptionConfig> IndexDescriptionConfigCollection { get; private set; }

        public IProjectIndexDescriptionConfig GetProjectIndexDescriptionConfig(ICommunicationDataKey key)
        {
            return
                ProjectIndexDescriptionConfigs.Where(w => w.ProjectType == key.ProjectType)
                    .FirstOrDefault(f => f.AppNameCollection.Contains(key.AppName));
        }

        public IndexDescriptionConfig()
        {
            ProjectIndexDescriptionConfigs = new ObservableCollection<ProjectIndexDescriptionConfig>();
        }

        public void Initalize(string configPath)
        {
            if (ProjectIndexDescriptionConfigs != null)
            {
                foreach (var config in ProjectIndexDescriptionConfigs)
                {
                    config.Initalize(configPath);
                }

                IndexDescriptionConfigCollection =
                    new ReadOnlyCollection<IProjectIndexDescriptionConfig>(
                        ProjectIndexDescriptionConfigs.Cast<IProjectIndexDescriptionConfig>().ToList());

            }
        }


        // ReSharper disable once UnusedMember.Local
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
                            IndexType = AppIndexType.InBool
                        },
                        new ProjectIndexDescriptionConfigItem() {File = "x.xml", IndexType = AppIndexType.OutBool}
                    }),
                ProjectType = ProjectType.Signal

            };
            var da = new IndexDescriptionConfig() { ProjectIndexDescriptionConfigs = new ObservableCollection<ProjectIndexDescriptionConfig>(new List<ProjectIndexDescriptionConfig>() { d }) };
            DataSerialization.SerializeToXmlFile(da, "D:\\a.xml");
        }
    }
}