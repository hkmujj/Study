using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.WPFApp;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Regions;

namespace Engine.TCMS.HXD3
{
    /// <summary>
    /// 
    /// </summary>
    public class HXD3TCMSMefBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Engine.TCMS.HXD3", "Config"));
            return new HXD3TCMS();
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

        /// <summary>
        /// Configures the default region adapter mappings to use in the application, in order
        /// to adapt UI controls defined in XAML to use a region and register it automatically.
        /// May be overwritten in a derived class to add specific mappings required by the application.
        /// </summary>
        /// <returns>The <see cref="T:Microsoft.Practices.Prism.Regions.RegionAdapterMappings" /> instance containing all the mappings.</returns>
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mapping = base.ConfigureRegionAdapterMappings();

            if (mapping != null && ServiceLocator.Current.GetInstance<IRegionBehaviorFactory>() != null)
            {
                mapping.RegisterMapping(typeof(Border), ServiceLocator.Current.GetInstance<BorderControlRegionAdapter>());
            }

            return mapping;
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }
    }
}