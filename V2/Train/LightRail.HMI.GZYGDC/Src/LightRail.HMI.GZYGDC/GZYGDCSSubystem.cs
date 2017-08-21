using System.IO;
using LightRail.HMI.GZYGDC.Adapter;
using LightRail.HMI.GZYGDC.Extension;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Resources.Keys;
using LightRail.HMI.GZYGDC.View.Shell;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace LightRail.HMI.GZYGDC
{
    [SubsystemExport(typeof(GZYGDCSSubystem))]
    public class GZYGDCSSubystem : ISubsystem
    {
        private DomainAdapter m_GZYGDCAdapter;

        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var serviceManager = initParam.DataPackage.ServiceManager;

            var form = new GZYGDCForm();

            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);

            m_GZYGDCAdapter = ServiceLocator.Current.GetInstance<DomainAdapter>();
            m_GZYGDCAdapter.Initalize(initParam.DataPackage.Config.SystemConfig.IsDebugModel);

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
            //InBoolKeys.黑屏.SendBoolValue(true, isInBool:true);
        }
    }
}