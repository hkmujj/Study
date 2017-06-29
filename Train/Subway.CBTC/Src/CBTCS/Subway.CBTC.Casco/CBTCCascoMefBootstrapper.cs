using System;
using System.IO;
using System.Windows;
using MMI.Facility.WPFInfrastructure;
using Subway.CBTC.Casco.Model;
using Subway.CBTC.Casco.View.Shell;

namespace Subway.CBTC.Casco
{
    /// <summary>
    /// 
    /// </summary>
    public class CBTCCascoMefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Subway.CBTC.Casco", "Config"), true);
            return new CascoShell();
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