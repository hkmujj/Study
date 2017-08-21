using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Engine.Angola.Diesel.View.Shell;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.Angola.Diesel
{
    /// <summary>
    /// 
    /// </summary>
    public class SubwayAngolaDieselMefBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<AngolaDieselShell>();
        }


        protected override void InitializeShell()
        {
            base.InitializeShell();

            var mainWindow = (Window) this.Shell;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Creates the <see cref="IModuleCatalog"/> used by Prism.
        /// </summary>
        /// <remarks>
        /// The base implementation returns a new ModuleCatalog.
        /// </remarks>
        /// <returns>
        /// A ConfigurationModuleCatalog.
        /// </returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            // When using MEF, the existing Prism ModuleCatalog is still the place to configure modules via configuration files.
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, "*.dll"));
            AggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, "*.exe"));

        }


        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }
    }
}