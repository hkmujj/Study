using System;
using System.IO;
using System.Windows;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.View.Shell;
using MMI.Facility.WPFInfrastructure;

namespace Engine.TPX21F.HXN5B
{
    /// <summary>
    /// 
    /// </summary>
    public class HXN5BTPX21FMefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Engine.TPX21F.HXN5B", "Config"));
            return new TAX2HXN5BShell();
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