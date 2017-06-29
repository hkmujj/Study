using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Subway.CBTC.BeiJiaoKong.ViewModel;

namespace Subway.CBTC.BeiJiaoKong.Views.App
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += WindowsLoaded;
        }

        private void WindowsLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.Current.GetInstance<BeiJiaoKongViewModel>();
            var dataMonitor = new DataMonitor
            {
                DataContext = DataContext,
                Owner = this
            };
            dataMonitor.Show();
        }
    }
}
