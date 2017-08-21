using System.IO;
using Engine.TAX2.SS7C.Adapter;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.View.Shell;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TAX2.SS7C
{
    [SubsystemExport(typeof(SS7CTCMSSubystem))]
    public class SS7CTCMSSubystem : ISubsystem
    {
        private DomainAdapter m_HXD3Adapter;

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

            m_HXD3Adapter = ServiceLocator.Current.GetInstance<DomainAdapter>();
            m_HXD3Adapter.Initalize(initParam.DataPackage.Config.SystemConfig.IsDebugModel);

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