using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.View.Contents.Help;

namespace Urban.GuiYang.DDU.View.Contents.Contents
{
    /// <summary>
    /// BrakeContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentContentContent)]
    [HelpView(typeof(BrakeHelpView))]
    public partial class BrakePage1ContentView
    {
        public BrakePage1ContentView()
        {
            InitializeComponent();
        }
    }
}
