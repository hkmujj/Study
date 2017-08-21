using System;
using System.IO;
using System.Windows;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.View.Shell;
using MMI.Facility.WPFInfrastructure;

namespace Engine.TAX2.SS7C
{
    /// <summary>
    /// 
    /// </summary>
    public class SS7CTAX2MefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Engine.TAX2.SS7C", "Config"));
            return new TAX2SS7CShell();
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