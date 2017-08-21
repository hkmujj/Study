using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using TestSubsystem1.Constant;
using TestSubsystem1.ViewModel;

namespace TestSubsystem1.View
{
    /// <summary>
    /// SubView1.xaml 的交互逻辑
    /// </summary>
    //[ViewExport(RegionName = RegionNames.MainContent)]
    //[ViewExport(RegionNameCollection = new[] { RegionNames.MainContent, RegionNames.MainContent1 })]
    [ViewExport(ViewRegionArrayDataType.Type1, new string[] { RegionNames.MainContent, "true", "0", RegionNames.MainContent1, "true", "0", })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SubView1 : UserControl
    {
        [ImportingConstructor]
        public SubView1(Sub1ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
