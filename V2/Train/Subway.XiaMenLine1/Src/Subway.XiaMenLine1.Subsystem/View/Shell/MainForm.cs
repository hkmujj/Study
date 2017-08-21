using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;

namespace Subway.XiaMenLine1.Subsystem.View.Shell
{
    public partial class MainForm : ProjectFormBase
    {
        public MainForm()
        {
            InitializeComponent();
        }


        public MainForm(IAppConfig appconfig, IDataPackage dataPackage)
            : this()
        {
            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());

            elementHost1.Child = ServiceLocator.Current.GetInstance<ShellView>();

            AppName = appconfig.AppName;
            AppConfig = appconfig;
            DataPackage = dataPackage;

            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }
    }
}
