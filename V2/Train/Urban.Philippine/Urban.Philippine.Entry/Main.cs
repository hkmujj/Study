using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Urban.Phillippine.View.Interface.ViewModel;
using Urban.Phillippine.View.Views;

namespace Urban.Philippine.Entry
{
    public partial class Main : ProjectFormBase
    {
        public Main()
        {
        }

        public Main(SubsystemInitParam initParam) : base(initParam.AppConfig.AppName, initParam.DataPackage)
        {
            InitializeComponent();
            RegionManager.SetRegionManager(elementHost1.Child, ServiceLocator.Current.GetInstance<IRegionManager>());
        }

        public void Init(IPhilippineViewModel model)
        {
            var tmp = elementHost1.Child as MainShell;
            if (tmp != null)
            {
                tmp.DataContext = model;
            }
        }
    }
}