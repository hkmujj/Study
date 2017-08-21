using System;
using System.Windows;
using Engine.Angola.Diesel.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;

namespace Engine.Angola.Diesel.View.Shell
{
    public partial class AngolaDieselShellForm : ProjectFormBase
    {
        public AngolaDieselShellForm()
        {
            InitializeComponent();

            elementHost1.Child = ServiceLocator.Current.GetInstance<AngolaDieselShellContent>();
        }

        public AngolaDieselShellForm(SubsystemInitParam initParam, AngolaDieselShellViewModel viewModel)
        {
            InitializeComponent();

            var dataPackage = initParam.DataPackage;
            var appconfig = initParam.AppConfig;
            RegionManager.SetRegionManager(elementHost1.HostContainer,
                ServiceLocator.Current.GetInstance<IRegionManager>());
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source =
                    new Uri("pack://application:,,,/Engine.Angola.Diesel;component/Resource/AngolaDieselResource.xaml")
            });

            var shellView = ServiceLocator.Current.GetInstance<AngolaDieselShellContent>();

            shellView.DataContext = viewModel;
            elementHost1.Child = shellView;

            AppName = appconfig.AppName;
            AppConfig = appconfig;
            DataPackage = dataPackage;
        }
    }
}
