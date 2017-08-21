using Engine.Angola.TCMS.DataAdapter;
using Engine.Angola.TCMS.Model;
using Engine.Angola.TCMS.View.Shell;
using Engine.Angola.TCMS.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.Angola.TCMS
{
    [SubsystemExport(typeof(AngolaTCMSSubsystem))]
    public class AngolaTCMSSubsystem : ISubsystem
    {
        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();

            var viewModel = ServiceLocator.Current.GetInstance<AngolaTCMSShellViewModel>();

            var view = new AngolaTCMSShellForm(initParam, viewModel);

            viewService.Regist(initParam.AppConfig.AppName, view);
            viewService.Active(initParam.AppConfig.AppName);

            var adpter = ServiceLocator.Current.GetInstance<TcmsModelAdapter>();
            adpter.Initalize();
        }
    }
}