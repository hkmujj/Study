using System.Windows;
using MMI.Facility.WPFInfrastructure;
using Motor.ATP._200H.Subsys.WPFView.Shell;

namespace Motor.ATP._200H.Subsys
{
    public class ATP200HMefBootstrapper: MMIMefBootstrapper
    {
        /// <summary>Creates the shell or main window of the application.</summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Microsoft.Practices.Prism.Bootstrapper" /> will attach the default <seealso cref="T:Microsoft.Practices.Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <seealso cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return new ATPDebugShell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            var mainWindow = (Window)Shell;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }
    }
}