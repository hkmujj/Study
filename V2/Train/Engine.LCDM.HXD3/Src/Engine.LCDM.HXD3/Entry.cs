using Engine.LCDM.HXD3.Common;
using Engine.LCDM.HXD3.Config;
using Engine.LCDM.HXD3.ViewModels;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Extension;
//using MMI.Facility.Interface.Service;
using MMI.Facility.Interface.Service;

namespace Engine.LCDM.HXD3
{
    [SubsystemExport(typeof(Entry))]
    public class Entry : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var model = ServiceLocator.Current.GetInstance<LCDMViewModel>();

            var frm = new Views.Shells.LCDM(initParam);
            initParam.DataPackage.ServiceManager.GetService<IViewService>().Regist(initParam.AppConfig.AppName, frm);

            initParam.RegistDataListener(model);

            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }
    }
}