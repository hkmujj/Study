using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using General.CIR.Models;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace General.CIR
{
	public class CIRMMIBootStrapper : MefBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			GlobalParams.Instance.Initialize();
			return new MainWindow();
		}

		protected override void InitializeShell()
		{
			Window window = Shell as Window;
			window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			Application.Current.MainWindow = window;
			Application.Current.MainWindow.Show();
			base.InitializeShell();
		}

		protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
		{
			IRegionBehaviorFactory regionBehaviorFactory = base.ConfigureDefaultRegionBehaviors();
			regionBehaviorFactory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));
			return regionBehaviorFactory;
		}

		protected override void ConfigureAggregateCatalog()
		{
			AggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, "*.dll"));
			AggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, "*.exe"));
		}
	}
}
