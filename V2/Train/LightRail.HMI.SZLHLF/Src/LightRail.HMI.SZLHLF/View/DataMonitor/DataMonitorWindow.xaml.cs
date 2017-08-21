using System.Windows;
using LightRail.HMI.SZLHLF.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace LightRail.HMI.SZLHLF.View.DataMonitor
{
    /// <summary>
    /// DataMonitorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataMonitorWindow : Window
    {
        public DataMonitorWindow()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<SZLHLFViewModel>();
        }
    }
}
