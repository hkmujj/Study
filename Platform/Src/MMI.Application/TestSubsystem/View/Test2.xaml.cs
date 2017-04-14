using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using TestSubsystem.Constant;

namespace TestSubsystem.View
{
    /// <summary>
    /// Test2.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.TabRegion)]
    //[ViewExport(RegionName = RegionNames.TabRegion2)]
    public partial class Test2 : UserControl
    {
        public Test2()
        {
            InitializeComponent();
        }
    }
}
