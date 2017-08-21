using System.Windows;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.Views
{
    /// <summary>
    ///     TMSContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion)]
    public partial class TMSContentView : UserControl
    {
        public TMSContentView()
        {
            InitializeComponent();
            IsVisibleChanged += TMSContentView_IsVisibleChanged;
        }

        private void TMSContentView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && !(bool)e.OldValue && DataContext != null)
            {
                ((IPhilippineViewModel)DataContext).TMS.ChangedDisplay.Execute("1");
                FirstButton.IsChecked = true;
            }
        }
    }
}