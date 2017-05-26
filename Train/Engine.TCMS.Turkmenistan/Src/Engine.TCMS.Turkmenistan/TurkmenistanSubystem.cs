using System.IO;
using Engine.TCMS.Turkmenistan.Adapter;
using Engine.TCMS.Turkmenistan.Model;
using Engine.TCMS.Turkmenistan.View.Shell;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TCMS.Turkmenistan
{
    [SubsystemExport(typeof(TurkmenistanSubystem))]
    public class TurkmenistanSubystem : ISubsystem
    {
        private DomainAdapter m_TurkmenistanAdapter;

        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var serviceManager = initParam.DataPackage.ServiceManager;

            var form = new TurkmenistanResourceForm();

            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);

            m_TurkmenistanAdapter = ServiceLocator.Current.GetInstance<DomainAdapter>();
            m_TurkmenistanAdapter.Initalize(initParam.DataPackage.Config.SystemConfig.IsDebugModel);

            if (initParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                //var domainMonitor = new DataMonitor();
                //var debugViewService = serviceManager.GetService<IDebugViewService>();
                //domainMonitor.Top = 10;
                //domainMonitor.Left = 200;
                //debugViewService.DebugFormCollection.Add(domainMonitor);
            }
            initParam.RegistDataListener(m_TurkmenistanAdapter);
            SetValueWhenDebug(initParam);

            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }

        private void SetValueWhenDebug(SubsystemInitParam initParam)
        {
        }
    }
}