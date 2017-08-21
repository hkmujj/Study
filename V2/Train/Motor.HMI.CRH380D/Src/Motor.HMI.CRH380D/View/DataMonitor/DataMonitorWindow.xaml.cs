using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.View.DataMonitor
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
