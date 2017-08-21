using System.Windows;
using Engine.TCMS.HXD3.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.HXD3.View.DataMonitor
{
    /// <summary>
    /// HXD3TCMSDataMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class HXD3TCMSDataMonitor : Window
    {
        public HXD3TCMSDataMonitor()
        {
            InitializeComponent();
            DataContext = ServiceLocator.Current.GetInstance<HXD3TCMSViewModel>();
        }
    }
}
