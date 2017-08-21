using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Util;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Attributes;
using MMI.Facility.DataType.Config.Form;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Form;
using MMI.Facility.Interface.IndexDescription;

namespace MMI.Facility.DataType.Config
{
    [XmlRoot]
    [DebuggerDisplay("Description={Description}")]
    public class AppCommunicationInterfaceConfig : IAppCommunicationInterfaceConfig
    {
        public const string ConfigFileName = "AppCommunicationInterfaceConfig.xml";

        public string Description { get; set; }

        public List<AppCommunicationInterfaceModel> InterfaceModelCollection { get; set; }

        [XmlIgnore]
        List<IAppCommunicationInterfaceModel> IAppCommunicationInterfaceConfig.InterfaceModelCollection
        {
            get { return InterfaceModelCollection.Cast<IAppCommunicationInterfaceModel>().ToList(); }
        }

        private static void Test()
        {
            var data = new AppCommunicationInterfaceConfig()
            {
                Description = "A",
                InterfaceModelCollection = new List<AppCommunicationInterfaceModel>()
                {
                    new AppCommunicationInterfaceModel()
                    {
                        IndexType = AppIndexType.InBool,
                        RelativeFilePath = "..\\a.xls"
                    },
                    new AppCommunicationInterfaceModel()
                    {
                        IndexType = AppIndexType.OutBool,
                        RelativeFilePath = "..\\a.xls"
                    },
                }
            };

            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");
        }
    }

    [XmlType("InterfaceModel")]
    [DebuggerDisplay("IndexType={IndexType}, RelativeFilePath={RelativeFilePath}")]
    public class AppCommunicationInterfaceModel : IAppCommunicationInterfaceModel
    {
        [XmlAttribute]
        public AppIndexType IndexType { get; set; }

        [XmlAttribute]
        public string RelativeFilePath { get; set; }

        [XmlIgnore]
        public string AbsolutFilePath { set; get; }

        [XmlIgnore]
        public IReadOnlyDictionary<string, int> NameIndexDictionary { get; set; }

    }


    [XmlRoot]
    [ConfigureDescription("屏的配置", FileName)]
    public class AppConfig : NotificationObject, IAppConfig
    {
        private ActureFormConfig m_ActureFormConfig;
        private AppFileConfig m_AppFileConfig;
        private AppCommunicationInterfaceConfig m_AppCommunicationInterfaceConfig;
        public const string FileName = "AppConfig.xml";

        public AppConfig()
        {
            // TODO initalize
            AppLogicConfig = new AppLogicConfigCollection();
        }

        [XmlIgnore]
        public string AppName { get; set; }

        [XmlIgnore]
        public ProjectType ProjectType
        {
            get { return SubsystemConfig.ProjectType; }
        }

        [XmlIgnore]
        public SubsystemType SubsystemType
        {
            get { return SubsystemConfig.SubsystemType; }
        }

        [XmlIgnore]
        public ISubsystemConfig SubsystemConfig { get; set; }

        [XmlIgnore]
        public AppPaths AppPaths { get; set; }

        [XmlIgnore]
        public IConfig ParentConfig { get; set; }

        [XmlIgnore]
        public IAppLogicConfigCollection AppLogicConfig { get; set; }

        [XmlIgnore]
        public AppCommunicationInterfaceConfig AppCommunicationInterfaceConfig
        {
            get { return m_AppCommunicationInterfaceConfig; }
            set
            {
                if (Equals(value, m_AppCommunicationInterfaceConfig))
                {
                    return;
                }
                m_AppCommunicationInterfaceConfig = value;
                RaisePropertyChanged(() => AppCommunicationInterfaceConfig);
            }
        }

        [XmlIgnore]
        IAppCommunicationInterfaceConfig IAppConfig.AppCommunicationInterfaceConfig
        {
            get { return AppCommunicationInterfaceConfig; }
        }

        [XmlIgnore]
        public IAppViewConfig AppViewConfig { get; set; }

        [XmlIgnore]
        public IAppObjcetConfig AppObjcetConfig { get; set; }

        /// <summary>
        /// 文件配置
        /// </summary>
        [XmlElement]
        public AppFileConfig AppFileConfig
        {
            get { return m_AppFileConfig; }
            set
            {
                if (Equals(value, m_AppFileConfig))
                {
                    return;
                }
                m_AppFileConfig = value;
                RaisePropertyChanged(() => AppFileConfig);
            }
        }

        /// <summary>
        /// 文件配置
        /// </summary>
        [XmlIgnore]
        IAppFileConfig IAppConfig.AppFileConfig
        {
            get { return AppFileConfig; }
        }

        [XmlIgnore]
        public ActureFormConfig ActureFormConfig
        {
            get { return m_ActureFormConfig; }
            set
            {
                if (Equals(value, m_ActureFormConfig))
                {
                    return;
                }
                m_ActureFormConfig = value;
                RaisePropertyChanged(() => ActureFormConfig);
            }
        }


        [XmlIgnore]
        IActureFormConfig IAppConfig.ActureFormConfig
        {
            get { return ActureFormConfig; }
        }

        [XmlElement]
        public ActureFormViewConfig ActureFormViewConfig { get; set; }

        [XmlIgnore]
        IActureFormViewConfig IAppConfig.ActureFormViewConfig
        {
            get { return ActureFormViewConfig; }
        }

    }
}
