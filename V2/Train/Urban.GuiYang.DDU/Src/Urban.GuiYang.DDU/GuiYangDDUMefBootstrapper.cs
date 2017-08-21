using System;
using System.IO;
using System.Windows;
using MMI.Facility.WPFInfrastructure;
using Urban.GuiYang.DDU.Model;
using Urban.GuiYang.DDU.View.Shell;

namespace Urban.GuiYang.DDU
{
    /// <summary>
    /// 
    /// </summary>
    public class GuiYangDDUMefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    GlobalParam.Instance.DesignName, "Config"));
            return new GuiYangShell();
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