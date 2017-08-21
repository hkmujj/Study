using System.Windows;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TPX21F.HXN5B.View.DataMonitor
{
    /// <summary>
    /// DataMonitorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataMonitorWindow : Window
    {
        public DataMonitorWindow()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<HXN5BViewModel>();
        }
    }
}
