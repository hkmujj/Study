using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View.Title
{
    /// <summary>
    /// TitleView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellTitleRegion)]
    public partial class TitleView : UserControl
    {
        public TitleView()
        {
            InitializeComponent();
        }
    }
}
