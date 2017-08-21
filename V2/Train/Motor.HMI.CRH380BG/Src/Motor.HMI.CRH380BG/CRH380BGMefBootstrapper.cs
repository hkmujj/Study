using System;
using System.IO;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Motor.HMI.CRH380BG.Model;
using MMI.Facility.WPFInfrastructure;
using Motor.HMI.CRH380BG.Services;
using Motor.HMI.CRH380BG.View.DataMonitor;
using Motor.HMI.CRH380BG.ViewModel;
using Motor.HMI.CRH380BG.View.Shell;

namespace Motor.HMI.CRH380BG
{
    /// <summary>
    /// 
    /// </summary>
    public class CRH380BGMefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Motor.HMI.CRH380BG.Brake/Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Config/Motor.HMI.CRH380BG"));
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Motor.HMI.CRH380BG.Traction/Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Config/Motor.HMI.CRH380BG"));

            var vmm = ServiceLocator.Current.GetInstance<ViewModelManager>();

            var shell = new CRH380BGShell() {DataContext = vmm.MainViewModel};
            shell.Loaded += (sender, args) =>
            {
                var reserve = new CRH380BGShell() {Owner = sender as Window, DataContext = vmm.ReserveViewModel};
                RegionManager.SetRegionManager(reserve,
                    ServiceLocator.Current.GetInstance<IRegionManagerProvider>().ReserveRegionManager);
                reserve.Show();

                var dm = ServiceLocator.Current.GetInstance<DataMonitorWindow>();
                dm.Owner = sender as Window;
                dm.Show();
                
            };
            return shell;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            var mainWindow = (Window) Shell;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }
    }
}