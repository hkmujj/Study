using LightRail.HMI.SZLHLF.Adapter;
using LightRail.HMI.SZLHLF.Model;
using LightRail.HMI.SZLHLF.View.Shell;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace LightRail.HMI.SZLHLF
{
    [SubsystemExport(typeof(SZLHLFTCMSSubystem))]
    public class SZLHLFTCMSSubystem : ISubsystem
    {
        private DomainAdapter m_DomainAdapter;

        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var serviceManager = initParam.DataPackage.ServiceManager;

            var form = new SZLHLFForm();

            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);

            m_DomainAdapter = ServiceLocator.Current.GetInstance<DomainAdapter>();
            m_DomainAdapter.Initalize(initParam.DataPackage.Config.SystemConfig.IsDebugModel);

            if (initParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                //var domainMonitor = new DataMonitor();
                //var debugViewService = serviceManager.GetService<IDebugViewService>();
                //domainMonitor.Top = 10;
                //domainMonitor.Left = 200;
                //debugViewService.DebugFormCollection.Add(domainMonitor);
            }

            SetValueWhenDebug(initParam);

            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }

        private void SetValueWhenDebug(SubsystemInitParam initParam)
        {
        }
    }
}