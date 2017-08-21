using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Other.ContactLine.JW4G.ViewModel;

namespace Other.ContactLine.JW4G.Views.Shells
{
    /// <summary>
    /// DoMainSheel.xaml 的交互逻辑
    /// </summary>
    public partial class DoMainSheel : UserControl
    {
        public DoMainSheel()
        {
            InitializeComponent();
            this.Loaded += DoMainSheel_Loaded;

        }

        private void DoMainSheel_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.Current.GetInstance<ContactLineViewModel>();
            RegionManager.SetRegionManager(this, ServiceLocator.Current.GetInstance<IRegionManager>());
        }
    }
}
