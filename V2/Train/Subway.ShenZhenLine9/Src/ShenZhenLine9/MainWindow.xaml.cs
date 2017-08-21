using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine9.ViewModels;

namespace Subway.ShenZhenLine9
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// MainWindow 构造函数
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var data = ServiceLocator.Current.GetInstance<ShenZhenLine9ViewModel>();
            DataContext = data;
            var win = new DataMonitor
            {
                DataContext = data,
                Owner = this
            };
            win.Show();
        }
    }
}
