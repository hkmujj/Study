using System.Windows;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using Subway.WuHanLine6.ViewModels;
using Subway.WuHanLine6.Views.Shell;

namespace Subway.WuHanLine6.Views.App
{
    /// <summary>
    /// WuHanLine6.xaml 的交互逻辑
    /// </summary>
    public partial class WuHanLine6 : Window
    {
        /// <summary>
        ///
        /// </summary>
        public WuHanLine6()
        {
            InitializeComponent();
            Content = ServiceLocator.Current.GetInstance<DoMainShell>();
            this.Loaded += WuHanLine6_Loaded;
        }

        private void WuHanLine6_Loaded(object sender, RoutedEventArgs e)
        {
            AppLog.Info($"{nameof(WuHanLine6_Loaded)}");

            var monito = new DataMonitor.DataMonitor();
            AppLog.Info($"new monito");
            monito.Owner = this;
            AppLog.Info($"owner");
            monito.DataContext = ServiceLocator.Current.GetInstance<WuHanViewModel>();
            AppLog.Info($"dataContetx");
            //{
            //    Owner = this,
            //    DataContext = ServiceLocator.Current.GetInstance<WuHanViewModel>()
            //};
            AppLog.Info($"Monito Show!");
            monito.Show();

        }
    }
}