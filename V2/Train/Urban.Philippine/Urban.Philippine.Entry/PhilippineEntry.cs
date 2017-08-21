using System.IO;
using System.Linq;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Urban.Philippine.Adapter.Interface;
using Urban.Phillippine.View.Config;

namespace Urban.Philippine.Entry
{
    [SubsystemExport(typeof(PhilippineEntry))]
    public class PhilippineEntry : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.InitParam = initParam;
            var file = Path.Combine(initParam.AppConfig.AppPaths.ConfigDirectory, GlobalParam.UserFileName);
            GlobalParam.AllUserConfig =
                DataSerialization.DeserializeFromXmlFile<UserConfig>(file).AllUser.ToDictionary(a => a.ID, a => a);
            IndexConfigure.Instance.Load(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory);
            var frm = new Main(initParam);
            var model = ServiceLocator.Current.GetInstance<IModelAdapter>();
            model.FaultManager.Init(7);
            model.FaultManager.LoadFile(initParam.AppConfig.AppPaths.ConfigDirectory);
            frm.Init(model.Model);
            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, frm);
            initParam.RegistDataListener(model);
            SetDebugValue();
        }

        private void SetDebugValue()
        {
            GlobalParam.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().ChangeState(CourseState.Started);
            for (int i = 0; i < 6; i++)
            {
                GlobalParam.InitParam.CommunicationDataService.WritableReadService.ChangeBool(2000 + i, true, true);
            }
            GlobalParam.InitParam.CommunicationDataService.WritableReadService.ChangeBool(300, true, true);
        }
    }
}