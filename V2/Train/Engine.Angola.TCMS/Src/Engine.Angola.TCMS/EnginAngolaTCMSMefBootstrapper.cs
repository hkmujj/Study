using System;
using System.IO;
using System.Windows;
using Engine.Angola.TCMS.Model;
using Engine.Angola.TCMS.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure;

namespace Engine.Angola.TCMS
{
    public class EnginAngolaTCMSMefBootstrapper : MMIMefBootstrapper
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
            return ServiceLocator.Current.GetInstance<View.Shell.AngolaTCMSShell>();
        }


        protected override void InitializeShell()
        {
            base.InitializeShell();

            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Engine.Angola.TCMS", "Config"));

            var mainWindow = (Window)this.Shell;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.DataContext = ServiceLocator.Current.GetInstance<AngolaTCMSShellViewModel>();
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }

    }
}