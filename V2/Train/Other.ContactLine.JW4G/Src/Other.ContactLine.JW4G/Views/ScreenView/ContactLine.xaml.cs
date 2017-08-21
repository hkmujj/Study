using System.Windows;
using Other.ContactLine.JW4G.Views.DataMonitor;

namespace Other.ContactLine.JW4G.Views.ScreenView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ContactLine : Window
    {
        public ContactLine()
        {
            InitializeComponent();
            this.Loaded += ContactLine_Loaded;
        }

        private void ContactLine_Loaded(object sender, RoutedEventArgs e)
        {
            var monitor = new ContactLineDataMonitor() { Owner = this };
            monitor.Show();

        }
    }
}
