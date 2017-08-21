using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Motor.LKJ._2000.Entity;

namespace Motor.LKJ._2000.Subsystem
{
    public class LKJ2000SubSystem : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            var form = new LKJ2000Form(initParam);
            var serviceManager = initParam.DataPackage.ServiceManager;
            var viewService = serviceManager.GetService<IViewService>();
            //viewService.Regist(appName, m_View);
            viewService.Regist(initParam.AppConfig.AppName, form);
            viewService.Active(initParam.AppConfig.AppName);
        }
    }
}
