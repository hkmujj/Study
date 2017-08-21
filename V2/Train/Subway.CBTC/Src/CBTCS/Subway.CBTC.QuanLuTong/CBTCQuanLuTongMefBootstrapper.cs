using System;
using System.IO;
using System.Windows;
using Subway.CBTC.QuanLuTong.Model;
using Subway.CBTC.QuanLuTong.View.Shell;
using MMI.Facility.WPFInfrastructure;

namespace Subway.CBTC.QuanLuTong
{
    /// <summary>
    /// 
    /// </summary>
    public class CBTCQuanLuTongMefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Subway.CBTC.QuanLuTong", "Config"), true);
            return new QuanLuTongShell();
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