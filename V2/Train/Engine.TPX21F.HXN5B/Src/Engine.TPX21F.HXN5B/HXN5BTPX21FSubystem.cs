using Engine.TPX21F.HXN5B.Adapter;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.View.DataMonitor;
using Engine.TPX21F.HXN5B.View.Shell;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TPX21F.HXN5B
{
    [SubsystemExport(typeof(HXN5BTPX21FSubystem))]
    public class HXN5BTPX21FSubystem : ISubsystem
    {
        private HXN5BAdapter m_HXD3Adapter;

        public HXN5BViewModel ViewModel { private set; get; }

        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var serviceManager = initParam.DataPackage.ServiceManager;

            var form = new HXN5BForm();

            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);

            m_HXD3Adapter = ServiceLocator.Current.GetInstance<HXN5BAdapter>();
            m_HXD3Adapter.Initalize(initParam.DataPackage.Config.SystemConfig.IsDebugModel);

            if (initParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                var domainMonitor = new DataMonitorForm();
                var debugViewService = serviceManager.GetService<IDebugViewService>();
                domainMonitor.Top = 10;
                domainMonitor.Left = 200;
                debugViewService.DebugFormCollection.Add(domainMonitor);
            }

            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }
    }
}