using System.ComponentModel.Composition;
using System.Windows;
using Motor.HMI.CRH380BG.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Motor.HMI.CRH380BG.View.DataMonitor
{
    /// <summary>
    /// DataMonitorWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class DataMonitorWindow : Window
    {
        public DataMonitorWindow()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<ViewModelManager>();
        }
    }
}
