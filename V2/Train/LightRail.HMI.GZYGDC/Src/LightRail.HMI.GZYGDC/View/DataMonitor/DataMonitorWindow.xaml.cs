using System.Windows;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace LightRail.HMI.GZYGDC.View.DataMonitor
{
    /// <summary>
    /// DataMonitorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataMonitorWindow : Window
    {
        public DataMonitorWindow()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>();
        }
    }
}
