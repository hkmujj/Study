using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Other.ContactLine.JW4G.ViewModel;

namespace Other.ContactLine.JW4G.Views.DataMonitor
{
    /// <summary>
    /// ContactLineDataMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class ContactLineDataMonitor : Window
    {
        public ContactLineDataMonitor()
        {
            InitializeComponent();
            DataContext = ServiceLocator.Current.GetInstance<ContactLineViewModel>();
        }
    }
}
