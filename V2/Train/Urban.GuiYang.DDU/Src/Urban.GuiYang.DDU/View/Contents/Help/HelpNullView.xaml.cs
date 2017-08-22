using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.GuiYang.DDU.Constant;

namespace Urban.GuiYang.DDU.View.Contents.Help
{
    /// <summary>
    /// HelpNullView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.HelpRegion, IsDefaultView = true)]
    public partial class HelpNullView : UserControl
    {
        public HelpNullView()
        {
            InitializeComponent();
        }
    }
}
