using System.Windows;
using CBTC.DataAdapter;
using CBTC.DataAdapter.ConcreateAdapter.QuanLuTong;
using CBTC.Infrasturcture.Model.Send;
using CBTC.Infrasturcture.Monitor;
using CBTC.Infrasturcture.ViewModel.Monitor;
using Subway.CBTC.QuanLuTong.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Subway.CBTC.QuanLuTong.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QuanLuTongShell
    {
        private readonly DomainViewModel m_DomainViewModel;


        public QuanLuTongShell()
        {
            InitializeComponent();


            m_DomainViewModel = ServiceLocator.Current.GetInstance<DomainViewModel>();

            m_DomainViewModel.Domain.SendInterface = new EmptySendInterface();

            DataContext = m_DomainViewModel;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var monitor = new DomainMonitorWindow()
            {
                Owner = this,
                DataContext = new MonitorViewModel<DomainViewModel>(m_DomainViewModel)
            };
            monitor.Show();
        }
    }
}