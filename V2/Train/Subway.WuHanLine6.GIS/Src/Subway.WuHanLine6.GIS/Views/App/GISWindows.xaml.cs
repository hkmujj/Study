using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Subway.WuHanLine6.GIS.ViewModels;

namespace Subway.WuHanLine6.GIS.Views.App
{
    /// <summary>
    /// GISWindows.xaml 的交互逻辑
    /// </summary>
    public partial class GISWindows : Window
    {
        public GISWindows()
        {
            InitializeComponent();
            Loaded += GISWindows_Loaded;
        }

        private void GISWindows_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.Current.GetInstance<WuHanLine6GisViewModel>();
            var monitor = new DataMonitor
            {
                DataContext = DataContext,
                Owner = this
            };
            monitor.Show();
        }
    }
}
