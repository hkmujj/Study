using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Regions;
using MMITool.Platform.View.Shell;

namespace MMITool.Platform
{
    class MMIToolBootstrapper : MefBootstrapper
    {

        protected override void InitializeShell()
        {
            base.InitializeShell();

            var mainWindow = (Window)Shell;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }

        protected override DependencyObject CreateShell()
        {
            //return ShellFactory.CreateShell(m_ApplicationModel);
            return Container.GetExportedValue<NormalShell>();

        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));

            AggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
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

        /// <summary>
        /// Creates the <see cref="IModuleCatalog"/> used by Prism.
        /// </summary>
        /// <remarks>
        /// The base implementation returns a new ModuleCatalog.
        /// </remarks>
        /// <returns>
        /// A ConfigurationModuleCatalog.
        /// </returns>
        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    // When using MEF, the existing Prism ModuleCatalog is still the place to configure modules via configuration files.
        //    //return new ConfigurationModuleCatalog();
        //    return new DirectoryModuleCatalog() {ModulePath = AppDomain.CurrentDomain.BaseDirectory};
        //}

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));
            //factory.AddIfMissing("WindowDialogActivationBehavior.Desktop", typeof(WindowDialogActivationBehavior));

            return factory;
        }
    }
}
