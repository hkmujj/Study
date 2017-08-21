using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.View.DataMonitor
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
