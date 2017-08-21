using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using TestSubsystem1.Constant;
using TestSubsystem1.ViewModel;

namespace TestSubsystem1.View
{
    /// <summary>
    /// SubView2.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent, Priority = 2, IsDefaultView = true)]
    public partial class SubView2 : UserControl
    {
        [ImportingConstructor]
        public SubView2(Sub1ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
