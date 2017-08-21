using System;
using System.IO;
using System.Windows;
using MMI.Facility.WPFInfrastructure;
using Tram.CBTC.NRIET.Model;
using Tram.CBTC.NRIET.View.Shell;

namespace Tram.CBTC.NRIET
{
    /// <summary>
    /// 
    /// </summary>
    public class CBTCNrietMefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Tram.CBTC.NRIET", "Config"), true);
            return new NrietShell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            var mainWindow = (Window) this.Shell;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

        }
    }
}