using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Bottom
{
    /// <summary>
    /// ButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellBottomRegion, IsDefaultView = true)]
    public partial class ButtonView : UserControl
    {
        public ButtonView()
        {
            InitializeComponent();
        }
    }
}
