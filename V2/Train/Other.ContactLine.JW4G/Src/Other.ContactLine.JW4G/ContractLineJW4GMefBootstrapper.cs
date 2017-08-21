using System;
using System.IO;
using System.Windows;
using MMI.Facility.WPFInfrastructure;
using Other.ContactLine.JW4G.Model;

namespace Other.ContactLine.JW4G
{
    public class ContractLineJW4GMefBootstrapper : MMIMefBootstrapper
    {
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        /// The shell of the application.
        /// </returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject"/>, the
        ///             <see cref="T:Microsoft.Practices.Prism.Bootstrapper"/> will attach the default <seealso cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> of
        ///             the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty"/> attached property
        ///             in order to be able to add regions by using the <seealso cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/>
        ///             attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Other.ContactLine.JW4G", "Config"));
            return new Views.ScreenView.ContactLine();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implementation ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            base.InitializeShell();
            var mainWindwo = this.Shell as Window;
            if (mainWindwo != null)
            {
                mainWindwo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Application.Current.MainWindow = mainWindwo;
                Application.Current.MainWindow.Show();
            }
        }
    }
}