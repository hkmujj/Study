using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using TestSubsystem1.View;
using TestSubsystem1.ViewModel;

namespace TestSubsystem1
{
    [SubsystemExport(typeof(TestSubsysEntry))]
    public class TestSubsysEntry : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            var form = new TestSubsysForm(initParam.AppConfig.AppName, initParam.DataPackage);

            initParam.DataPackage.ServiceManager.GetService<IViewService>().Regist(initParam.AppConfig.AppName, form);

            var rm = ServiceLocator.Current.GetInstance<IRegionManager>();

            //rm.RegisterViewWithRegion(RegionNames.MainContent, typeof (SubView1));

            //rm.RequestNavigate(RegionNames.MainContent, "TestSubsystem.View.SubView1");

            //ServiceLocator.Current.TryResolve<SubView1>();

            var vm = ServiceLocator.Current.GetInstance<TestSubMainViewModel>();

            initParam.RegistDataListener(vm);
        }
    }
}