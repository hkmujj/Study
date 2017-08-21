using System.Windows;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;

namespace Urban.Phillippine.View.Views
{
    /// <summary>
    ///     MainContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion, IsDefaultView = true)]
    public partial class MainContentView : UserControl
    {
        public MainContentView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BoolValueProperty = DependencyProperty.Register(
            "BoolValue", typeof (bool), typeof (MainContentView), new PropertyMetadata(default(bool),OnBoolChanged));

        private static void OnBoolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MainContentView)d).OnBoolChanged(e);
        }

        private void OnBoolChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }

        public bool BoolValue
        {
            get { return (bool) GetValue(BoolValueProperty); }
            set { SetValue(BoolValueProperty, value); }
        }
    }
}