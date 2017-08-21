using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace ShenHuaHaoTMS
{
    [SubsystemExport(typeof(App))]
    public class App : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var dataService = initParam.CommunicationDataService;
            var courseService = initParam.DataPackage.ServiceManager.GetService<ICourseService>();

            var mView = new BlackView(initParam, dataService, courseService);
            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, mView);
            viewService.Active(initParam.AppConfig.AppName);

            //SetValueWhenDebug(initParam);

            dataService.ReadService.RaiseAllDataChanged();
        }

        private void SetValueWhenDebug(SubsystemInitParam initParam)
        {
            if (initParam.DataPackage.Config.SystemConfig.StartModel != StartModel.Edit)
            {
                return;
            }

            var dataService = initParam.CommunicationDataService.WritableReadService;

            dataService.ChangeBool(1300, true);
        }
    }
}
