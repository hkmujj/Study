using System.ComponentModel.Composition;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using TestSubsystem.Constant;

namespace TestSubsystem.View
{
    /// <summary>
    /// SubSubView2.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.SubViewContent)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SubSubView2 : UserControl
    {
        public SubSubView2()
        {
            InitializeComponent();
        }
    }
}
