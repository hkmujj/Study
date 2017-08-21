using Engine.TCMS.HXD3.DataAdapter;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Resource.Keys;
using Engine.TCMS.HXD3.View.Form;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.TCMS.HXD3
{
    [SubsystemExport(typeof(HXD3TCMSSubystem))]
    public class HXD3TCMSSubystem : ISubsystem
    {
        private HXD3Adapter m_HXD3Adapter;

        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var serviceManager = initParam.DataPackage.ServiceManager;

            var form = new HXD3TCMS();

            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);

            m_HXD3Adapter = ServiceLocator.Current.GetInstance<HXD3Adapter>();
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
            initParam.CommunicationDataService.WritableReadService.ChangeBool(
                GlobalParam.Instance.IndexDescription.InBoolDescriptionDictionary[Inb.主界面_牵引制动画面_亮屏], true);
        }
    }
}