using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Urban.Phillippine.View.Interface.ViewModel;
using Urban.Phillippine.View.ViewModel;
using Urban.Phillippine.View.Views.PopUp;


namespace Urban.Phillippine.View.Views
{
    /// <summary>
    ///     MaintainView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion)]
    public partial class MaintainView : UserControl
    {
        public MaintainView()
        {
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            InitializeComponent();
        }
        public IRegionManager RegionManager { get; set; }
        private void Btn_CourseEdit_Click(object sender, RoutedEventArgs e)
        {
            RegionManager.RequestNavigate(RegionNames.ReLoginRegion, ControlNames.ReLoginInfomation);
            Visibility = Visibility.Visible;
            ServiceLocator.Current.GetInstance<ReLoginPop>().Visibility = Visibility.Visible;
            
        }

    }
}