using System.Windows;
using General.CIR.ViewModels;
using General.CIR.Views.Shell;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace General.CIR
{
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();
			Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			RegionManager.SetRegionManager(this, ServiceLocator.Current.GetInstance<IRegionManager>());
			CIRViewModel instance = ServiceLocator.Current.GetInstance<CIRViewModel>();
			DataContext = instance;
			DataMonitor dataMonitor = new DataMonitor
			{
				Owner = this,
				DataContext = instance
			};
			dataMonitor.Show();
			Content = ServiceLocator.Current.GetInstance<DoMain>();
		}


	}
}
