using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Urban.GuiYang.DDU.Adapter;
using Urban.GuiYang.DDU.Model;
using Urban.GuiYang.DDU.View.DataMonitor;
using Urban.GuiYang.DDU.View.Shell;

namespace Urban.GuiYang.DDU
{
    [SubsystemExport(typeof(GuiYangDDUSubystem))]
    public class GuiYangDDUSubystem : ISubsystem
    {
        private DomainAdapter m_HXD3Adapter;

        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var serviceManager = initParam.DataPackage.ServiceManager;

            var form = new GuiDDUForm();

            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);

            m_HXD3Adapter = ServiceLocator.Current.GetInstance<DomainAdapter>();
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