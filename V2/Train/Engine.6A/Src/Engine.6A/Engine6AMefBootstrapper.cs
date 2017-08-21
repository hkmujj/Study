using System.Windows;
using Engine._6A.Views.Shell;
using MMI.Facility.WPFInfrastructure;

namespace Engine._6A
{
    /// <summary>
    /// 
    /// </summary>
    public class Engine6AMefBootstrapper : MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new ShellWindow();
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