using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine7.ViewModels;

namespace Subway.ShenZhenLine7
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
            var data = ServiceLocator.Current.GetInstance<ShenZhenLine7ViewModel>();
            DataContext = data;
            var win = new DataMonitor
            {
                DataContext = data,
                Owner = this
            };
            win.Show();
            
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                this.Title = "";
            }));
        }
    }
}
