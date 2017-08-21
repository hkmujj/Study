using Motor.HMI.CRH380BG.Adapter;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.View.Shell;
using Motor.HMI.CRH380BG.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Motor.HMI.CRH380BG
{
    [SubsystemExport(typeof(CRH380BGSubystem))]
    public class CRH380BGSubystem : ISubsystem
    {
        private CRH380BGAdapter m_CRH380BGAdapter;

        public CRH380BGViewModel ViewModel { private set; get; }

        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var serviceManager = initParam.DataPackage.ServiceManager;

            var form = new CRH380BGForm();

            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);

            m_CRH380BGAdapter = ServiceLocator.Current.GetInstance<CRH380BGAdapter>();
            m_CRH380BGAdapter.Initalize(initParam.DataPackage.Config.SystemConfig.IsDebugModel);

            if (initParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                //var domainMonitor = new DataMonitorForm();
                //var debugViewService = serviceManager.GetService<IDebugViewService>();
                //domainMonitor.Top = 10;
                //domainMonitor.Left = 200;
                //debugViewService.DebugFormCollection.Add(domainMonitor);
            }

            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }
    }
}