using System.Windows;
using Engine.TCMS.Turkmenistan.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.View.DataMonitor
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
