using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View.Bottom
{
    /// <summary>
    /// ButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellBottomRegion)]
    public partial class ButtonView : UserControl
    {
        public ButtonView()
        {
            InitializeComponent();
        }
    }
}
