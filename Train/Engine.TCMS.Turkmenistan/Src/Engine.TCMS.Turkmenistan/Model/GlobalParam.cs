using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model.ConfigModel;
using Engine.TCMS.Turkmenistan.Model.Fault;
using Excel.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TCMS.Turkmenistan.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();
        private bool m_IsTurkmenistan;
        private bool m_IsReconnection;

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName { get { return typeof(TurkmenistanSubystem).Namespace; } }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }
        public List<FaultCutUnit> FaultCutUnit { get; private set; }
        public List<AxleBitUnit> AxleBitUnit { get; private set; }
        public List<FaultInfo> FaultInfos { get; private set; }

        /// <summary>
        /// 语言是否是土库曼斯坦
        /// </summary>
        public bool IsTurkmenistan
        {
            get { return m_IsTurkmenistan; }
            set
            {
                if (m_IsTurkmenistan == value)
                {
                    return;
                }
                m_IsTurkmenistan = value;
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ReSourceChangedEvent>().Publish(null);
            }
        }

        /// <summary>
        /// 是否显示重联信息 
        /// </summary>
        public bool IsReconnection
        {
            get { return m_IsReconnection; }
            set
            {
                if (m_IsReconnection == value)
                {
                    return;
                }
                m_IsReconnection = value;
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ReconnectionChangedEvent>().Publish(new ReconnectionChangedEvent.Args(value));
            }
        }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig
                    .GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));
            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory);
        }

        public void Initalize(string rootConfigPath, string appConfigPath)
        {
            StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList().AsReadOnly();
            FaultCutUnit = ExcelParser.Parser<FaultCutUnit>(appConfigPath).ToList();
            AxleBitUnit = ExcelParser.Parser<AxleBitUnit>(appConfigPath).ToList();
            FaultInfos = ExcelParser.Parser<FaultInfo>(appConfigPath).ToList();
        }

        public void SetStartLangage(bool isThurkmenistan = false)
        {
            IsTurkmenistan = isThurkmenistan;
        }
    }
}