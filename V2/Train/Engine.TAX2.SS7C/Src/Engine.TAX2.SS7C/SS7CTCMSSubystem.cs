using Engine.TAX2.SS7C.Adapter;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.View.Shell;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TAX2.SS7C
{
    [SubsystemExport(typeof(SS7CTCMSSubystem))]
    public class SS7CTCMSSubystem : ISubsystem
    {
        private SS7CAdapter m_SS7CAdapter;

        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var serviceManager = initParam.DataPackage.ServiceManager;

            var form = new SS7CForm();

            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);

            m_SS7CAdapter = ServiceLocator.Current.GetInstance<SS7CAdapter>();
            m_SS7CAdapter.Initalize(initParam.DataPackage.Config.SystemConfig.IsDebugModel);

            if (initParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                //var domainMonitor = new DataMonitor();
                //var debugViewService = serviceManager.GetService<IDebugViewService>();
                //domainMonitor.Top = 10;
                //domainMonitor.Left = 200;
                //debugViewService.DebugFormCollection.Add(domainMonitor);
            }

            initParam.RegistDataListener(m_SS7CAdapter);
            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }
    }
}