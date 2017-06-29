using System;
using System.IO;
using System.Windows;
using MMI.Facility.WPFInfrastructure;
using Subway.CBTC.THALES.Model;
using Subway.CBTC.THALES.View.Shell;

namespace Subway.CBTC.THALES
{
    /// <summary>
    /// 
    /// </summary>
    public class CBTCTHALESMefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Subway.CBTC.THALES", "Config"), true);
            return new THALESShell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            var mainWindow = (Window) this.Shell;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }

    }
}