using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;

namespace SH_Reconnect
{
    [SubsystemExport(typeof(App))]
    public class App : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            string appName = initParam.AppConfig.AppName;
            //var dataService = initParam.DataPackage.ServiceManager.GetService<ICommunicationDataFacadeService>().GetCommunicationDataService(initParam.AppConfig.ProjectType);
            var dataService = initParam.CommunicationDataService;
            var courseService = initParam.DataPackage.ServiceManager.GetService<ICourseService>();
            courseService.CourseStateChanged += courseService_CourseStateChanged;

            var mView = new MainView(initParam, dataService, courseService);

            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            viewService.Regist(appName, mView);
            //mView.Top = 100;
            //mView.Left = 10;
            viewService.Active(appName);

            GlobalParam.Instance.InitParam.RegistDataListener(DataChangedProxy.Instance);
        }

        void courseService_CourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            //throw new NotImplementedException();
            if (e.CourseService.CurrentCourseState == CourseState.Started)
            { }
        }
    }
}
