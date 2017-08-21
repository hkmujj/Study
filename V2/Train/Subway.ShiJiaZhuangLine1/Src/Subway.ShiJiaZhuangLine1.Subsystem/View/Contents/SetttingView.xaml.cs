using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents
{
    /// <summary>
    /// SetttingView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContentRegion)]
    [TitleName("设置")]
    public partial class SetttingView : UserControl
    {
        public SetttingView()
        {
            InitializeComponent();
        }
    }
}
