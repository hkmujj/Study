using System.Windows;

namespace MMI_PublicApp
{
    /// <summary>
    /// 
    /// </summary>
    internal class MMIMefBootstrapper : MMI.Facility.WPFInfrastructure.MMIMefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Application.Current.MainWindow;
        }
    }
}