using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Util;
using MMI.Facility.DataType.Attributes;
using MMI.Facility.DataType.Properties;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.DataType.Config
{
    [XmlRoot]
    [ConfigureDescription("系统配置", FileName)]
    public class SystemConfig : INotifyPropertyChanged, ISystemConfig
    {
        public const string FileName = "SystemConfig.xml";

        private List<ISubsystemConfig> m_ISubsystemConfigs;
        private ReadOnlyCollection<ISubsystemConfig> m_SubsystemConfigCollection;
        private bool m_IsDebugModel;
        private StartModel m_StartModel;
        private ObservableCollection<SubsystemConfig> m_SubsystemConfigs;

        public bool IsDebugModel
        {
            get { return m_IsDebugModel; }
            set
            {
                if (value.Equals(m_IsDebugModel))
                {
                    return;
                }
                m_IsDebugModel = value;
                OnPropertyChanged("IsDebugModel");
            }
        }

        public StartModel StartModel
        {
            get { return m_StartModel; }
            set
            {
                if (value == m_StartModel)
                {
                    return;
                }
                m_StartModel = value;
                OnPropertyChanged("StartModel");
            }
        }

        public ObservableCollection<SubsystemConfig> SubsystemConfigs
        {
            get { return m_SubsystemConfigs; }
            set
            {
                if (Equals(value, m_SubsystemConfigs))
                {
                    return;
                }
                m_SubsystemConfigs = value;
                OnPropertyChanged("SubsystemConfigs");
                OnPropertyChanged("SubsystemConfigCollection");
            }
        }

        [XmlIgnore]
        public ReadOnlyCollection<ISubsystemConfig> SubsystemConfigCollection
        {
            get
            {
                if (m_SubsystemConfigCollection == null)
                {
                    m_ISubsystemConfigs = SubsystemConfigs.Cast<ISubsystemConfig>().ToList();
                    m_SubsystemConfigCollection = new ReadOnlyCollection<ISubsystemConfig>(m_ISubsystemConfigs);
                }
                return m_SubsystemConfigCollection;
            }
        }

        public static void Test()
        {
            var d = new SystemConfig()
                    {
                        SubsystemConfigs = new ObservableCollection<SubsystemConfig>()
                                           {
                                               new SubsystemConfig(){ SubsystemType = SubsystemType.Addin, Name = "1D"},
                                               new SubsystemConfig(),
                                           }
                    };

            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
