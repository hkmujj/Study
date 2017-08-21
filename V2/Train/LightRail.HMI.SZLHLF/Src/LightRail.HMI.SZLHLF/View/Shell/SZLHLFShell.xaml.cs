using LightRail.HMI.SZLHLF.Resources;
using LightRail.HMI.SZLHLF.View.DataMonitor;
using LightRail.HMI.SZLHLF.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace LightRail.HMI.SZLHLF.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SZLHLFShell
    {
        public SZLHLFShell()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<SZLHLFViewModel>();
            this.Resources.MergedDictionaries.Add(SZLHLFResourceManager.Instance);
            //SZLHLFResourceManager.ResourceChanged += this.ResourceChanged;
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            RegionManager.SetRegionManager(this, ServiceLocator.Current.GetInstance<IRegionManager>());
            var monitor = new DataMonitorWindow() {Owner = this};
            monitor.Show();
        }
    }
}