using Engine.Angola.Diesel.Adapter;
using Engine.Angola.Diesel.Model;
using Engine.Angola.Diesel.View.Shell;
using Engine.Angola.Diesel.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.Angola.Diesel
{
    [SubsystemExport(typeof(AngolaDieselSubsystem))]
    public class AngolaDieselSubsystem : ISubsystem
    {
        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();

            var viewModel = ServiceLocator.Current.GetInstance<AngolaDieselShellViewModel>();

            var view = new AngolaDieselShellForm(initParam, viewModel);

            viewService.Regist(initParam.AppConfig.AppName, view);
            viewService.Active(initParam.AppConfig.AppName);

            var adpter = ServiceLocator.Current.GetInstance< DieselModelAdapter>();
            adpter.Initalize();
        }
    }
}