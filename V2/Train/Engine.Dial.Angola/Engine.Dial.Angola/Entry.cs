using CommonUtil.Util;
using Engine.Dial.Angola.Config;
using Engine.Dial.Angola.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.Dial.Angola
{
    [SubsystemExport(typeof(Entry))]
    public class Entry : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            AppLog.Info("----------------------Engine.Dial.Angola is Start!-----------------------");
            GlobalConfigParam.Instance.IndexConfig =
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig));
            var model = ServiceLocator.Current.GetInstance<AngolaViewModel>();
            var frm = new EntryForm(initParam);
            initParam.DataPackage.ServiceManager.GetService<IViewService>().Regist(frm.AppName, frm);
            initParam.RegistDataListener(model);
            AppLog.Info("----------------------Engine.Dial.Angola Suncceed-----------------------");
        }
    }
}