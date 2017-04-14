using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;

namespace TestSubsystem.View
{
    public partial class TestSubsysForm : ProjectFormBase
    {
        public TestSubsysForm(string appName, IDataPackage dataPackage)
            : base(appName, dataPackage)
        {
            InitializeComponent();

            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());

            var view = ServiceLocator.Current.GetInstance<TestSubMainView>();
            elementHost1.Child = view;
        }

        protected TestSubsysForm()
        {
            InitializeComponent();
        }
    }
}
