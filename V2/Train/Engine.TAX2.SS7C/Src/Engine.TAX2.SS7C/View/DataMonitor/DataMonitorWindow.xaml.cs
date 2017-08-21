using System.Windows;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TAX2.SS7C.View.DataMonitor
{
    /// <summary>
    /// DataMonitorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataMonitorWindow : Window
    {
        public DataMonitorWindow()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<SS7CViewModel>();
        }
    }
}
