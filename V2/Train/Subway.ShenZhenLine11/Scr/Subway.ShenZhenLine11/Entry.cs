using System.Linq;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Enum;
using Subway.ShenZhenLine11.ViewModels;
using Subway.ShenZhenLine11.Views.Shell;

namespace Subway.ShenZhenLine11
{
    [SubsystemExport(typeof(Entry))]
    public class Entry : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            AppLog.Info("---------------------------ShenZhenLine11 Is Start-----------------------------");

            GlobalParam.Instance.InitPara = initParam;
            var frm = new ShenZhenForm(initParam);
            var model = ServiceLocator.Current.GetInstance<ShenZhenViewModel>();
            //initParam.CommunicationDataService.ReadService.BoolChanged += model.Changed;
            //initParam.CommunicationDataService.ReadService.FloatChanged += model.Changed;
            initParam.DataPackage.ServiceManager.GetService<IViewService>().Regist(initParam.AppConfig.AppName, frm);
            initParam.RegistDataListener(model);
            AppLog.Info("---------------------------ShenZhenLine11 Is Succese---------------------------");
            SetDebugValue(initParam);
        }

        private void SetDebugValue(SubsystemInitParam para)
        {
            //foreach (var value in GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary.Values)
            //{
            //    para.CommunicationDataService.WritableReadService.ChangeBool(value, true, true);
            //}
        }
    }
}