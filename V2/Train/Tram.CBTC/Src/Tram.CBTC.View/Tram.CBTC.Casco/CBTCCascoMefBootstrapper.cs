using System;
using System.IO;
using System.Windows;
using MMI.Facility.WPFInfrastructure;
using Tram.CBTC.Casco.Model;
using Tram.CBTC.Casco.View.Shell;

namespace Tram.CBTC.Casco
{
    /// <summary>
    /// 
    /// </summary>
    public class CBTCCascoMefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
           // ImageResCreater.Create();
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Tram.CBTC.Casco", "Config"), true);
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