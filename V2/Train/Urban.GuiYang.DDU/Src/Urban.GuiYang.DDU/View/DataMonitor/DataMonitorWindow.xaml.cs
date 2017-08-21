using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.View.DataMonitor
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
